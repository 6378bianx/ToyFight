﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	// Use this for initialization

	public void loadlevel(string level)
	{
		SceneManager.LoadScene (level);

	}


}
