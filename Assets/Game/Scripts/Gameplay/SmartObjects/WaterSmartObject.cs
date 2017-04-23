using UnityEngine;

public class WaterSmartObject : SmartObject
{
  public int FoodAmount
  {
    get
    {
      return foodAmount;
    }
  }

  public override void Interact()
  {
    HoldObject.SetActive(true);
    base.Interact();
    print("Interaction With Water");
  }

  [SerializeField]
  private int foodAmount;
}
