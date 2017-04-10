using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Pixelation.Scripts;

public class Pause : MonoBehaviour {

    [Header ("Pause")]
    public GameObject pauseText;
    public GameObject pauseBack;
    private bool playerFound;
    public bool paused;

    [SerializeField] [Header ("Player")]
    private GameObject player;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private ShotgunShoot shotgunShoot;
    private GunClamp gunClamp;
    private GameObject playerCam;
    private GameObject cam;

    [SerializeField] [Header ("Canvas")]
    public Slider pixelSlider;
    public Slider fovSlider;
    public GameObject slider;
    public GameObject fov;

    private Pixelation pixel;

    void Start () {
        pixelSlider.value = 312;
        fovSlider.value = 90;
    }

    void Update() {
        if (!player) {
            FindPlayer();
        }
        if (playerCam && player && !playerFound) {
            pixel = playerCam.GetComponent<Pixelation>();
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            playerController = player.GetComponent<PlayerController>();
            gunClamp = playerCam.GetComponentInChildren<GunClamp>();
            shotgunShoot = playerCam.GetComponentInChildren<ShotgunShoot>();
            playerFound = true;
        }
        if (paused) {
            pixel.BlockCount = pixelSlider.value;
            cam.GetComponent<Camera>().fieldOfView = fovSlider.value;
            fov.GetComponentInChildren<Text>().text = fovSlider.value.ToString("FOV: 0");
        }
        if (Input.GetKeyDown("backspace")) {
            if (Time.timeScale == 1.0f) {
                Time.timeScale = 0;
                paused = true;
                pauseText.SetActive(true);
                pauseBack.SetActive(true);
                slider.SetActive(true);
                fov.SetActive(true);
                pixelSlider.enabled = true;
                playerController.enabled = false;
                gunClamp.enabled = false;
                shotgunShoot.enabled = false;
            } else {
                Time.timeScale = 1.0f;
                paused = false;
                pauseText.SetActive(false);
                pauseBack.SetActive(false);
                slider.SetActive(false);
                fov.SetActive(false);
                pixelSlider.enabled = false;
                playerController.enabled = true;
                gunClamp.enabled = true;
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
