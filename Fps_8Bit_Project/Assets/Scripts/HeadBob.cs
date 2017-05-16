﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class HeadBob : MonoBehaviour {

    private float timer = 0.0f;
    public float bobSpeed = 0.18f;
    public float bobbingAmount = 2.0f;
    public float mid = 0.5f;

	void Update () {
        float waveSlice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 localPos = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
            timer = 0.0f;
        } else {
            waveSlice = Mathf.Sin(timer);
            timer = timer + bobbingAmount;
            if (timer > Mathf.PI * 2) {
                timer = timer - (Mathf.PI * 2);
            }
        }

        if (waveSlice != 0) {
            float translateChange = waveSlice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            localPos.y = mid + translateChange;
        } else {
            localPos.y = mid;
        }
        transform.localPosition = localPos;
    }

    void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Water") {
            GetComponent<DepthOfField>().enabled = true;
            GetComponent<ScreenOverlay>().enabled = true;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Water") {
            GetComponent<DepthOfField>().enabled = false;
            GetComponent<ScreenOverlay>().enabled = false;
        }
    }
}
