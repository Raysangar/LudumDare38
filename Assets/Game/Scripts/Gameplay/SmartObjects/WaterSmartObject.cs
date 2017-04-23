using UnityEngine;

public class WaterSmartObject : SmartObject
{
  public override void Interact()
  {
    base.Interact();
    HoldObject.SetActive(true);
    print("Interaction With Water");
  }
}
