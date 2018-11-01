using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour {
    private Camera main_camera;


	void Start () {
        main_camera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + main_camera.transform.rotation * Vector3.forward, main_camera.transform.rotation * Vector3.up);
        
	}
}
