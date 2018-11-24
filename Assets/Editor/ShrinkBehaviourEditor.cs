using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShrinkBehaviour))]
public class ShrinkBehaviourEditor : Editor
{
	override public void OnInspectorGUI()
	{
		var myScript = target as ShrinkBehaviour;

		myScript.shrinkSpeed = EditorGUILayout.FloatField("Shrink Speed", myScript.shrinkSpeed);
		myScript.targetScale = EditorGUILayout.FloatField("Target Scale", myScript.targetScale);

		myScript.useRandom = GUILayout.Toggle(myScript.useRandom, "Use Random");

		if (myScript.useRandom)
		{
			myScript.minRandom = EditorGUILayout.FloatField("Min Random", myScript.minRandom);
			myScript.maxRandom = EditorGUILayout.FloatField("Max Random", myScript.maxRandom);
		}
	}
}