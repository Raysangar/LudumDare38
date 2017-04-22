using UnityEngine;

public class GardenSmartObject : SmartObject
{
  public override void Interact()
  {
    base.Interact();
    print("Interaction With Garden");
  }
}
