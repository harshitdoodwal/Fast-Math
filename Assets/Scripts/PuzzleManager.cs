using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{


	public List<string> puzzles = new List<string> ();
	public List<int> answer = new List<int> ();

	int m_count, m_puzzleLength;

	public int m_puzzleIndex{ get; set; }


	public string GeneratePuzzle ()
	{
		if (puzzles.Count <= 0) {
			return "EOF";
		}

		int l_puzzleIndex = Random.Range (0, puzzles.Count);
		//Debug.Log ("puzzle Index " + l_puzzleIndex);
		string l_puzzle = puzzles [l_puzzleIndex];
		puzzles.RemoveAt (l_puzzleIndex);
		m_puzzleIndex = l_puzzleIndex;
		return l_puzzle;
	}
}

