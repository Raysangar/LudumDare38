using UnityEngine;

public class WoodSmartObject : SmartObject
{

  public override void Interact()
  {
    base.Interact();
    print("Interaction With Wood");
  }
}
