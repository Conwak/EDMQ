using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    private PlayerStats pStats;
    private Pause pause;

    private Scene scene;

    public GameObject endScreen;
    private Text kills;
    private Text secrets;
    private Text timeTaken;

    private float minutes;
    private float seconds;

    void Start() {
        pause = GameObject.FindObjectOfType<Pause>();
    }

    void Update() {
        if (!pStats) {
            pStats = GameObject.FindObjectOfType<PlayerStats>();
        }

        if (!pStats.levelFinished) {
            seconds += Time.deltaTime;

            if (seconds >= 60) {
                seconds = 0;
                minutes += 1;
            }
        }
        else if (pStats.levelFinished) {
            SceneManager.UnloadScene("E1");
            endScreen.SetActive(true);
            kills = GameObject.Find("Canvas/End/RawImage/Kills").GetComponent<Text>();
            secrets = GameObject.Find("Canvas/End/RawImage/Secrets").GetComponent<Text>();
            timeTaken = GameObject.Find("Canvas/End/RawImage/Time").GetComponent<Text>();

            Time.timeScale = 0;
            kills.text = pStats.levelKills.ToString("KILLS: 0 / " + pStats.currentEnemies.Length);
            secrets.text = pStats.levelSecrets.ToString("SECRETS: 0 / " + pStats.currentSecrets.Length);
            timeTaken.text = ("TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00"));
            if (Input.anyKeyDown) {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
