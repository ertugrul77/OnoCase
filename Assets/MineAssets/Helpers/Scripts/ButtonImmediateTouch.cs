using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonImmediateTouch : MonoBehaviour,IPointerDownHandler
{
    public UnityEvent pointerDown; 

    public void OnPointerDown(PointerEventData eventData) {
        pointerDown.Invoke();
    }
}
