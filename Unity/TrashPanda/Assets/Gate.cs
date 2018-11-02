using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
    public Animator myAnimator;
    public float RangeDetect = 5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Player[] playerUnits = GameObject.FindObjectsOfType<Player>();

        Player nearestEnemy = null;
        float dist = RangeDetect;
        foreach (Player p in playerUnits)
        {
            float d = Vector3.Distance(this.transform.position, p.transform.position);
            if (d < dist)
            {
                nearestEnemy = p;
                dist = d;

                if (Vector3.Distance(transform.position, p.transform.position) < RangeDetect)
                {





                    myAnimator.SetBool("Open", true);
                    StartCoroutine(OpenDoor());


                }
            
                
            }

            
            

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangeDetect);
    }

   public IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(5);
        myAnimator.SetBool("Open", false);
    }
}
