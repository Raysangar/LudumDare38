using UnityEngine;
using UnityEngine.UI;

public class ActionsHUDManager : MonoBehaviour {

  [System.Serializable]
  public class ActionHUDInfo
  {
    public SmartObject.ObjectType Action;
    public Sprite ActionIcon;
  }

  private void Awake ()
  {
    ActionsManager.OnPlayerPerformedAction += UpdateHUD;
    TransitionStageAnimationManager.OnHalfAnimationDone += OnStageFinished;
  }

  private void Start ()
  {
    firstActionTweenColor.ResetToBeginning ();
    secondActionTweenColor.ResetToBeginning ();
    UpdateHUD ();
  }

  private void OnStageFinished ()
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
  private ActionHUDInfo[] actionsHUDInfo;
}
