using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseFollower : MonoBehaviour {
	public delegate void PositionsAction(Vector3[] vector3s);
	public static event PositionsAction OnPositionsAdded;

	public delegate void StartTrail();
	public static event StartTrail OnNewTrailStarted;

	Vector2 pos;
	TrailRenderer tr;
	
	
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
#if UNITY_EDITOR || UNITY_STANDALONE
		if (!EventSystem.current.IsPointerOverGameObject())
		{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			tr.Clear();
			positions = new Vector3[0];
		}
		if (Input.GetKey(KeyCode.Mouse0))
		{
			if (OnNewTrailStarted != null)
			{
				OnNewTrailStarted();
			}

			tr.emitting = true;
		}

		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			tr.emitting = false;
			//===========================================Getting positions===================

			positions = new Vector3[tr.positionCount];
			NumberOfPositions = tr.GetPositions(positions);
			
			if (OnPositionsAdded != null)
			{
				OnPositionsAdded(positions);
			}
		}

		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = pos;
		}
#else
		if (!EventSystem.current.IsPointerOverGameObject())
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				tr.Clear();
				positions = new Vector3[0];
			}
			if (touch.phase == TouchPhase.Moved)
			{
				if (OnNewTrailStarted != null)
				{
					OnNewTrailStarted();
				}

				tr.emitting = true;
			}
			if (touch.phase == TouchPhase.Ended)
			{
				tr.emitting = false;

				//===========================================Getting positions===================

				positions = new Vector3[tr.positionCount];
				NumberOfPositions = tr.GetPositions(positions);
				if (OnPositionsAdded != null)
				{
					OnPositionsAdded(positions);
				}
			}

			pos = Camera.main.ScreenToWorldPoint(touch.position);
		transform.position = pos;

		} 
	}
#endif



	}
}

