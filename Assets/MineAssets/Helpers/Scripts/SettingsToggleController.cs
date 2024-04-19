using MineAssets.Helpers.Scripts;
using UnityEngine;
using UnityEngine.UI;


	public class SettingsToggleController : MonoBehaviour
	{
		public SettingsBoolElement element;
		public Image activeImage;

		private void Start()
		{
			activeImage.sprite = element.GetSprite();
			element.SendCurrentEvent();
			EventManager2.Instance.OnSettingsPopupClosed += PersistCurrent;
		}

		public void OnTouchOccured()
		{
			element.InvertValue();
			activeImage.sprite = element.GetSprite();
		}

		private void PersistCurrent()
		{
			element.PersistValue();
		}
	}
