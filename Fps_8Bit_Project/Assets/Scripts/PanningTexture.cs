using UnityEngine;
using System.Collections;

public class PanningTexture : MonoBehaviour {

    private int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(0.2f, 0.2f);
    private string textureName = "_MainTex";
    private Vector2 uvOffset = Vector2.zero;

    private Renderer rend;

    void Start () {
        rend = GetComponent<Renderer>();
    }

    void LateUpdate () {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rend.enabled == true) {
            rend.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }
}
