using UnityEngine;

public class WaterSmartObject : SmartObject
{
  public override void Interact()
  {
    base.Interact();
    print("Interaction With Water");
  }
}
