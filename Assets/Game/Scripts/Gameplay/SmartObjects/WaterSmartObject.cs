using UnityEngine;

public class WaterSmartObject : SmartObject
{
  public int WaterAmount
  {
    get
    {
      return waterAmount;
    }
  }

  public override void Interact()
  {
    HoldObject.SetActive(true);
    base.Interact();
    print("Interaction With Water");
  }

  [SerializeField]
  private int waterAmount;
}
