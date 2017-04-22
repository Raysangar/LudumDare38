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

  public void UpgradeHouse()
  {
    ((HouseSmartObject)smartObjectsByType[SmartObject.ObjectType.House]).IncreaseLevel();
  }

  public void Irrigate()
  {
    smartObjectsByType[SmartObject.ObjectType.Water].SetMaxUsage();
  }

  [SerializeField]
  private SmartObject[] smartObjects;

  private Dictionary<SmartObject.ObjectType, SmartObject> smartObjectsByType;
}
