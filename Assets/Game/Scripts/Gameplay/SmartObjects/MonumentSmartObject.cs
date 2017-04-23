using System.Collections.Generic;
using UnityEngine;

public class MonumentSmartObject : SmartObject
{
  public override void Interact()
  {
    base.Interact();
    print("Monument");
  }

  public void IncreaseLevel()
  {
    level++;
    if (level == MaxUsage)
    {
      Debug.Log("You win!");
    }
  }

  public override void SetGraphicByUsage()
  {
    if (level > -1)
    {
      piecesMonument[level].SetActive(true);
    }
  }

  [SerializeField]
  private List<GameObject> piecesMonument;

  private int level = -1;
}
