namespace MineAssets.Helpers.Scripts
{
    using System;
    using UnityEngine;

    public class EventManager2 : MonoBehaviour
    {
        public static EventManager2 Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    	
        public event Action<SettingsElementType> OnSettingsElementEnabled;
        public void EnableSettingsElement(SettingsElementType type) 
            => OnSettingsElementEnabled?.Invoke(type);
		
        public event Action<SettingsElementType> OnSettingsElementDisabled;
        public void DisableSettingsElement(SettingsElementType type) 
            => OnSettingsElementDisabled?.Invoke(type);
		
        public event Action OnSettingsPopupClosed;
        public void CloseSettingsPopup() => OnSettingsPopupClosed?.Invoke();
    }

}