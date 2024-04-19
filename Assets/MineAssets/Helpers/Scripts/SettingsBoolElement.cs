using System;
using MineAssets.Helpers.Scripts;
using UnityEngine;

	[CreateAssetMenu(menuName = "Elements/SettingsBoolElement")]
	public class SettingsBoolElement : ScriptableObject
	{
		public bool currentValue;
		public Sprite onImage;
		public Sprite offImage;
		public SettingsElementType type;

		private void OnEnable()
		{
			currentValue = PlayerPrefs.GetInt(name, 1) == 1;
		}

		public void InvertValue()
		{
			currentValue = !currentValue;
			SendCurrentEvent();
		}

		public void SendCurrentEvent()
		{
			if (currentValue)
			{
				EventManager2.Instance.EnableSettingsElement(type);
			}
			else
			{
				EventManager2.Instance.DisableSettingsElement(type);
			}
		}

		public Sprite GetSprite()
		{
			return currentValue ? onImage : offImage;
		}

		public void PersistValue()
		{
			PlayerPrefs.SetInt(name, Convert.ToInt32(currentValue));
			PlayerPrefs.Save();
		}
	}

	public enum SettingsElementType
	{
		Sound,
		Vibration
	}
