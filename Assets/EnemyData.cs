using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyData : MonoBehaviour {

	public Enemy enemy;

	private void Start()
	{
		setGFX();
	}

	private void setGFX()
	{
		GetComponent<SpriteRenderer>().sprite = enemy.GFX;
	}
}
