using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ノベルゲームの会話パートのように、文章を順々に表示、ボタンで次にすすむなどを制御する
/// </summary>
public class NovelMessageController : MonoBehaviour
{
    /// <summary>
    /// 文章めくりに使うボタン名
    /// </summary>
    [SerializeField]
    string useButton = default;


    /// <summary>
    /// メッセージを表示するテキストオブジェクト
    /// </summary>
    [SerializeField]
    Text messageText = default;

    /// <summary>
    /// 発言者の名前を表示するテキストオブジェクト
    /// </summary>
    [SerializeField]
    Text nameText = default;


    /// <summary>
    /// メッセージウィンドウをクリックしたときに立たせるフラグ
    /// </summary>
    bool isClickedMessageWindow = false;

    /// <summary>
    /// 文章めくり要求フラグ
    /// </summary>
    bool isRequestProceedNextMessage = false;

    /// <summary>
    /// 遅延表示をするフラグ
    /// </summary>
    bool isDelaySkip = false;

    /// <summary>
    /// アクション実行許可フラグ
    /// </summary>
    [SerializeField]
    bool isRunnableAction = false;

    /// <summary>
    /// 全アクション実行済みフラグ
    /// </summary>
    [SerializeField]
    bool isRunAllActions = false;

    /// <summary>
    /// コルーチン内の文字表示遅延に利用するクラスインスタンス
    /// </summary>
    WaitForSeconds waitForSeconds = default;


    /// <summary>
    /// 文章格納庫
    /// </summary>
    [System.Serializable]
    public class MessageContainer
    {
        /// <summary>
        /// 名前
        /// </summary>
        [SerializeField]
        string name = "名前";

        /// <summary>
        /// この発言をする者の名前
        /// </summary>
        [SerializeField]
        string whose = "この発言をする者の名前";

        /// <summary>
        /// 文章の本文
        /// </summary>
        [SerializeField, TextArea(1,10)]
        string sentence = "文章";

        /// <summary>
        /// 表示中の文章
        /// </summary>
        string disclosuredSentence = "";

        /// <summary>
        /// 文章の表示速度
        /// </summary>
        [SerializeField]
        float speed = 0.05f;

        /// <summary>
        /// 文章をすべて表示し終えた
        /// </summary>
        bool isDisclosuredAll = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MessageContainer()
        {
            name = "名前";
            whose = "この発言をする者の名前";
            sentence = "文章";
            disclosuredSentence = "";
            speed = 0.05f;
            isDisclosuredAll = false;
        }

        public string Whose { get => whose; }
        public string DisclosuredSentence { get => disclosuredSentence; }
        public bool IsDisclosuredAll { get => isDisclosuredAll; }
        public float Speed { get => speed; }

        /// <summary>
        /// 文章表示
        /// </summary>
        public void Show()
        {
            //文字列がなければ、全文表示した扱いとする
            isDisclosuredAll = (sentence.Length <= 0);

            //文章をすべて表示し終えるまで、
            if (!isDisclosuredAll)
            {
                //「表示中の文章」に「表示中の文章」の長さ+1番目のsentenceの文字を追加する
                disclosuredSentence += sentence[disclosuredSentence.Length];

                //「表示中の文章」と「文章の本文」の長さが一致したら「文章をすべて表示し終えた」状態とする
                isDisclosuredAll = (disclosuredSentence.Length == sentence.Length);
            }
        }
    }
    [SerializeField]
    MessageContainer[] messageContainer = default;

    public bool IsRunnableAction { set => isRunnableAction = value; }
    public bool IsRunAllActions { get => isRunAllActions; }

    /// <summary>
    /// 文章を作成
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateMessage()
    {
        //アクションを実行するフラグが立つまで待つ
        while (!isRunnableAction)
        {
            //次のフレームへ
            yield return null;
        }

        foreach (MessageContainer mc in messageContainer)
        {
            //表示文字列初期化
            if(messageText) messageText.text = "";
            if(nameText) nameText.text = mc.Whose;

            //遅延秒数設定
            waitForSeconds = new WaitForSeconds(mc.Speed);

            //文字の遅延表示
            while (!mc.IsDisclosuredAll)
            {
                mc.Show();
                messageText.text = mc.DisclosuredSentence;

                //遅延を待たずに表示
                if (isDelaySkip) continue;
                yield return waitForSeconds;
            }

            //ボタンが押されるまで待つ
            do
            {
                //次のフレームへ
                yield return null;
            } while (!isRequestProceedNextMessage);

            //遅延スキップフラグを折る
            isDelaySkip = false;
        }

        //全アクション実行済みフラグを立てる
        isRunAllActions = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //「文章を作成」コルーチン実行
        StartCoroutine(CreateMessage());
    }

    // Update is called once per frame
    void Update()
    {
        RequestProceedNextMessage();

        //文章表示遅延フラグを立てる
        if (isRunnableAction && !isRunAllActions && !isDelaySkip && isRequestProceedNextMessage)
        {
            isDelaySkip = true;
        }
    }





    /// <summary>
    /// ノベルメッセージ表示を操作するボタン入力を集約するメソッド
    /// </summary>
    void RequestProceedNextMessage()
    {
        isRequestProceedNextMessage = (isClickedMessageWindow || Input.GetButtonDown(useButton));
        isClickedMessageWindow = false;
    }

    /// <summary>
    /// 画面上のボタン入力により操作するためのメソッド
    /// ButtonのOnClickにて呼び出す
    /// </summary>
    public void ClickedMessageWindow()
    {
        isClickedMessageWindow = true;
    }
}
