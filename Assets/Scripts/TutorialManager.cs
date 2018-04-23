using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	public Image loadingBar;
	bool isLoading = true;

	void FixedUpdate ()
	{
		if (!isLoading)
			return;

		loadingBar.fillAmount += Time.deltaTime * 0.5f;
		if (loadingBar.fillAmount >= 0.98f) {
			isLoading = false;
			loadingBar.GetComponentInChildren <Text> ().text = "Click Here";
			loadingBar.raycastTarget = true;
		}
			
	}

	public void ActionLoadGameScene ()
	{
		//SoundManager.sm.ButtonClick ();
		GameManager.gm.StartGame ();
	}

}

