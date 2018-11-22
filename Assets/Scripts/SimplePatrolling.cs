using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrolling : MonoBehaviour {

	public List<Transform> waypoints;
	public float TimeOnStay;
	public float moveSpeed;

	int waypointIndex = 0;

	
	private void Start()
	{
		StartCoroutine(EndlessMove());
	}

	private IEnumerator EndlessMove()
	{
		while (true)
		{
			if (waypointIndex <= waypoints.Count - 1)
			{
				Vector2 tagetPos = waypoints[waypointIndex].position;

				transform.position = Vector2.MoveTowards(this.transform.position, tagetPos, moveSpeed * Time.deltaTime);
				if (Vector2.Distance(transform.position, tagetPos) <= 0.01f)
				{
					waypointIndex++;
					yield return waitOnSpot(TimeOnStay);
				}
			}
			else
			{
				waypointIndex = 0;
			}
			yield return new WaitForEndOfFrame();	
		}
		
	}

	IEnumerator waitOnSpot(float timetowait)
	{
		yield return new WaitForSeconds(timetowait);
	}
}
