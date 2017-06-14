using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Pixelation.Scripts;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public bool paused;

    private PlayerController playerController;
    private GameObject[] weapons;
    private Camera playerCam;
    public Pixelation pixel;

    public Slider pixelSlider;
    public Slider fovSlider;
    public Slider sensitivitySlider;

    public Toggle pixelToggle;

    private Text fovText;
    private Text pixelText;
    private Text sensText;

    public GameObject pauseObjs;

    void Start () {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        pixel = playerCam.GetComponent<Pixelation>();
        playerController = GameObject.FindObjectOfType<PlayerController>();

        fovText = fovSlider.GetComponentInChildren<Text>();
        pixelText = pixelSlider.GetComponentInChildren<Text>();
        sensText = sensitivitySlider.GetComponentInChildren<Text>();

        pixelSlider.value = 465;
        fovSlider.value = 90;
        sensitivitySlider.value = 8;
    }

    void Update() {
        if (paused) {
            pixel.BlockCount = pixelSlider.value;
            playerCam.fieldOfView = fovSlider.value;
            playerController.mouseSensitivity = sensitivitySlider.value;
            fovText.text = fovSlider.value.ToString("FOV: 0");
            pixelText.text = pixelSlider.value.ToString("PIXELIZATION: 0");
            sensText.text = sensitivitySlider.value.ToString("SENSITIVITY: 0");
        }
        if (Input.GetButtonDown("Pause")) {
            if (Time.timeScale == 1.0f) {
                Time.timeScale = 0;
                paused = true;
                pauseObjs.SetActive(true);
                pixelSlider.enabled = true;
                playerController.enabled = false;
                weapons = GameObject.FindGameObjectsWithTag("Weapon");
                foreach (GameObject obj in weapons) {
                    if (obj.name == "Shotgun") {
                        obj.GetComponent<ShotgunShoot>().enabled = false;
                    } else if (obj.name == "MachineGun") {
                        obj.GetComponent<MachineGunShoot>().enabled = false;
                    }
                }
            } else {
                Time.timeScale = 1.0f;
                paused = false;
                pauseObjs.SetActive(false);
                pixelSlider.enabled = false;
                playerController.enabled = true;
                foreach (GameObject obj in weapons) {
                    if (obj.name == "Shotgun") {
                        obj.GetComponent<ShotgunShoot>().enabled = true;
                    } else if (obj.name == "MachineGun") {
                        obj.GetComponent<MachineGunShoot>().enabled = true;
                    }
                }
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

    public void MainMenu () {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
