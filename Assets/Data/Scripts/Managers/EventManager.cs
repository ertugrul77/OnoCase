using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
		
    public event Action OnHousePressed;
    public void HousePressed() => OnHousePressed?.Invoke();
    
    public event Action OnBackButtonPressed;
    public void BackButtonPressed() => OnBackButtonPressed?.Invoke();
    
}
