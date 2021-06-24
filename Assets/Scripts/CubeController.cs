using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
	

	private float speed = 3.5f;
	private float speed2 = 5.0f;

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

		float sp = speed;
		if (Input.GetButton("Jump")) sp = speed2;

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
		transform.position += move.normalized * sp * Time.deltaTime;

		transform.forward = move.normalized;


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



	
	
		


    




