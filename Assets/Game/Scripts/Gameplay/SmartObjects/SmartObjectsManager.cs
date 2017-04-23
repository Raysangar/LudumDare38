using System.Collections.Generic;
using UnityEngine;

public class SmartObjectsManager : MonoBehaviour
{
  void Awake()
  {
    smartObjectsByType = new Dictionary<SmartObject.ObjectType, SmartObject>();
    for (int i = 0; i < smartObjects.Length; i++)
    {
      smartObjectsByType.Add(smartObjects[i].Type, smartObjects[i]);
    }
  }

  public void UpgradeMonument()
  {
    if (smartObjectsByType[SmartObject.ObjectType.Wood].CurrentUsage > 0)
    {
      ((MonumentSmartObject)smartObjectsByType[SmartObject.ObjectType.Monument]).IncreaseLevel();
    }
  }

  public void UpgradeHouse()
  {
    ((HouseSmartObject)smartObjectsByType[SmartObject.ObjectType.House]).IncreaseLevel();
  }

  public void AddUsagesToRanch()
  {
    if (smartObjectsByType[SmartObject.ObjectType.Ranch].CurrentUsage != -1)
    {
      smartObjectsByType[SmartObject.ObjectType.Ranch].SetMaxUsage();
    }
  }

  public void Irrigate()
  {
    smartObjectsByType[SmartObject.ObjectType.Wood].SetMaxUsage();
  }

  public void BuildRanch()
  {
    if (smartObjectsByType[SmartObject.ObjectType.Ranch].CurrentUsage == -1)
    {
      smartObjectsByType[SmartObject.ObjectType.Ranch].SetMaxUsage();
    }
  }

  public void BuildGarden()
  {
    smartObjectsByType[SmartObject.ObjectType.Garden].SetMaxUsage();
  }

  [SerializeField]
  private SmartObject[] smartObjects;

  private Dictionary<SmartObject.ObjectType, SmartObject> smartObjectsByType;
}
