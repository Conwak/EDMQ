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

    private bool isPlaying;
    public LayerMask UI;

    void Update () {
        
        if (Input.GetMouseButtonDown(0) && isPlaying == false) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.collider.name == "SinglePlayer") {
                    SinglePlayer();
                }
            }
        }
    }

    public void SinglePlayer () {
        isPlaying = true;
        foreach (GameObject obj in hiddenObjects) {
            obj.SetActive(true);
        }
        sceneCamera.SetActive(false);
        mainMenu.SetActive(false);
        Instantiate(player, spawn.position, spawn.rotation);
        mainCamera = player.GetComponentInChildren<Camera>();
    }
}
