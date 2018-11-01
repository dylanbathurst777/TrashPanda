using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
   [SerializeField]
    private DamageType damageType;
    [SerializeField]
    private int damageAmount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            FindObjectOfType<Enemy>().TakeDamage(damageAmount, damageType);
        }
	}
    
}
