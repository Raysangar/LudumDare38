﻿using UnityEngine;

public class WoodSmartObject : SmartObject
{
  public void Awake()
  {
    SetMaxUsage();
  }

  public override void Interact()
  {
    if (CurrentUsage != -1)
    {
      HoldObject.SetActive(true);
      SpendUsage();
      if (CurrentUsage == 0)
      {
        BreakObject();
      }
    }
    base.Interact();
    print("Interaction With Wood");
  }

  public override void SetGraphicByUsage()
  {
    base.SetGraphicByUsage();
    switch (CurrentUsage)
    {
      case -1:
        stage0.SetActive(true);
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
      case 4:
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
}
