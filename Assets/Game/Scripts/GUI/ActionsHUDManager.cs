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
    OnStageFinished ();
  }

  private void OnStageFinished ()
  {
    firstActionTweener.PlayBackwards ();
    secondActionTweener.PlayBackwards ();
  }

  private void UpdateHUD ()
  {
    ActionsManager.Stage stageActions = ActionsManager.Instance.ActionsMadeByPlayerOnCurrentStage;
    UpdateAction (firstActionIcon, firstActionTweener, stageActions.FirstAction);
    UpdateAction (secondActionIcon, secondActionTweener, stageActions.SecondAction);
  }

  private void UpdateAction (Image actionIcon, TweenScale tweener, SmartObject action)
  {
    if (action != null)
    {
      actionIcon.enabled = true;
      actionIcon.sprite = actionsHUDInfo.Find ((info) => info.Action == action.Type).ActionIcon;
      tweener.PlayForward ();
    }
    else
    {
      actionIcon.enabled = false;
    }
  }

  [SerializeField]
  private Image firstActionIcon;

  [SerializeField]
  private TweenScale firstActionTweener;

  [SerializeField]
  private Image secondActionIcon;

  [SerializeField]
  private TweenScale secondActionTweener;

  [SerializeField]
  private ActionHUDInfo[] actionsHUDInfo;
}
