using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoLoadLevel : MonoBehaviour
{
	public float loadTime = 2f;
	// Use this for initialization
	IEnumerator Start ()
	{
		yield return new WaitForSeconds (loadTime);
		SceneManager.LoadScene ("Game");
	}

}

