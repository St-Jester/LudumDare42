using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class ShrinkBehaviour : MonoBehaviour {

	public float targetScale = 0.1f;
	public float shrinkSpeed = 0.1f;
	public bool useRandom = false;

	public float minRandom;
	public float maxRandom;


	private bool PlayerEntered = false;
	private bool doneOnce = false;
	private Animator anims;
	private Vector3 startScale = Vector3.zero;
	private float timeLerping = 0;
	private float timeTakenDuringLerp;
	private bool hasStartedLerping = false;

	void Start () {
		anims = GetComponent<Animator>();

		if (useRandom)
		{
			shrinkSpeed = Random.Range(minRandom, maxRandom);
		}
	}
	
	
	void FixedUpdate()
	{
		if (PlayerEntered)
		{
			if(!hasStartedLerping)
				StartLerping();

			timeLerping += shrinkSpeed * Time.deltaTime;
			float percentageComplete = timeLerping / timeTakenDuringLerp;

			this.transform.localScale =
			Vector3.Lerp(startScale,
			new Vector3(targetScale, targetScale, 1),
			percentageComplete);


			if (this.transform.localScale.x - targetScale <= 0.04f && !doneOnce)
			{
				this.transform.localScale = new Vector3(targetScale, targetScale, 1);
				doneOnce = true;

				anims.SetTrigger("Pop");
				Destroy(this.gameObject, 2f);
			}
		}
	}

	private void StartLerping()
	{
		startScale = this.transform.localScale;
		timeLerping = 0;
		timeTakenDuringLerp = this.transform.localScale.x / targetScale;
		hasStartedLerping = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			PlayerEntered = true;
		}
	}
}



