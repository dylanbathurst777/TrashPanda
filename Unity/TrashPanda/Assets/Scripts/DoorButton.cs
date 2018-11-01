using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DoorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public GroundPlacementController GM;
    public Renderer doorRenderer;
    public Color HighLightColor;
    public Color ClickedColor;
   


    private  Color doorColor;

	// Use this for initialization
	void Start () {
        doorColor = doorRenderer.material.color;
        GM = FindObjectOfType<GroundPlacementController>();
	}
	
	public void OnPointerEnter(PointerEventData eventData)
    {
        doorRenderer.material.color = HighLightColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        doorRenderer.material.color = doorColor;
    }
    public void OnPointerClick(PointerEventData eventData)
    {


       
      
    }
    public void OnSelect(BaseEventData eventData)
    {

        doorRenderer.material.color = ClickedColor;

      


    }
    public void OnDeselect(BaseEventData eventData)
    {

        doorRenderer.material.color = doorColor;
       


    }

}
