using UnityEngine;

	[CreateAssetMenu(menuName = "Elements/UpgradeElement")]
	public class UpgradeElement : ScriptableObject
	{

		public string displayName;
		public float elementValue;
		public float elementOriginValue;
		public float elementIncreaseRatio;
		public bool isFixedIncrease;
		public int upgradeCost;
		public int upgradeCostOriginValue;
		public float costIncreaseRatio;
		public int level;

		private void OnEnable()
		{
			upgradeCost = PlayerPrefs.GetInt(name + "Cost", upgradeCostOriginValue);
			level = PlayerPrefs.GetInt(name + "Level", 1);
		}

		public void Upgrade()
		{
			elementValue += isFixedIncrease
				? elementOriginValue * elementIncreaseRatio
				: elementValue * elementIncreaseRatio;
			var cost = upgradeCost * costIncreaseRatio;
			upgradeCost += Mathf.RoundToInt(cost);
			level++;
			PersistValues();
		}

		private void PersistValues()
		{
			PlayerPrefs.SetFloat(name, elementValue);
			PlayerPrefs.SetInt(name + "Cost", upgradeCost);
			PlayerPrefs.SetInt(name + "Level", level);
			PlayerPrefs.Save();
		}
	}
