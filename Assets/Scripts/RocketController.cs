using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketController : MonoBehaviour {

	Animator _anim;
	public string nextScene;
	private void Start()
	{
		_anim = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			_anim.SetTrigger("InRocket");
			collision.gameObject.SetActive(false);
			
		}
		if (collision.CompareTag("WinCollider"))
		{

			Cursor.visible = true;

			SceneManager.LoadScene(nextScene);

		}


	}


}
