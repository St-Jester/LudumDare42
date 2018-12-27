using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNumFade : MonoBehaviour {

	public float FadeTime = 1.5f;
	TMP_Text tMP_Text;

	// Use this for initialization
	void Start () {
		tMP_Text = GetComponent<TMP_Text>();
	}
	
	// Update is called once per frame
	void Update () {
		tMP_Text.CrossFadeAlpha(0, FadeTime, false);

	}
}
