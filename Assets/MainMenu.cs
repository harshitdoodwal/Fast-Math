using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

	public GameObject cross, checkmark;

	public void ActionShowTutorial ()
	{

		if (GameManager.isTutorialOn) {
			cross.SetActive (true);
			checkmark.SetActive (false);
		} else {
			cross.SetActive (false);
			checkmark.SetActive (true);
		}

		GameManager.isTutorialOn = !GameManager.isTutorialOn;

	}

	public void ActionExitGame ()
	{
		//SoundManager.sm.ButtonClick ();
		Application.Quit ();
	}

	public void StartGame ()
	{
		GameManager.gm.ActionStartGame ();
	}

}

