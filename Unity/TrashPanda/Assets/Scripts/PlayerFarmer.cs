using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerFarmer : MonoBehaviour {

    public List<PlayerFarmer> PlayerList = new List<PlayerFarmer>();

    public bool Gathering;

    public byte currentResources = 0;
    public GameObject Dumpster;
    [SerializeField]
    private GameObject Target;
    private NavMeshAgent myAgent;

    public UIM uiMan;

    // Use this for initialization
    void Start () {
        myAgent = GetComponent<NavMeshAgent>();
        uiMan = FindObjectOfType<UIM>();
    }
	
	// Update is called once per frame
	void Update () {
        GatheringResources();

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            PlayerList.Clear();
        }

        

    }
    public void FixedUpdate()
    {
        GatheringActive();
    }

    public void GatheringResources()
    {
        if (Gathering == true)
        {

            Gathering = uiMan.GatheringMaster;

            float dist = Mathf.Infinity;
           ResourcePile nearestPile = null;

            ResourcePile[] pileList = GameObject.FindObjectsOfType<ResourcePile>();

            foreach (PlayerFarmer f in PlayerList)
            {

                foreach (ResourcePile p in pileList)
                {
                    float d = Vector3.Distance(this.transform.position, p.transform.position);


                    if (d < dist)
                    {
                        nearestPile = p;
                        dist = d;
                        Target = nearestPile.gameObject;
                    }


                    if (Gathering == true && Target != null)
                    {


                        if (currentResources <= 0)
                        {
                            Debug.Log("Going to gather");
                            myAgent.SetDestination(Target.transform.position);


                        }
                        if (currentResources >= 1)
                        {
                            Debug.Log("Returning to dumpster");

                            myAgent.SetDestination(Dumpster.transform.position);


                        }


                    }


                }
            }
            if (currentResources >= 1 && Gathering == true)
            {
                Debug.Log("Returning to dumpster");

                myAgent.SetDestination(Dumpster.transform.position);


            }
            if (currentResources <= 0 && Gathering == true)
            {
                Debug.Log("Returning to dumpster");

                myAgent.SetDestination(Target.transform.position);


            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResourcePile"))
        {
            currentResources += 5;
        }

        if ( other.gameObject == Dumpster.gameObject)
        {
            uiMan.CurrencyTotal += currentResources;
            currentResources = 0;
        }
    }

    public void AddFarmer()
    {
        PlayerList.Add(this);
        Gathering = uiMan.GatheringMaster;
    }

    public void GatheringActive()
    {
        foreach (PlayerFarmer selectable in PlayerList)
        {
            Gathering = uiMan.GatheringMaster;
        }
    }

   

}
