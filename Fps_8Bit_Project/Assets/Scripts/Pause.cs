using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Pixelation.Scripts;

public class Pause : MonoBehaviour {

    public bool paused;

    public PlayerController playerController;
    
    private ShotgunShoot shotgunShoot;
    private GunClamp gunClamp;
    public GameObject playerCam;

    public Pixelation pixel;

    public Slider pixelSlider;
    public Slider fovSlider;
    public Toggle pixelToggle;
    private Text fovText;

    public GameObject[] activeObjs;

    void Start () {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        pixel = playerCam.GetComponent<Pixelation>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        gunClamp = playerCam.GetComponentInChildren<GunClamp>();
        shotgunShoot = playerCam.GetComponentInChildren<ShotgunShoot>();
        fovText = fovSlider.GetComponentInChildren<Text>();
        pixelSlider.value = 465;
        fovSlider.value = 90;
    }

    void Update() {
        if (paused) {
            pixel.BlockCount = pixelSlider.value;
            fovText.text = fovSlider.value.ToString("FOV: 0");
        }
        if (Input.GetKeyDown("backspace")) {
            if (Time.timeScale == 1.0f) {
                Time.timeScale = 0;
                paused = true;
                foreach (GameObject obj in activeObjs) {
                    obj.SetActive(true);
                }
                pixelSlider.enabled = true;
                playerController.enabled = false;
                gunClamp.enabled = false;
                shotgunShoot.enabled = false;
            } else {
                Time.timeScale = 1.0f;
                paused = false;
                foreach (GameObject obj in activeObjs) {
                    obj.SetActive(false);
                }
                pixelSlider.enabled = false;
                playerController.enabled = true;
                gunClamp.enabled = true;
                shotgunShoot.enabled = true;
            }
        }
    }
    public void Pixelization () {
        if (pixelToggle.isOn == false) {
            pixel.enabled = false;
        } else {
            pixel.enabled = true;
        }
    }
}
