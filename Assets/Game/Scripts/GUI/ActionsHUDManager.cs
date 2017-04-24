using UnityEngine;
using UnityEngine.UI;

public class ActionsHUDManager : MonoBehaviour
{

  [System.Serializable]
  public class ActionHUDInfo
  {
    public SmartObject.ObjectType Action;
    public Sprite ActionIcon;
  }

  private void Awake ()
  {
    ActionsManager.OnPlayerPerformedAction += UpdateHUD;
    TransitionStageAnimationManager.OnHalfAnimationDone += OnStageHalfAnimationDone;
  }

  private void OnDestroy ()
  {
    ActionsManager.OnPlayerPerformedAction -= UpdateHUD;
    TransitionStageAnimationManager.OnHalfAnimationDone -= OnStageHalfAnimationDone;
  }

  private void Start ()
  {
    if (actionFailedTweeners.Length > 0)
    {
      actionFailedTweeners[0].SetOnFinishedCallback (ResetActions);
    }
    firstActionTweenColor.ResetToBeginning ();
    secondActionTweenColor.ResetToBeginning ();
    UpdateHUD ();
  }

  private void OnStageHalfAnimationDone ()
  {
    if (GameController.Instance.LastStageFailed)
    {
      foreach (TweenColor tweener in actionFailedTweeners)
      {
        tweener.PlayForward ();
      }
    }
    else
    {
      ResetActions ();
    }
  }

  private void ResetActions ()
  {
    firstActionTweener.PlayBackwards ();
    secondActionTweener.PlayBackwards ();
    firstActionTweenColor.PlayBackwards ();
    secondActionTweenColor.PlayBackwards ();
  }

  private void UpdateHUD ()
  {
    ActionsManager.Stage stageActions = ActionsManager.Instance.ActionsMadeByPlayerOnCurrentStage;
    UpdateAction (firstActionIcon, firstActionTweenColor, firstActionTweener, stageActions.FirstAction);
    UpdateAction (secondActionIcon, secondActionTweenColor, secondActionTweener, stageActions.SecondAction);
  }

  private void UpdateAction (Image actionIcon, TweenColor tweenColor, TweenScale tweener, SmartObject action)
  {
    if (action != null)
    {
      actionIcon.enabled = true;
      tweenColor.PlayForward ();
      actionIcon.sprite = actionsHUDInfo.Find ((info) => info.Action == action.Type).ActionIcon;
      tweener.PlayForward ();
    }
    else
    {
      tweenColor.ResetToBeginning ();
      actionIcon.enabled = false;
    }
  }

  [SerializeField]
  private Image firstActionIcon;

  [SerializeField]
  private TweenScale firstActionTweener;

  [SerializeField]
  private TweenColor firstActionTweenColor;

  [SerializeField]
  private Image secondActionIcon;

  [SerializeField]
  private TweenScale secondActionTweener;

  [SerializeField]
  private TweenColor secondActionTweenColor;

  [SerializeField]
  private TweenColor[] actionFailedTweeners;

  [SerializeField]
  private ActionHUDInfo[] actionsHUDInfo;
}
