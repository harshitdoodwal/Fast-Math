using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int m_playerID;
	public KeyCode attack;

	public float keypressTime = 0f;

	public Animator muzzleEffect;



	void Update ()
	{
		if (Input.GetKeyUp (attack) && GameManager.gm.gameState == GameManager.GAMESTATE.Game) {
			
			GameManager.gm.gameState = GameManager.GAMESTATE.End;
			this.keypressTime = CountDownTimer.instance.m_duration;
			bool l_isSuccess = GameManager.gm.ValidateInput (this.m_playerID);
			int l_winPlayerID;

			if (l_isSuccess) {
				l_winPlayerID = OnGameWin ();
				Debug.Log (this.gameObject.name + " won at " + this.keypressTime);
			} else {
				l_winPlayerID = OnGameLose ();
				Debug.Log (this.gameObject.name + " disqualified " + this.keypressTime);
			}

			Debug.Log ("win ID " + l_winPlayerID);
			GameManager.gm.AddScore (l_winPlayerID);
		}
	}

	void OnDisable ()
	{
		keypressTime = 0f;
	}

	int OnGameWin ()
	{
		muzzleEffect.Play ("Shoot");
		return this.m_playerID;
	}

	int OnGameLose ()
	{
		if (this.m_playerID == 0)
			return 1;
		else
			return 0;
	}
}
