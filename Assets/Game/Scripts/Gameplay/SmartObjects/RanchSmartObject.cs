using UnityEngine;

public class RanchSmartObject : SmartObject
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
    if (CurrentUsage > 0)
    {
      HoldObject.SetActive(true);
      SpendUsage();
      if (CurrentUsage == 0)
      {
        BreakObject();
      }
    }
    base.Interact();
    print("Interaction With Ranch");
  }

  public override void SetGraphicByUsage()
  {
    base.SetGraphicByUsage();
    switch (CurrentUsage)
    {
      case -1:
        stage0.SetActive(false);
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        break;
      case 0:
        stage0.SetActive(true);
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        break;
      case 1:
        stage0.SetActive(true);
        stage1.SetActive(true);
        stage2.SetActive(false);
        stage3.SetActive(false);
        break;
      case 2:
        stage0.SetActive(true);
        stage1.SetActive(true);
        stage2.SetActive(true);
        stage3.SetActive(false);
        break;
      case 3:
        stage0.SetActive(true);
        stage1.SetActive(true);
        stage2.SetActive(true);
        stage3.SetActive(true);
        break;
      default:
        stage0.SetActive(false);
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        break;
    }
  }

  [SerializeField]
  private GameObject stage0;
  [SerializeField]
  private GameObject stage1;
  [SerializeField]
  private GameObject stage2;
  [SerializeField]
  private GameObject stage3;

  [SerializeField]
  private int foodAmount;
}
