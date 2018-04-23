using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;

	public enum GAMESTATE
	{
		Default,
		MainMenu,
		Game,
		End,
	};

	public GAMESTATE gameState;
	internal PrefabHolder prefabHolder;
	internal PuzzleManager puzzleManager;


	public static bool isWinner = false;
	public static bool isReseting = false;

	public int m_scoreA, m_scoreB;

	public static bool isTutorialOn = true;


	void Awake ()
	{
		if (gm == null)
			gm = this;
		else if (gm != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		//Debug.Log ("Awake Called");
	}

	void Start ()
	{
		
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		//InitLevel (GameConstants.MAINMENU);

	}


	void OnDisable ()
	{
		ClearPrefabs ();
	}


	#region ButtonEvents

	public void ActionStartGame ()
	{
		if (!isTutorialOn) {
			gameState = GAMESTATE.Game;
			SceneManager.LoadScene (GameConstants.GAMEPLAY);
		} else {
			SceneManager.LoadScene ("Tutorial");
		}
	}

	public void StartGame ()
	{
		gameState = GAMESTATE.Game;
		SceneManager.LoadScene (GameConstants.GAMEPLAY);
	}





	#endregion

	#region LevelMamnagement

	public void SceneManager_sceneLoaded (Scene arg0, LoadSceneMode arg1)
	{
		//Debug.Log ("Scene Loaded");
		if (gameState == GAMESTATE.Game) {

			prefabHolder = FindObjectOfType<PrefabHolder> ();
			puzzleManager = FindObjectOfType <PuzzleManager> ();
			InitLevel (GameConstants.GAMEPLAY);

		} else if (gameState == GAMESTATE.MainMenu) {

			InitLevel (GameConstants.MAINMENU);

		}
	}


	internal void InitLevel (string a_levelName)
	{
		if (a_levelName == GameConstants.MAINMENU) {
			

			//Debug.Log ("init menu called");
			SoundManager.sm.BackGroundMusic (SoundManager.sm.mainMenuBGM);
			
		} else if (a_levelName == GameConstants.GAMEPLAY) {
			isReseting = true;
			SoundManager.sm.BackGroundMusic (SoundManager.sm.gameStartBGM [Random.Range (0, SoundManager.sm.gameStartBGM.Length)]);

			prefabHolder.textWin.text = string.Empty;
			string l_puzzleText = puzzleManager.GeneratePuzzle ();

			if (l_puzzleText == "EOF") {
				StartCoroutine (GameEnded ());
				return;
			}

			prefabHolder.textPuzzle.text = l_puzzleText;

			EnablePuzzlePanel ();

			if (gameState != GAMESTATE.MainMenu)
				gameState = GAMESTATE.Game;
			
			int l_countDownValue = GetCountDownValue (puzzleManager.answer [puzzleManager.m_puzzleIndex]);
			CountDownTimer.instance.StartTimer (l_countDownValue);

		}
	}

	public void RestartLevel ()
	{
		
		//Debug.Log (gameState);
		InitLevel (GameConstants.GAMEPLAY);

	}

	public void ClearPrefabs ()
	{
		m_scoreA = 0;
		m_scoreB = 0;
//		puzzleManager.puzzles.Clear ();
//		puzzleManager.answer.Clear ();
	}

	IEnumerator  GameEnded ()
	{
		int l_timer = 10;
	
		while (l_timer >= 0) {
			prefabHolder.textPuzzle.text = "Game Over ! Going Back to Main Menu in " + l_timer + " seconds";
			EnablePuzzlePanel ();
			yield return new WaitForSeconds (1f);
			l_timer--;
		}
	
		ReturnHome ();
	}

	public void ReturnHome ()
	{
		gameState = GAMESTATE.MainMenu;
		ClearPrefabs ();
		SceneManager.LoadScene ("MainMenu");
	}

	int GetCountDownValue (int a_rangeValue)
	{
		return a_rangeValue + Random.Range (7, 13);
	}

	#endregion

	#region ScoreManagement

	internal bool ValidateInput (int a_playerID)
	{
		if (CountDownTimer.instance.m_duration < puzzleManager.answer [puzzleManager.m_puzzleIndex] && GameManager.isWinner == false) {
			isWinner = true;
			return true;
		} else {
			return false;
		}

	}

	internal void AddScore (int a_playerID)
	{
		string l_winnerName = "SomeOne";
		if (a_playerID == 0) {
			m_scoreA++;
			l_winnerName = "The PiMan";
			prefabHolder.scoreA.text = m_scoreA.ToString ();
		} else if (a_playerID == 1) {
			m_scoreB++;
			l_winnerName = "The Theta";
			prefabHolder.scoreB.text = m_scoreB.ToString ();
		}

		DisablePuzzlePanel ();
		prefabHolder.textWin.text = l_winnerName + " wins!";
	}

	internal void DrawCondition ()
	{
		DisablePuzzlePanel ();
		prefabHolder.textWin.text = "Shoot you nerds";
	}

	void DisablePuzzlePanel ()
	{
		prefabHolder.gameEndPanel.SetActive (true);
		prefabHolder.puzzlePanel.SetActive (false);
	}

	void EnablePuzzlePanel ()
	{
		prefabHolder.gameEndPanel.SetActive (false);
		prefabHolder.puzzlePanel.SetActive (true);
	}

	#endregion


}
