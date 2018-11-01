using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MySelectable : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public static HashSet<MySelectable> allMySelectables = new HashSet<MySelectable>();
    public static HashSet<MySelectable> currentlySelected = new HashSet<MySelectable>();

    Renderer myrenderer;

    [SerializeField]
    Material unselectedMaterial;

    [SerializeField]
    Material selectedMaterial;
    public void Awake()
    {
        allMySelectables.Add(this);
        myrenderer = GetComponent<Renderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect(eventData);
    }
    public void OnSelect(BaseEventData eventData)
    {
        DeselectAll(eventData);
        currentlySelected.Add(this);
        myrenderer.material = selectedMaterial;
    }


    public void OnDeselect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public static void DeselectAll(BaseEventData eventData)
    {
        foreach (MySelectable selectable in currentlySelected)
        {
            selectable.OnDeselect(eventData);
        }
        currentlySelected.Clear();
    }
   

  
}
