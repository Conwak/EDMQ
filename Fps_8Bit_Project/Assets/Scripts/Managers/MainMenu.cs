﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Start () {
        
	}
	
	void Update () {
	
	}

    public void PlayGame () {
        SceneManager.LoadScene("E1", LoadSceneMode.Single);
    }

    public void OptionsMenu () {

    }

    public void ExitGame () {
        Application.Quit();
    }
}