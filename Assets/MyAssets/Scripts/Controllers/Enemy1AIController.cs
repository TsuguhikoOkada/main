using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の行動
/// </summary>
public class Enemy1AIController : MonoBehaviour
{
    /// <summary>
    /// 敵の行動方針
    /// </summary>
    public enum AIType
    {
        /// <summary>
        /// ただ近づいて正面から攻撃
        /// </summary>
        Breakthrough,
        /// <summary>
        /// 相手の行動後を狙って攻撃
        /// </summary>
        AfterTheMotion,
        /// <summary>
        /// 相手の後ろに向かうように動いて攻撃
        /// </summary>
        AroundToTheBack
    }
    [SerializeField]
    AIType aiType = AIType.Breakthrough;

    /// <summary>
    /// 敵の行動状況
    /// </summary>
    public enum AIState : byte
    {
        /// <summary>
        /// 待機
        /// </summary>
        Idle,
        /// <summary>
        /// 適当に移動
        /// </summary>
        Wander,
        /// <summary>
        /// 追いかける
        /// </summary>
        Seek,
        /// <summary>
        /// 対峙している
        /// </summary>
        Confronting,
        /// <summary>
        /// 攻撃
        /// </summary>
        Attack,
        /// <summary>
        /// 距離をとる
        /// </summary>
        Flee,
        /// <summary>
        /// 倒された
        /// </summary>
        Defeated
    }
    [SerializeField]
    AIState aiState = AIState.Idle;


    /// <summary>
    /// 武器(素手)情報
    /// </summary>
    WeaponInfo[] weapons = default;

    /// <summary>
    /// 素手の武器名
    /// </summary>
    [SerializeField, Tooltip("素手の場合の武器名")] string weaponBareHand = "素手";


    /// <summary>
    /// キャラクターの能力値
    /// </summary>
    CharacterStatus status = default;


    /// <summary>
    /// 弱攻撃の実施を要求
    /// </summary>
    bool doCommonAttack = false;
    /// <summary>
    /// 強攻撃の実施を要求
    /// </summary>
    bool doStrongAttack = false;
    /// <summary>
    /// 武器を装備中である
    /// </summary>
    bool isArmed = false;
    /// <summary>
    /// 攻撃範囲が有効である(ForEnemyGladiatorAnimatorから変更)
    /// </summary>
    bool isAttacking = false;
    /// <summary>
    /// 他のアクションの受付を許可しているか(ForEnemyGladiatorAnimatorから変更)
    /// </summary>
    bool isAcceptOtherActions = true;


    /// <summary>
    /// Idle状態の待機時間
    /// </summary>
    float idleTimelimit = 0.0f;
    /// <summary>
    /// timelimit_Idleのタイムアップカウンター
    /// </summary>
    float idleTimer = 0.0f;


    /// <summary>
    /// ナビメッシュ
    /// </summary>
    NavMeshAgent navmesh = default;
    /// <summary>
    /// ナビメッシュを指定した地形オブジェクトのレイヤ
    /// </summary>
    [SerializeField]
    LayerMask navmeshGroundLayers = default;
    /// <summary>
    /// 好戦的度合(0でまったく攻撃しない、1で間髪入れず攻撃し続ける)
    /// </summary>
    [SerializeField, Range(0.0f, 1.0f)]
    float warlikeRatio = 0.2f;
    /// <summary>
    /// 移動先目的地の基準値
    /// </summary>
    [SerializeField]
    GameObject destinationBase = default;
    /// <summary>
    /// 追跡・攻撃対象のオブジェクト
    /// </summary>
    [SerializeField]
    GameObject target = default;
    /// <summary>
    /// 追跡・攻撃対象のステータス
    /// </summary>
    CharacterStatus targetStatus = default;
    /// <summary>
    /// 対峙する際にとる距離
    /// </summary>
    float confrontDistance = 1.0f;
    /// <summary>
    /// 対峙後再度追跡を開始する、離された距離
    /// </summary>
    float seekAgainDistance = 4.0f;

    /// <summary>
    /// 移動時の目的地
    /// </summary>
    List<Vector3> destinations = new List<Vector3>();





    /* プロパティ */
    public bool DoCommonAttack { get => doCommonAttack; }
    public bool DoStrongAttack { get => doStrongAttack; }
    public bool IsArmed { get => isArmed; }
    public bool IsAttacking { set => isAttacking = value; }
    public bool IsAcceptOtherActions { get => isAcceptOtherActions; set => isAcceptOtherActions = value; }
    public NavMeshAgent Navmesh { get => navmesh; }
    public AIState AiState { get => aiState; }
    public AIType AiType { get => aiType; }





    // Start is called before the first frame update
    void Start()
    {
        if (!destinationBase)
        {
            destinationBase = new GameObject();
            destinationBase.transform.position = Vector3.zero;
        }

        navmesh = GetComponent<NavMeshAgent>();
        weapons = GetComponentsInChildren<WeaponInfo>();
        status = GetComponentInChildren<CharacterStatus>();
        CheckArmed();
    }

    // Update is called once per frame
    void Update()
    {
        //倒された状態なら即抜ける
        if (aiState == AIState.Defeated) return;

        //攻撃フラグ初期化
        doCommonAttack = false;
        doStrongAttack = false;

        //攻撃アニメーションが、ダメージ判定のある動きに入っている
        if (isAttacking)
        {
            //武器毎に当たり判定コライダーを起動
            foreach (WeaponInfo wep in weapons)
            {
                if (isArmed)
                {
                    if (wep.WeaponName != weaponBareHand) wep.RangeActivator(true);
                }
                else
                {
                    wep.RangeActivator(true);
                }
            }
        }
        else
        {
            //武器毎に当たり判定コライダーを終了
            foreach (WeaponInfo wep in weapons)
            {
                wep.RangeActivator(false);
            }
        }

        //操作を受け付けない状態なら即抜ける
        if (!isAcceptOtherActions) return;

        //自分が、体力が尽きやられた
        if (status.IsDefeated)
        {
            //目的地削除
            destinations = new List<Vector3>();

            //強制的に経路巡行を中断
            navmesh.isStopped = true;

            //Defeated状態へ
            aiState = AIState.Defeated;
        }

        //追跡・攻撃対象が、体力が尽きやられた
        if (targetStatus && targetStatus.IsDefeated)
        {
            //追跡・攻撃対象を解除
            target = null;

            //追跡・攻撃対象のステータスも同時に解除
            targetStatus = null;

            //Idle状態へ
            aiState = AIState.Idle;
        }

        

        //行動状態に応じて分岐
        switch (aiState)
        {
            case AIState.Idle:
                {
                    StateIdle();
                    break;
                }
            case AIState.Wander:
                {
                    StateWander();
                    break;
                }
            case AIState.Seek:
                {
                    StateSeek();
                    break;
                }
            case AIState.Confronting:
                {
                    StateConfronting();
                    break;
                }
            case AIState.Attack:
                {
                    StateAttack();
                    break;
                }
            case AIState.Flee:
                {
                    StateFlee();
                    break;
                }
            case AIState.Defeated:
                {
                    break;
                }
            default: break;
        }
    }

    /// <summary>
    /// 武器情報をチェック
    /// ・素手以外の武器を装備中かチェック
    /// ・武器すべてに自分のタグを適用
    /// ・武器に自分のステータス情報のアクセッサーを渡す
    /// </summary>
    void CheckArmed()
    {
        isArmed = false;
        foreach (WeaponInfo wep in weapons)
        {
            wep.tag = this.gameObject.tag;
            wep.Status = status;

            if (isArmed || wep.WeaponName == weaponBareHand) continue;

            isArmed = true;
        }
    }


    /// <summary>
    /// 待機状態
    /// </summary>
    void StateIdle()
    {
        //タイムリミットが未定義(待機状態になったばかり)なら、
        //待機時間を0.1秒～5.0秒で決定
        if (idleTimelimit <= 0.0f) idleTimelimit = Random.Range(0.1f, 5.0f);
            
        //カウントアップ
        idleTimer += Time.deltaTime;

        //タイムリミットに到達した
        if (idleTimelimit <= idleTimer)
        {
            //タイムリミット初期化
            idleTimelimit = 0.0f;
            //カウンター初期化
            idleTimer = 0.0f;

            //Wander状態へ
            aiState = AIState.Wander;
        }

        //追跡・攻撃対象を見つけた
        if (target)
        {
            //追跡・攻撃対象のステータスを取得
            targetStatus = target.GetComponentInChildren<CharacterStatus>();

            //Seek状態へ
            aiState = AIState.Seek;
        }
    }
    /// <summary>
    /// ランダム移動状態
    /// </summary>
    void StateWander()
    {
        //目的地座標が未定の場合
        if(destinations.Count <= 0)
        {
            //目的地となる水平位置の上空10m程度にあたる座標点を作成
            Vector3 destinationBasePos = destinationBase.transform.position;
            Vector3 destination2D = new Vector3(Random.Range(-10.0f, 10.0f) + destinationBasePos.x,
                                                destinationBasePos.y + 10.0f,
                                                Random.Range(-10.0f, 10.0f) + destinationBasePos.z);

            //上空10m程度にあたる座標点から真下の地形に向けRayを落とす
            RaycastHit hitInfo;
            bool isfound = Physics.Raycast(destination2D, Vector3.down, out hitInfo, 1000, navmeshGroundLayers);

            //地形に当たったら
            if (isfound)
            {
                //その位置を記録
                destinations.Add(hitInfo.point);

                //移動先に決定
                navmesh.SetDestination(destinations[0]);

                //経路巡行を開始
                navmesh.isStopped = false;
            }
        }

        //目的地に近づいた
        if (Vector3.SqrMagnitude(transform.position - destinations[0]) <= Mathf.Pow(1.0f, 2.0f))
        {
            //目的地削除
            destinations = new List<Vector3>();

            //Idle状態へ
            aiState = AIState.Idle;
        }

        //追跡・攻撃対象を見つけた
        if (target)
        {
            //Seek状態へ
            aiState = AIState.Seek;
        }
    }
    /// <summary>
    /// 追いかけ状態
    /// </summary>
    void StateSeek()
    {
        //水平座標における目的地
        Vector3 destination2D = target.transform.position;

        //走行状態
        status.IsRunning = true;
        navmesh.speed = 5.0f;

        //回り込み作戦なら
        if (aiType == AIType.AroundToTheBack)
        {
            //ターゲットから見て右側か左側どちらにいるか
            if(Vector3.SqrMagnitude(transform.position - (destination2D + target.transform.right)) < Vector3.SqrMagnitude(transform.position - (destination2D - target.transform.right)))
            {
                //相手の右後方を目指す
                destination2D += target.transform.right * 1.5f;
            }
            else
            {
                //相手の左後方を目指す
                destination2D -= target.transform.right * 1.5f;
            }
            destination2D -= target.transform.forward;
        }

        //追跡・攻撃対象となる敵の水平位置の上空10m程度にあたる座標点を作成
        destination2D += Vector3.up * 10.0f;

        //上空10m程度にあたる座標点から真下の地形に向けRayを落とす
        RaycastHit hitInfo;
        bool isfound = Physics.Raycast(destination2D, Vector3.down, out hitInfo, 1000, navmeshGroundLayers);

        //Rayが地形に当たったら
        if (isfound)
        {
            //その位置を記録
            destinations.Add(hitInfo.point);

            //移動先に決定
            navmesh.SetDestination(destinations[0]);

            //経路巡行を開始
            navmesh.isStopped = false;
        }

        //追跡対象に、とるべき間合いまで接近した
        if (Vector3.SqrMagnitude(transform.position - destinations[0]) <= Mathf.Pow(confrontDistance, 2.0f))
        {
            //目的地削除
            destinations = new List<Vector3>();

            //強制的に経路巡行を中断
            navmesh.isStopped = true;

            //歩行状態
            status.IsRunning = false;
            navmesh.speed = 3.5f;

            //Confronting状態へ
            aiState = AIState.Confronting;
        }
    }
    /// <summary>
    /// 対峙している状態
    /// </summary>
    void StateConfronting()
    {
        //常にtargetの方向を向かせる
        transform.LookAt(target.transform);

        //目的地座標が未定の場合
        if (destinations.Count <= 0)
        {
            /* 以下、targetの周囲を旋回するような目的地を設定 */
            Vector3 destination2D = transform.position 
                                    + transform.right * Random.Range(-2.0f, 2.0f) 
                                    + Vector3.up * 10.0f;

            //上空10m程度にあたる座標点から真下の地形に向けRayを落とす
            RaycastHit hitInfo;
            bool isfound = Physics.Raycast(destination2D, Vector3.down, out hitInfo, 1000, navmeshGroundLayers);

            //Rayが地形に当たったら
            if (isfound)
            {
                //その位置を記録
                destinations.Add(hitInfo.point);

                //移動先に決定
                navmesh.SetDestination(destinations[0]);

                //経路巡行を開始
                navmesh.isStopped = false;
            }
        }

        //目的地に近づいた
        if (Vector3.SqrMagnitude(transform.position - destinations[0]) <= Mathf.Pow(0.1f, 2.0f))
        {
            //目的地削除
            destinations = new List<Vector3>();

            //好戦的度合に基づいて、攻撃か再度移動かを決定
            if(Random.value <= warlikeRatio)
            {
                //経路巡行を終了
                navmesh.isStopped = true;

                //Attack状態へ
                aiState = AIState.Attack;
            }
        }

        //対峙対象に、間合いを離された
        if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= Mathf.Pow(seekAgainDistance, 2.0f))
        {
            //Seek状態へ
            aiState = AIState.Seek;
        }
    }
    /// <summary>
    /// 攻撃状態
    /// </summary>
    void StateAttack()
    {
        //次の操作入力を許可している
        if (isAcceptOtherActions)
        {
            //対峙対象に、間合いを離された
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= Mathf.Pow(seekAgainDistance, 2.0f))
            {
                //Seek状態へ
                aiState = AIState.Seek;
            }
            //好戦的度合に応じて、攻撃か対峙に遷移
            else if (Random.value > warlikeRatio)
            {
                //対峙に遷移の場合、Confronting状態へ
                aiState = AIState.Confronting;
            }
            //間合いはとられていない、攻撃に遷移した場合は攻撃
            else
            {
                doCommonAttack = true;

                //武器に、強攻撃であるかの情報と威力補正情報を渡す
                foreach (WeaponInfo wep in weapons)
                {
                    wep.DoStrongAttack = doStrongAttack;

                    wep.PowerRatio = 1.0f;
                }
            }
        }
    }
    /// <summary>
    /// 距離をとる状態
    /// </summary>
    void StateFlee()
    {
        
    }
}
