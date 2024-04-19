using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class EventClick : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Click Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Click Up");
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Clicked");
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Click Enter");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Click Exit");
    }
}
