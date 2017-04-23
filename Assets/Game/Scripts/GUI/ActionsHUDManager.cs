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
    UpdateHUD ();
  }

  private void OnStageFinished ()
  {
    firstActionTweener.PlayBackwards ();
    secondActionTweener.PlayBackwards ();
    firstActionBackground.color = pendingActionBackground;
    secondActionBackground.color = pendingActionBackground;
  }

  private void UpdateHUD ()
  {
    ActionsManager.Stage stageActions = ActionsManager.Instance.ActionsMadeByPlayerOnCurrentStage;
    UpdateAction (firstActionIcon, firstActionBackground, firstActionTweener, stageActions.FirstAction);
    UpdateAction (secondActionIcon, secondActionBackground, secondActionTweener, stageActions.SecondAction);
  }

  private void UpdateAction (Image actionIcon, Image actionBackground, TweenScale tweener, SmartObject action)
  {
    if (action != null)
    {
      actionIcon.enabled = true;
      actionBackground.color = actionPerformedBacground;
      actionIcon.sprite = actionsHUDInfo.Find ((info) => info.Action == action.Type).ActionIcon;
      tweener.PlayForward ();
    }
    else
    {
      actionBackground.color = pendingActionBackground;
      actionIcon.enabled = false;
    }
  }

  [SerializeField]
  private Image firstActionIcon;

  [SerializeField]
  private TweenScale firstActionTweener;

  [SerializeField]
  private Image firstActionBackground;

  [SerializeField]
  private Image secondActionIcon;

  [SerializeField]
  private TweenScale secondActionTweener;

  [SerializeField]
  private Image secondActionBackground;

  [SerializeField]
  private ActionHUDInfo[] actionsHUDInfo;

  [SerializeField]
  private Color actionPerformedBacground;

  [SerializeField]
  private Color pendingActionBackground;
}
