using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DragSelectionHandler : MonoBehaviour ,IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    [SerializeField]
    Image selectionBoxImage;

    Vector2 startPosistion;
    Rect selectionRect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            Player.DeselectAll(new BaseEventData(EventSystem.current));
        }
        selectionBoxImage.gameObject.SetActive(true);
        startPosistion = eventData.position;
        selectionRect = new Rect();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.position.x < startPosistion.x)
        {
            selectionRect.xMin = eventData.position.x;
            selectionRect.xMax = startPosistion.x;
        }
        else
        {
            selectionRect.xMin = startPosistion.x;
            selectionRect.xMax = eventData.position.x;
        }
        if (eventData.position.y < startPosistion.y)
        {
            selectionRect.yMin = eventData.position.y;
            selectionRect.yMax = startPosistion.y;
        }
        else
        {
            selectionRect.yMin = startPosistion.y;
            selectionRect.yMax = eventData.position.y;
        }
        selectionBoxImage.rectTransform.offsetMin = selectionRect.min;
        selectionBoxImage.rectTransform.offsetMax = selectionRect.max;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        selectionBoxImage.gameObject.SetActive(false);
        foreach(Player selectable in Player.allMySelectables)
        {
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(selectable.transform.position)))
            {
                selectable.OnSelect(eventData);
            }
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        float myDistance = 0;
        foreach(RaycastResult result in results)
        {
            if(result.gameObject == gameObject)
            {
                myDistance = result.distance;
                break;
            }
        }
        GameObject nextObject = null;
        float maxDistance = Mathf.Infinity;
        foreach(RaycastResult result in results)
        {
            if(result.distance > myDistance && result.distance < maxDistance)
            {
                nextObject = result.gameObject;
                maxDistance = result.distance;
            }
        }
        if (nextObject)
        {
            ExecuteEvents.Execute<IPointerEnterHandler>(nextObject, eventData, (x, y) => { x.OnPointerEnter((PointerEventData)y); });
            ExecuteEvents.Execute<IPointerClickHandler>(nextObject, eventData, (x, y) => { x.OnPointerClick((PointerEventData)y); });
            
        }
    }
}
