using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeWall : MonoBehaviour , IPointerClickHandler, ISelectHandler
{


    [SerializeField]
    public GameObject CurrentUpgradeableWall;

    public UIM uiMan;

    public int CurrentLevel = 1;

  

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect(eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        CurrentUpgradeableWall = gameObject;
        uiMan.UpgradewallCurrentselectedWall = CurrentUpgradeableWall.gameObject;
        uiMan.CurrentWallLevel = CurrentLevel;
    }

    // Use this for initialization
    void Start () {
        uiMan = FindObjectOfType<UIM>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            CurrentUpgradeableWall = null;
           
        }
    }


}
