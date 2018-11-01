using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMold : MonoBehaviour {

    public Image deactivatedHouse;
    public Image deactivatedWall;
    public Image deactivatedGate;
    public Image deactivatedRangeUnit;
    public Image deactivatedMeleeUnit;
    public Image deactivatedFarmUnit;
    public bool GatheringMaster;

    public Button activatedHouse;
    public Button activatedWall;
    public Button activatedGate;
    public Button activatedRangeUnit;
    public Button activatedMeleeUnit;
    public Button activatedFarmUnit;

    public Button SendFarmersToGather;
    public Button StopFarmersToGather;


    public Text CurrencyText;
    public int CurrencyTotal;
    public int HouseCost = -20;
    public int WallCost = -5;
    public int GateCost = -50;
    public int RangeUnit = -15;
    public int MeleeUnit = -25;
    public int FarmerUnit = -5;

    public PlayerFarmer playerfarm;





    // Use this for initialization
    void Start () {
        // deactivatedHouse.gameObject.SetActive(true);
        //  deactivatedWall.gameObject.SetActive(true);

        // activatedHouse.gameObject.SetActive(false);
        // activatedWall.gameObject.SetActive(false);
       
        
        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        CurrencyUpdate();
        



    }

   public void CurrencyUpdate()
    {
        CurrencyText.text = "Scrap:" + CurrencyTotal.ToString();
        if (CurrencyTotal < 50 && activatedGate)
        {
            activatedGate.gameObject.SetActive(false);
            deactivatedGate.gameObject.SetActive(true);
        }
        else
        {
            activatedGate.gameObject.SetActive(true);
            deactivatedGate.gameObject.SetActive(false);
        }
        if (CurrencyTotal < 25 && activatedMeleeUnit)
        {
           activatedMeleeUnit.gameObject.SetActive(false);
            deactivatedMeleeUnit.gameObject.SetActive(true);
        }
        else
        {
            activatedMeleeUnit.gameObject.SetActive(true);
            deactivatedMeleeUnit.gameObject.SetActive(false);
        }
        if (CurrencyTotal < 20 && activatedHouse)
        {
            activatedHouse.gameObject.SetActive(false);
            deactivatedHouse.gameObject.SetActive(true);
        }
        else
        {
            activatedHouse.gameObject.SetActive(true);
            deactivatedHouse.gameObject.SetActive(false);
        }
        if (CurrencyTotal < 15 && activatedRangeUnit)
        {
           activatedRangeUnit.gameObject.SetActive(false);
            deactivatedRangeUnit.gameObject.SetActive(true);
        }
        else
        {
            activatedRangeUnit.gameObject.SetActive(true);
            deactivatedRangeUnit.gameObject.SetActive(false);
        }

        if (CurrencyTotal < 5 && activatedWall)
        {
            activatedWall.gameObject.SetActive(false);
            deactivatedWall.gameObject.SetActive(true);
            activatedFarmUnit.gameObject.SetActive(false);
            deactivatedFarmUnit.gameObject.SetActive(true);
        }
        else
        {
            activatedFarmUnit.gameObject.SetActive(true);
            deactivatedFarmUnit.gameObject.SetActive(false);
            activatedWall.gameObject.SetActive(true);
            deactivatedWall.gameObject.SetActive(false);
        }

        if (CurrencyTotal <= 999)
        {
            CurrencyText.text = "Scrap:0" + CurrencyTotal.ToString();
        }
        if (CurrencyTotal <= 99)
        {
            CurrencyText.text = "Scrap:00" + CurrencyTotal.ToString();
        }
        if (CurrencyTotal <= 9)
        {
            CurrencyText.text = "Scrap:000" + CurrencyTotal.ToString();
        }
    }
    public void GatheringIsActive()
    {
        GatheringMaster = true;
    }
    public void GatheringIsNotActive()
    {
        GatheringMaster = false;
    }

}
