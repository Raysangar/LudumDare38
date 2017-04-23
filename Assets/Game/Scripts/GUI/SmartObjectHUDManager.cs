using UnityEngine;
using UnityEngine.UI;

public class SmartObjectHUDManager : MonoBehaviour
{
  private void Start ()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    TransitionStageAnimationManager.OnHalfAnimationDone += UpdateSmartObjectInfo;
    remainUsesAmount = target.CurrentUsage;
    foreach (BaseTweener tweener in usesModifiedTweeners)
    {
      tweener.ResetToBeginning ();
    }
    UpdateHUD ();
  }

  private void UpdateSmartObjectInfo ()
  {
    if (target.CurrentUsage != remainUsesAmount)
    {
      if (remainUsesAmount < 0)
      {
        remainUsesAmount = 0;
      }
      if (target.CurrentUsage == -1)
      {
        remainUsesAmount -= 1;
      }
      modifiedUsesLabel.text = (target.CurrentUsage - remainUsesAmount).ToString("+#;-#;0");
      modifiedUsesLabel.color = (remainUsesAmount > target.CurrentUsage) ? removedUsesColor : addedUsesColor;
      foreach(BaseTweener tweener in usesModifiedTweeners)
      {
        tweener.PlayForward ();
      }
      remainUsesAmount = target.CurrentUsage;
      UpdateHUD ();
    }
  }

  private void OnStageFinished (ActionsManager.Stage stage)
  {
    stageFinishedTweener.PlayForward ();
  }

  private void UpdateHUD ()
  {
    usesLabel.text = (remainUsesAmount == -1) ? "0/0" : remainUsesAmount + "/" + target.MaxUsage;
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
