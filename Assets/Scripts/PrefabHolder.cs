using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PrefabHolder : MonoBehaviour
{
	public Sprite[] playerSprites;

	public PlayerController[] player;

	public Text textPuzzle;
	public Text scoreA, scoreB;

	public Text textWin;

	public GameObject gameEndPanel;
	public GameObject puzzlePanel;


	public void ActionClickRestart ()
	{
		//Debug.Log ("RestartHit");
		GameManager.gm.RestartLevel ();
	}

	public void ActionClickHome ()
	{
		GameManager.gm.ReturnHome ();
	}
}

