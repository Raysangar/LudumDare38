using UnityEngine;

public class RanchSmartObject : SmartObject
{
  public override void Interact()
  {
    base.Interact();
    print("Interaction With Ranch");
  }
}
