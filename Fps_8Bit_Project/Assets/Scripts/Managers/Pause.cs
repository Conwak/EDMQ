using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Pixelation.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Pause : MonoBehaviour {

    public bool paused;

    private PlayerController playerController;
    private GameObject[] weapons;
    private Camera playerCam;
    [SerializeField]
    public Pixelation pixel;

    [SerializeField]
    public Slider pixelSlider;
    [SerializeField]
    public Slider fovSlider;
    [SerializeField]
    public Slider sensitivitySlider;
    [SerializeField]
    public Slider audioSlider;

    [SerializeField]
    public Toggle pixelToggle;

    private Text fovText;
    private Text pixelText;
    private Text sensText;

    [SerializeField]
    public GameObject pauseObjs;

    [SerializeField]
    public AudioMixer mixer;
    public string parameterName = "MasterVolume";

    void Start () {
        fovText = fovSlider.GetComponentInChildren<Text>();
        pixelText = pixelSlider.GetComponentInChildren<Text>();
        sensText = sensitivitySlider.GetComponentInChildren<Text>();

    }

    public void ChangeVolume(float newVolume) {
        PlayerPrefs.SetFloat("volume", newVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    void Update() {
        if (!playerCam) {
            playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            pixel = playerCam.GetComponent<Pixelation>();
            playerController = GameObject.FindObjectOfType<PlayerController>();

            pixelSlider.value = 465;
            fovSlider.value = 90;
            sensitivitySlider.value = 7;
            pixelToggle.isOn = true;
        }
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
                playerController.enabled = false;
                pauseObjs.SetActive(true);
                pixelSlider.enabled = true;
                sensitivitySlider.enabled = true;
                weapons = GameObject.FindGameObjectsWithTag("Weapon");
                foreach (GameObject obj in weapons) {
                    if (obj.name == "Shotgun") {
                        obj.GetComponent<ShotgunShoot>().enabled = false;
                    } else if (obj.name == "MachineGun") {
                        obj.GetComponent<MachineGunShoot>().enabled = false;
                    } else if (obj.name == "GLauncher") {
                        obj.GetComponent<GLauncherShoot>().enabled = false;
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
                    } else if (obj.name == "GLauncher") {
                        obj.GetComponent<GLauncherShoot>().enabled = true;
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
        SceneManager.LoadScene("MainMenu");
    }
}
