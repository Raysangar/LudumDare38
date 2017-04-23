using UnityEngine;
using UnityEngine.UI;

public class SmartObjectHUDManager : MonoBehaviour
{
  private void Start ()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    stageFinishedTweener.SetOnFinishedCallback (UpdateSmartObjectInfo);
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
