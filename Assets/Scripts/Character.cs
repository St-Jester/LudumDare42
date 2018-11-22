using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	public Slider mainSlider;
	public float MaxHealth = 100f;

	public float Health = 100f;
	[Range(0f, 1f)]
	public float DamageRate = 0.1f;
	public float ColdDamage = 1f;
	private IEnumerator coroutineDamage, coroutineHeal;

	public float HealthRestore = 1f;
	[Range(0f,1f)]
	public float RegenRate = 0.1f;


	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Bubble"))
		{
			if (coroutineDamage != null)
			{
				StopCoroutine(coroutineDamage);
				Debug.Log("StoppedCoroutineDamage");
			}
			coroutineHeal = ResoreHealth();
			StartCoroutine(coroutineHeal);
			Debug.Log("StartedCoroutine ResoreHealth");
		}
		else
			if (collision.CompareTag("Enemy"))
			{
			GetDamage(collision.gameObject.GetComponent<EnemyData>().enemy.Damage);
			}
		
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Bubble"))
		{
			coroutineDamage = GetPeriodicDamage();
			Debug.Log("StartedCoroutine GetPeriodicDamage");

			StartCoroutine(coroutineDamage);

			if(coroutineHeal!=null)
			{
				StopCoroutine(coroutineHeal);
				Debug.Log("StoppedCoroutineHeal");

			}
		}
	}


	public IEnumerator GetPeriodicDamage()
	{
		while(Health >= 0)
		{
			GetDamage(ColdDamage);
			yield return new WaitForSeconds(DamageRate);
		}
		yield return null;
	}

	private void GetDamage(float damageAmount)
	{
		Health -= damageAmount;
	}

	public IEnumerator ResoreHealth()
	{
		while (Health < MaxHealth)
		{
			Health += HealthRestore;
			yield return new WaitForSeconds(RegenRate);
		}
		yield return null;

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//update slider

		

		mainSlider.value = MaxHealth - Health;

		if(Health<=0)
		{
			SceneManager.LoadScene("Lose");


			Cursor.visible = true;

		}
	}

	
}
