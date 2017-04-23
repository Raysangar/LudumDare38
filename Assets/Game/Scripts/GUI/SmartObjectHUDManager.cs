using UnityEngine;
using UnityEngine.UI;

public class SmartObjectHUDManager : MonoBehaviour
{
  private void Start ()
  {
    SmartObject.OnPlayerInteraction += OnPlayerInteractedWithSmartObject;
    ActionsManager.OnStageFinished += OnStageFinished;
    remainUsesAmount = target.CurrentUsage;
    foreach (BaseTweener tweener in usesModifiedTweeners)
    {
      tweener.ResetToBeginning ();
    }
    UpdateHUD ();
  }

  private void OnPlayerInteractedWithSmartObject (SmartObject smartObject)
  {
    if (smartObject == target && smartObject.CurrentUsage != remainUsesAmount)
    {
      modifiedUsesLabel.text = (smartObject.CurrentUsage - remainUsesAmount).ToString("+#;-#;0");
      modifiedUsesLabel.color = (remainUsesAmount > smartObject.CurrentUsage) ? removedUsesColor : addedUsesColor;
      foreach(BaseTweener tweener in usesModifiedTweeners)
      {
        tweener.PlayForward ();
      }
      remainUsesAmount = smartObject.CurrentUsage;
      UpdateHUD ();
    }
  }

  private void OnStageFinished (ActionsManager.Stage stage)
  {
    stageFinishedTweener.PlayForward ();
  }

  private void UpdateHUD ()
  {
    usesLabel.text = remainUsesAmount + "/" + target.MaxUsage;
  }

  [SerializeField]
  private Text usesLabel;

  [SerializeField]
  private Text modifiedUsesLabel;

  [SerializeField]
  private SmartObject target;

  [SerializeField]
  private BaseTweener[] usesModifiedTweeners;

  [SerializeField]
  private TweenScale stageFinishedTweener;

  [SerializeField]
  private Color addedUsesColor = Color.green;

  [SerializeField]
  private Color removedUsesColor = Color.red;

  private int remainUsesAmount;
}
