using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
	[SerializeField]
	string mainCameraTag = "MainCamera";

	[SerializeField]
	string button_moveHorizontal = "Horizontal";
	[SerializeField]
	string button_moveVertical = "Vertical";

	bool isRunning = false;
	Vector3 moveDirection = Vector3.zero;

	private float speed = 3.5f;
	private float speed2 = 5.0f;

	Transform mainCameraTransform = default;

	CharacterStatus status = default;

	bool isMovable = true;

    public bool IsRunning { get => isRunning; }
    public Vector3 MoveDirection { get => moveDirection; }
    public bool IsMovable { set => isMovable = value; }

    void Start()
    {
		GetComponent<Rigidbody>().maxAngularVelocity = 100.0f;
		mainCameraTransform = GameObject.FindWithTag(mainCameraTag).transform;
		status = GetComponentInChildren<CharacterStatus>();

	}
	

	void Update()
	{
		if (status.IsDefeated) isMovable = false;

		//　歩くコード

		/*if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += Vector3.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += Vector3.back * speed * Time.deltaTime;
		}*/

		if (isMovable)
        {
			float sp = speed;
			if (Input.GetButton("Fire3"))
			{
				sp = speed2;
				isRunning = true;
			}
			else isRunning = false;

			//カメラ視点向き
			Transform cameraTransform = mainCameraTransform;
			//カメラ視点の正面(y軸無視)
			Vector3 forward = cameraTransform.forward;
			forward.y = 0.0f;
			forward = forward.normalized;
			//カメラ視点の右方向
			Vector3 right = cameraTransform.right;
			moveDirection = (forward * Input.GetAxis(button_moveVertical) 
							+ right * Input.GetAxis(button_moveHorizontal)).normalized;

			transform.position += moveDirection * sp * Time.deltaTime;

			if(isRunning)
            {
				Quaternion charDirectionQuaternion = Quaternion.LookRotation(moveDirection);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, charDirectionQuaternion, 180.0f * Time.deltaTime);
            }
            else
            {
				Quaternion charDirectionQuaternion = Quaternion.LookRotation(Vector3.Normalize(forward));
				transform.rotation = Quaternion.RotateTowards(transform.rotation, charDirectionQuaternion, 360.0f * Time.deltaTime);
			}
		}



		//　ここまで

		//　走るコード
		/*
				if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.RightArrow))
				{
					transform.position += Vector3.right * speed2 * Time.deltaTime;

				}
				if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow))
				{
					transform.position += Vector3.left * speed2 * Time.deltaTime;

				}
				if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.UpArrow))
				{
					transform.position += Vector3.forward * speed2 * Time.deltaTime;
				}
				if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow))
				{
					transform.position += Vector3.back * speed2 * Time.deltaTime;
				}*/



		//　ここまで
		

	}

}



	
	
		


    




