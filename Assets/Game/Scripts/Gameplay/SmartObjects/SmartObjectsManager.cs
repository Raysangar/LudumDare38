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
    smartObjectsByType[SmartObject.ObjectType.House].SetGraphicByUsage();
  }

  public void Irrigate()
  {
    smartObjectsByType[SmartObject.ObjectType.Wood].SetMaxUsage();
    smartObjectsByType[SmartObject.ObjectType.Wood].SetGraphicByUsage();
  }

  public void BuildRanch()
  {
    smartObjectsByType[SmartObject.ObjectType.Ranch].AddUsage(true);
    smartObjectsByType[SmartObject.ObjectType.Ranch].SetGraphicByUsage();
  }

  public void BuildGarden()
  {
    smartObjectsByType[SmartObject.ObjectType.Garden].AddUsage();
    smartObjectsByType[SmartObject.ObjectType.Garden].SetGraphicByUsage();
  }

  [SerializeField]
  private SmartObject[] smartObjects;

  private Dictionary<SmartObject.ObjectType, SmartObject> smartObjectsByType;
}
