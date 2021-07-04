﻿using System;
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


	bool isMovable = true;

    public bool IsRunning { get => isRunning; }
    public Vector3 MoveDirection { get => moveDirection; }
    public bool IsMovable { set => isMovable = value; }

    void Start()
    {
		GetComponent<Rigidbody>().maxAngularVelocity = 100.0f;
	}
	

	void Update()
	{

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
			if (Input.GetButton("Jump"))
			{
				sp = speed2;
				isRunning = true;
			}
			else isRunning = false;

			moveDirection = new Vector3(Input.GetAxis(button_moveHorizontal), 0.0f, Input.GetAxis(button_moveVertical)).normalized;
			transform.position += moveDirection * sp * Time.deltaTime;

			if (isRunning) transform.forward = moveDirection;
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



	
	
		


    




