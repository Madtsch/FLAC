using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    GameObject cameraArm;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate () {
        transform.position = player.transform.position;
	}
}
