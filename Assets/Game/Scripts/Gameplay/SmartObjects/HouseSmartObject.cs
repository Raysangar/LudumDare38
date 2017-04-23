using UnityEngine;

public class HouseSmartObject : SmartObject
{
  public int Level
  {
    get { return level; }
  }

  public override int CurrentUsage
  {
    get
    {
      return level;
    }
  }

  public override void Interact()
  {
    base.Interact();
    print("Interaction With House");
  }

  public void IncreaseLevel()
  {
    level++;
  }

  public void DecreaseLevel()
  {
    level--;
    if (level < 0)
    {
      level = 0;
    }
  }

  public override void SetGraphicByUsage()
  {
    base.SetGraphicByUsage();
    switch (Level)
    {
      case 0:
        stage0.SetActive(true);
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        break;
      case 1:
        stage0.SetActive(false);
        stage1.SetActive(true);
        stage2.SetActive(false);
        stage3.SetActive(false);
        break;
      case 2:
        stage0.SetActive(false);
        stage1.SetActive(true);
        stage2.SetActive(true);
        stage3.SetActive(false);
        break;
      case 3:
        stage0.SetActive(false);
        stage1.SetActive(true);
        stage2.SetActive(true);
        stage3.SetActive(true);
        break;
      default:
        stage0.SetActive(false);
        stage1.SetActive(true);
        stage2.SetActive(true);
        stage3.SetActive(true);
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

  private int level = 0;

}
