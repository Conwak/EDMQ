using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Start () {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
	}
	
	void Update () {
	
	}

    public void PlayGame () {
        SceneManager.UnloadScene("MainMenu");
        SceneManager.LoadScene("E1", LoadSceneMode.Single);
    }

    public void OptionsMenu () {

    }

    public void ExitGame () {
        Application.Quit();
    }
}
