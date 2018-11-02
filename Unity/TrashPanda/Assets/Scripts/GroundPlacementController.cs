using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundPlacementController : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    private GameObject HouseplaceableObjectzPrefab;
    [SerializeField]
    private GameObject WallplaceableObjectzPrefab;
    [SerializeField]
    private GameObject GateplaceableObjectzPrefab;

  

  

    [SerializeField]
    private GameObject RangeUnitPrefab;
    [SerializeField]
    private GameObject MeleeUnitPrefab;
    [SerializeField]
    private GameObject FarmerUnitPrefab;

    public GameObject[] spawnPoints;
    public GameObject currentSpawn;
    private int index;

    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.A;

    //make this private
    public GameObject currentPlaceableObject;

    public bool buildingIsinside;

    public LayerMask ground;

    public UIM uiMan;

   








    private float mouseWheelRotation;

    public Material RedTransparency;
    public Material DefultMat;

    void Start () {

        uiMan = FindObjectOfType<UIM>();
       

    }
	
	// Update is called once per frame
	void Update () {

       

        //  HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
            if (Input.GetKeyDown(newObjectHotkey))
            {
                Destroy(currentPlaceableObject);
            }
        }

      
	}

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0) && !buildingIsinside)
        {
              if(buildingIsinside)
        {
            currentPlaceableObject.GetComponentInChildren<Renderer>().material = RedTransparency;
        }
            currentPlaceableObject = null;
        }


        if (buildingIsinside)
        {
            currentPlaceableObject.GetComponentInChildren<Renderer>().material = RedTransparency;
        }


        if (!buildingIsinside)
        {
            if (currentPlaceableObject != null)
            {
                currentPlaceableObject.GetComponentInChildren<Renderer>().material = DefultMat;
            }
            
        }

    }

    private void RotateFromMouseWheel()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 90f);
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ground))
        {
            currentPlaceableObject.transform.position = hitInfo.point + new Vector3(0,1.5f,0);
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    public void HouseHandleNewObjectHotkey()
    {
        
            if(currentPlaceableObject == null)
            {
            uiMan.maxUnits = 0;

            houseScript[] houses = GameObject.FindObjectsOfType<houseScript>();


            foreach (houseScript h in houses)
            {
                uiMan.maxUnits = uiMan.maxUnits + 5;
            }
            uiMan.CurrencyTotal += uiMan.HouseCost;
                currentPlaceableObject = Instantiate(HouseplaceableObjectzPrefab);
            DefultMat = currentPlaceableObject.GetComponentInChildren<Renderer>().material;

        }
            else
            {
                Destroy(currentPlaceableObject);
                DefultMat = null;
        }
      
    }


    public void WallHandleNewObjectHotkey()
    {

        if (currentPlaceableObject == null)
        {
            uiMan.CurrencyTotal += uiMan.WallCost;
            currentPlaceableObject = Instantiate(WallplaceableObjectzPrefab);
            DefultMat = currentPlaceableObject.GetComponentInChildren<Renderer>().material;

        }
        else
        {
            Destroy(currentPlaceableObject);
            DefultMat = null;
        }

    }
    public void GateHandleNewObjectHotkey()
    {

        if (currentPlaceableObject == null)
        {
            uiMan.CurrencyTotal += uiMan.GateCost;
            currentPlaceableObject = Instantiate(GateplaceableObjectzPrefab);
            DefultMat = currentPlaceableObject.GetComponentInChildren<Renderer>().material;

        }
        else
        {
            Destroy(currentPlaceableObject);
            DefultMat = null;
        }

    }

    public void SpawnRangeUnit()
    {
        uiMan.CurrencyTotal += uiMan.RangeUnit;
        uiMan.CurrentUnits++;
        index = UnityEngine.Random.Range(0, spawnPoints.Length);
        currentSpawn = spawnPoints[index];
        Instantiate(RangeUnitPrefab, currentSpawn.transform.position, Quaternion.identity);

    }

   

    public void SpawnMeleeUnit()
    {
        uiMan.CurrencyTotal += uiMan.MeleeUnit;
        uiMan.CurrentUnits++;
        index = UnityEngine.Random.Range(0, spawnPoints.Length);
        currentSpawn = spawnPoints[index];
        Instantiate(MeleeUnitPrefab, currentSpawn.transform.position, Quaternion.identity);

    }



    public void SpawnFarmerUnit()
    {
        uiMan.CurrencyTotal += uiMan.FarmerUnit;
        uiMan.CurrentUnits++;
        index = UnityEngine.Random.Range(0, spawnPoints.Length);
        currentSpawn = spawnPoints[index];
        Instantiate(FarmerUnitPrefab, currentSpawn.transform.position, Quaternion.identity);
    }
    


}
