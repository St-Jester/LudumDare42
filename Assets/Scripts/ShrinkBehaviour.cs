using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShrinkBehaviour : MonoBehaviour {

	public float targetScale = 0.1f;

	[Range(0.1f, 1f)]
	public float shrinkSpeed = 0.01f;
	public bool useRandom = true;

	bool PlayerEntered = false;
	bool doneOnce = false;
	Animator anims;
	// Use this for initialization
	void Start () {
		anims = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (useRandom)
		{
			shrinkSpeed = Random.Range(0f, 0.5f);
		}

		if (PlayerEntered)
		{
			this.transform.localScale =
			Vector3.Lerp(this.transform.localScale,
			new Vector3(targetScale, targetScale, 1),
			Time.deltaTime * shrinkSpeed);

			if (this.transform.localScale.x - targetScale <= 0.04f && !doneOnce)
			{
				doneOnce = true;
				
				anims.SetTrigger("Pop");
				Destroy(this.gameObject, 2f);
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			PlayerEntered = true;
		}
	}
}
