using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowTrail : MonoBehaviour {

	public float PlayerTravelSpeed = 10f;
	public Transform WaypointParent;
	public Transform waypoint;

	private IEnumerator coroutine;
	private Vector3[] shortestPath;
	private Vector3[] positionsInWaypoints;

	//starting pos = this.position
	private void Awake()
	{
		positionsInWaypoints = new Vector3[0];

		MouseFollower.OnPositionsAdded += BuildWaypoints;
		MouseFollower.OnNewTrailStarted += ClearWaypoints;
	}

	private void ClearWaypoints()
	{
		for (int i = 0; i < positionsInWaypoints.Length; i++)
		{
			positionsInWaypoints[i] = Vector3.zero;
		}
		if (WaypointParent.childCount != 0)
		{
			var allChildren = WaypointParent.GetComponentsInChildren(typeof(Transform));
			
			for (int i = 1; i < allChildren.Length; i++)
			{
				Destroy(allChildren[i].gameObject);
			}
		}
	}

	private void OnDisable()
	{
		MouseFollower.OnPositionsAdded -= BuildWaypoints;
		MouseFollower.OnNewTrailStarted -= ClearWaypoints;
	}
	
	private void BuildWaypoints(Vector3[] positions)
	{
		if(coroutine != null)
			StopCoroutine(coroutine);
		
		positionsInWaypoints = new Vector3[positions.Length + 1];

		//set first waypoint on players current position
		positionsInWaypoints[0] = this.transform.position;
		Instantiate(waypoint, positionsInWaypoints[0], Quaternion.identity, WaypointParent);

		//fill the rest of Array
		for (int i = 0; i < positions.Length; i++)
		{
			positionsInWaypoints[i + 1] = positions[i];
			Instantiate(waypoint, positionsInWaypoints[i + 1], Quaternion.identity, WaypointParent);
		}
		//find closest position index
		//start loop from this index
		int indexInArray = GetClosestPosition(positions);
		Debug.Log(indexInArray);
		try
		{
			int newlength = positionsInWaypoints.Length - indexInArray + 1;
			shortestPath = new Vector3[newlength];
			shortestPath[0] = positionsInWaypoints[0];
			for (int i = 1; i < shortestPath.Length-1; i++)
			{
				shortestPath[i] = positionsInWaypoints[indexInArray + i];
			}
		}
		catch(IndexOutOfRangeException outofrange)
		{
			Debug.LogWarning(outofrange.Message);
		}
		//start moving need a delay
		coroutine = MovePlayer(shortestPath);
		StartCoroutine(coroutine);
	}

	private int GetClosestPosition(Vector3[] positions)
	{
		//store distances in array
		//get smallest
		//return index
		float[] distances = new float [positions.Length];

		for (int i = 0; i < positions.Length; i++)
		{
			distances[i] = Vector3.Distance(this.transform.position, positions[i]);
		}
		float min = distances.Min();
		return distances.ToList().IndexOf(min);
	}


	private IEnumerator MovePlayer(Vector3[] waypoints)
	{
		//disable mouse when player moves

		//===========needs fixing anyway============
		//move along path
		
		transform.position = waypoints[0];
		int targetWaypointIndex = 1;
		Vector3 targetWaypoint = waypoints[targetWaypointIndex];

		
		while (targetWaypointIndex < waypoints.Length-1)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, PlayerTravelSpeed * Time.deltaTime);
			if (transform.position == targetWaypoint)
			{
				targetWaypointIndex++;
				targetWaypoint = waypoints[targetWaypointIndex];
				
			}
			yield return null;
		}
	}

	void OnDrawGizmos()
	{
		if (WaypointParent.childCount > 0)
		{
			Vector3 startPosition = WaypointParent.GetChild(0).position;
			Vector3 previousPosition = startPosition;

			foreach (Transform waypoint in WaypointParent)
			{
				Gizmos.DrawSphere(waypoint.position, .3f);
				Gizmos.DrawLine(previousPosition, waypoint.position);
				previousPosition = waypoint.position;
			}
		}
	}
}
