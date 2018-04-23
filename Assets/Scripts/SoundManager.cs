using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public static SoundManager sm;
	public AudioSource src;
	public AudioClip gunshot, buttonPress;
	public AudioClip mainMenuBGM;
	public AudioClip[] gameStartBGM;

	void Awake ()
	{
		if (!sm)
			sm = this;
		else
			Destroy (this);

		DontDestroyOnLoad (this);
	}

	public void GunShot ()
	{
		src.clip = gunshot;
		src.Play ();
	}



	public void BackGroundMusic (AudioClip a_clip)
	{
		src.clip = a_clip;
		src.Play ();
	}
}

