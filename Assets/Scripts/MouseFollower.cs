using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour {

	
	public delegate void PositionsAction(Vector3[] vector3s);
	public static event PositionsAction OnPositionsAdded;

	public delegate void StartTrail();
	public static event StartTrail OnNewTrailStarted;

	Vector2 pos;
	TrailRenderer tr;
	//float timeElapsed = 0f;
	//float timeOfExist;
	
	//waypoint things
	public Vector3[] positions { get; private set; }
	public int NumberOfPositions { get; private set; }

	void Start () {
		
		//Cursor.visible = false;
		tr = GetComponent<TrailRenderer>();
		tr.emitting = false;
		//timeOfExist = tr.time;
	}

	void Update()
	{

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (Input.GetKeyDown(KeyCode.Mouse0) || touch.phase == TouchPhase.Began)
			{
				tr.Clear();
				positions = new Vector3[0];
			}
			if (Input.GetKey(KeyCode.Mouse0) || touch.phase == TouchPhase.Moved)
			{
				if (OnNewTrailStarted != null)
				{
					OnNewTrailStarted();
				}

				tr.emitting = true;
			}

			if (Input.GetKeyUp(KeyCode.Mouse0) || touch.phase == TouchPhase.Ended)
			{

				tr.emitting = false;

				//===========================================Getting positions===================

				positions = new Vector3[tr.positionCount];
				NumberOfPositions = tr.GetPositions(positions);

				//for (int i = 0; i < NumberOfPositions; i++)
				//{
				//	positions[i] = positions[i];
				//}

				//====================create waypoints object & fill it with positions============
				//player will access this data
				//notify via delegates pass positions
				if (OnPositionsAdded != null)
				{
					OnPositionsAdded(positions);
				}

			}
#if UNITY_EDITOR||UNITY_STANDALONE
			pos = Camera.main.ScreenToWorldPoint(Input.mousePosition );
#else
			pos = Camera.main.ScreenToWorldPoint(touch.position);
#endif
			transform.position = pos;
		}
	}
}
