using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SellObjects : MonoBehaviour , IPointerClickHandler, ISelectHandler
{

    public GameObject gameObjectSellableObject;

    public bool houseOb;
    public bool gateOb;
    public bool wallOb1;
    public bool wallOb2;
    public bool wallOb3;




    public UIM uiMan;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect(eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        gameObjectSellableObject = gameObject;
        uiMan.sellingObject = gameObjectSellableObject.gameObject;
        
    }
    // Use this for initialization
    void Start () {
        uiMan = FindObjectOfType<UIM>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            gameObjectSellableObject = null;

        }
    }
}
