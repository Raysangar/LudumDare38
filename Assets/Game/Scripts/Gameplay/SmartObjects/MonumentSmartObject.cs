using System.Collections.Generic;
using UnityEngine;

public class MonumentSmartObject : SmartObject
{
  public override void Interact()
  {
    sumByLevel = (piecesMonument.Count + 1) / MaxUsage;
    base.Interact();
    print("Monument");
  }

  public void IncreaseLevel()
  {
    level += sumByLevel;
    if (level >= (piecesMonument.Count + 1))
    {
      level = (piecesMonument.Count + 1);
      Debug.Log("You win!");
    }
  }

  public override void SetGraphicByUsage()
  {
    if (level > -1)
    {
      for (int i = 0; i < piecesMonument.Count; i++)
      {
        if (i < level)
        {
          piecesMonument[i].SetActive(true);
        }
      }
    }
  }

  [SerializeField]
  private List<GameObject> piecesMonument;

  private int sumByLevel;

  private int level = 0;
}
