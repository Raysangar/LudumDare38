using UnityEngine;

public class HouseSmartObject : SmartObject
{
  public int Level
  {
    get { return level; }
  }

  public override void Interact()
  {
    base.Interact();
    print("Interaction With House");
  }

  private int level;
}
