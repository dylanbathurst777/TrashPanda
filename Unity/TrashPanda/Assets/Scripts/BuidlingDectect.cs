using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuidlingDectect : MonoBehaviour {

    public GroundPlacementController gm;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GroundPlacementController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Building") || other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
           Debug.Log("Cannot build");
            gm.buildingIsinside = true;
        }
       
           
        
        
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Building") || other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Can Build");
            gm.buildingIsinside = false;
        }




    }
}
