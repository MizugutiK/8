using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class background : MonoBehaviour
{
	void Update()
	{
		transform.Translate(0 , -0.05f , 0);
		if (transform.position.y < -13.8f)
		{
			transform.position = new Vector3(0, 13.8f, 3f);
		}
	}
}