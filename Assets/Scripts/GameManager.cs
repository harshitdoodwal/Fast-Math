using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;

	public enum GAMESTATE
	{
		MainMenu,
		Game,
		End,
	};

	public GAMESTATE gameState;
	internal PrefabHolder prefabHolder;
	internal PuzzleManager puzzleManager;


	public static bool isWinner = false;
	public static int maxScore;
	public int m_scoreA, m_scoreB;


	void Awake ()
	{
		if (!gm)
			gm = this;
		else
			Destroy (this);

		DontDestroyOnLoad (this);
	}

	void Start ()
	{
		
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;

	}

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

	void OnDisable ()
	{
		ClearPrefabs ();
	}


	#region ButtonEvents

	public void ActionStartGame (string a_levelName)
	{
		gameState = GAMESTATE.Game;
		SceneManager.LoadScene (a_levelName);

	}

	#endregion

	#region LevelMamnagement

	internal void InitLevel (string a_levelName)
	{
		if (a_levelName == GameConstants.MAINMENU) {


			
		} else if (a_levelName == GameConstants.GAMEPLAY) {
			
			prefabHolder.gameEndPanel.SetActive (false);
			prefabHolder.textPuzzle.text = puzzleManager.GeneratePuzzle ();
			int l_countDownValue = GetCountDownValue (puzzleManager.answer [puzzleManager.m_puzzleIndex]);
			//Debug.Log ("puzzle soln " + puzzleManager.answer [puzzleManager.m_puzzleIndex]);
			CountDownTimer.instance.StartTimer (l_countDownValue);

		}
	}

	public void RestartLevel ()
	{
		gameState = GAMESTATE.Game;
		InitLevel (GameConstants.GAMEPLAY);

	}

	public void ClearPrefabs ()
	{
		m_scoreA = 0;
		m_scoreB = 0;
		puzzleManager.puzzles.Clear ();
		puzzleManager.answer.Clear ();

	}

	public void ReturnHome ()
	{
		gameState = GAMESTATE.MainMenu;
		ClearPrefabs ();
		SceneManager.LoadScene ("MainMenu");
	}

	int GetCountDownValue (int a_rangeValue)
	{
		return a_rangeValue + Random.Range (5, 10);
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
		if (a_playerID == 0) {
			m_scoreA++;
			prefabHolder.scoreA.text = m_scoreA.ToString ();
		} else if (a_playerID == 1) {
			m_scoreB++;
			prefabHolder.scoreB.text = m_scoreB.ToString ();
		}

		prefabHolder.gameEndPanel.SetActive (true);
		prefabHolder.puzzlePanel.SetActive (false);
	}

	#endregion


}
