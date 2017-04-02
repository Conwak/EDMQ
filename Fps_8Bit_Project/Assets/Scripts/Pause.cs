using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Pixelation.Scripts;

public class Pause : MonoBehaviour {

    public GameObject pauseText;
    public bool paused;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private ShotgunShoot shotgunShoot;
    private GameObject playerCam;

    [SerializeField]
    private Slider pixelSlider;
    [SerializeField]
    private GameObject slider;
    private Pixelation pixel;

    void Start () {
        pixelSlider.value = 312;
    }

    void Update() {
        if (!player) {
            FindPlayer();
        }
        if (playerCam && player) {
            pixel = playerCam.GetComponent<Pixelation>();
            pixel.BlockCount = pixelSlider.value;
            playerController = player.GetComponent<PlayerController>();
            shotgunShoot = playerCam.GetComponentInChildren<ShotgunShoot>();
        }
        if (Input.GetKeyDown("backspace")) {
            if (Time.timeScale == 1.0f) {
                Time.timeScale = 0;
                paused = true;
                pauseText.SetActive(true);
                slider.SetActive(true);
                pixelSlider.enabled = true;
                playerController.enabled = false;
                shotgunShoot.enabled = false;
            } else {
                Time.timeScale = 1.0f;
                paused = false;
                pauseText.SetActive(false);
                slider.SetActive(false);
                pixelSlider.enabled = false;
                playerController.enabled = true;
                shotgunShoot.enabled = true;
            }
        }
    }

    void FindPlayer () {
        if (player != null)
            return;
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
    }
}
