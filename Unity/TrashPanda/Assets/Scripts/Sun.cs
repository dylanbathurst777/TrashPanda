using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sun : MonoBehaviour {

    public WaveSpawner waveSpawner;
    public Transform from;
   public Transform to;

	// Use this for initialization
	void Start () {
        waveSpawner = FindObjectOfType<WaveSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
        TimeOfDay();

    }

    public void TimeOfDay()
    {
        if (waveSpawner.state == WaveSpawner.SpawnState.COUNTING)
        {
            

            transform.rotation = Quaternion.Slerp(this.transform.rotation, from.rotation, Time.deltaTime * 0.5f);
            return;
        }
         if(waveSpawner.state == WaveSpawner.SpawnState.WAITING)
        {

         
            transform.rotation = Quaternion.Slerp(this.transform.rotation, to.rotation, Time.deltaTime * 0.5f);
            return;
              
            
        }
    }
}
