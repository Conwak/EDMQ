using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject sceneCamera;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject[] hiddenObjects;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public Transform spawn;

    public void SinglePlayer () {
        foreach (GameObject obj in hiddenObjects) {
            obj.SetActive(true);
        }
        sceneCamera.SetActive(false);
        mainMenu.SetActive(false);
        Instantiate(player, spawn.position, spawn.rotation);
        mainCamera = player.GetComponentInChildren<Camera>();
    }
}
