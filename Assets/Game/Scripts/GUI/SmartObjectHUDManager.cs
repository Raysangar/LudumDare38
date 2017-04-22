using UnityEngine;
using UnityEngine.UI;

public class SmartObjectHUDManager : MonoBehaviour
{
  private void Start ()
  {
    SmartObject.OnPlayerInteraction += OnPlayerInteractedWithSmartObject;
    remainUsesAmount = target.CurrentUsage;
    UpdateHUD ();
  }

  private void OnPlayerInteractedWithSmartObject (SmartObject smartObject)
  {
    if (smartObject == target)
    {
      remainUsesAmount = smartObject.CurrentUsage;
      UpdateHUD ();
    }
  }

  private void UpdateHUD ()
  {
    usesLabel.text = remainUsesAmount + "/" + target.MaxUsage;
  }

  [SerializeField]
  private Text usesLabel;

  [SerializeField]
  private SmartObject target;

  private int remainUsesAmount;
}
