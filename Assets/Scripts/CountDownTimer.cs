using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CountDownTimer : MonoBehaviour
{
	public static CountDownTimer instance;
	Text textTimer;

	public float m_duration{ get; set; }

	float deltaTime;
	IEnumerator coroutine;
	// Use this for initialization
	void Awake ()
	{
		instance = this;
		textTimer = GetComponent <Text> ();
		deltaTime = Time.fixedDeltaTime;
	}

	public void StartTimer (float a_duration)
	{
		
		StartCoroutine (E_StartTimer (a_duration));
	}

	IEnumerator E_StartTimer (float a_duration)
	{
		while (a_duration >= 0 && GameManager.gm.gameState == GameManager.GAMESTATE.Game) {
			m_duration = a_duration;
			textTimer.text = Mathf.RoundToInt (a_duration).ToString ();
			yield return new WaitForFixedUpdate ();
			a_duration = a_duration - deltaTime;

		}
	}

}

