using System.Collections.Generic;
using UnityEngine;

public class SmartObjectsManager : MonoBehaviour
{
	public static System.Action OnPlayerUpgradeMonument = delegate {
	};
	public static System.Action OnPlayerUpgradeHouse = delegate {
	};
	public static System.Action OnPlayerBuildGarden = delegate {
	};
	public static System.Action OnPlayerBuildRanch = delegate {
	};
	public static System.Action OnPlayerIrrigate = delegate {
	};

	void Awake ()
	{
		smartObjectsByType = new Dictionary<SmartObject.ObjectType, SmartObject> ();
		for (int i = 0; i < smartObjects.Length; i++) {
			smartObjectsByType.Add (smartObjects [i].Type, smartObjects [i]);
		}
	}

	public void UpgradeMonument ()
	{
		if (smartObjectsByType [SmartObject.ObjectType.Wood].HoldObject.activeInHierarchy) {
			((MonumentSmartObject)smartObjectsByType [SmartObject.ObjectType.Monument]).IncreaseLevel ();
			OnPlayerUpgradeMonument ();
		} else {
			((MonumentSmartObject)smartObjectsByType [SmartObject.ObjectType.Monument]).LaunchErrorAction ();
		}
	}

	public void CheckEatingGarden ()
	{
		if (((HouseSmartObject)smartObjectsByType [SmartObject.ObjectType.House]).Level > 0) {
			PlayerManager.Instance.Eat (((GardenSmartObject)smartObjectsByType [SmartObject.ObjectType.Garden]).FoodAmount);
			((HouseSmartObject)smartObjectsByType [SmartObject.ObjectType.House]).DecreaseLevel ();
		}
	}

	public void CheckEatingRanch ()
	{
		if (((HouseSmartObject)smartObjectsByType [SmartObject.ObjectType.House]).Level > 0) {
			PlayerManager.Instance.Eat (((RanchSmartObject)smartObjectsByType [SmartObject.ObjectType.Ranch]).FoodAmount);
			((HouseSmartObject)smartObjectsByType [SmartObject.ObjectType.House]).DecreaseLevel ();
		}
	}

	public void CheckDrinkin ()
	{
		if (((HouseSmartObject)smartObjectsByType [SmartObject.ObjectType.House]).Level > 0) {
			PlayerManager.Instance.Drink (((WaterSmartObject)smartObjectsByType [SmartObject.ObjectType.Water]).WaterAmount);
			((HouseSmartObject)smartObjectsByType [SmartObject.ObjectType.House]).DecreaseLevel ();
		}

	}

	public void UpgradeHouse ()
	{
		if (smartObjectsByType [SmartObject.ObjectType.Wood].HoldObject.activeInHierarchy) {
			((HouseSmartObject)smartObjectsByType [SmartObject.ObjectType.House]).IncreaseLevel ();
			OnPlayerUpgradeHouse ();
		}
	}

	public void AddUsagesToRanch ()
	{
		if (smartObjectsByType [SmartObject.ObjectType.Ranch].CurrentUsage != -1) {
			smartObjectsByType [SmartObject.ObjectType.Ranch].SetMaxUsage ();
		} else {
			smartObjectsByType [SmartObject.ObjectType.Ranch].LaunchErrorAction ();
		}
	}

	public void Irrigate ()
	{
		smartObjectsByType [SmartObject.ObjectType.Wood].SetMaxUsage ();
		OnPlayerIrrigate ();
	}

	public void BuildRanch ()
	{
		if (smartObjectsByType [SmartObject.ObjectType.Ranch].CurrentUsage == -1) {
			smartObjectsByType [SmartObject.ObjectType.Ranch].SetMaxUsage ();
			OnPlayerBuildRanch ();
		} else {
			smartObjectsByType [SmartObject.ObjectType.Ranch].LaunchErrorAction ();
		}
	}

	public void BuildGarden ()
	{
		smartObjectsByType [SmartObject.ObjectType.Garden].SetMaxUsage ();
		OnPlayerBuildGarden ();
	}

	[SerializeField]
	private SmartObject[] smartObjects;

	private Dictionary<SmartObject.ObjectType, SmartObject> smartObjectsByType;
}
