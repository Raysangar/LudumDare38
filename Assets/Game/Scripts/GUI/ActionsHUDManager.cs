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
  }

  private void UpdateHUD ()
  {
    ActionsManager.Stage stageActions = ActionsManager.Instance.ActionsMadeByPlayerOnCurrentStage;
    UpdateAction (firstAction, firstActionIcon, stageActions.FirstAction);
    UpdateAction (secondAction, secondActionIcon, stageActions.SecondAction);
  }

  private void UpdateAction (GameObject actionHUD, Image actionIcon, SmartObject action)
  {
    actionHUD.SetActive (action != null);
    if (action != null)
    {
      firstActionIcon.sprite = actionsHUDInfo.Find ((info) => info.Action == action.Type).ActionIcon;
    }
  }

  [SerializeField]
  private GameObject firstAction;

  [SerializeField]
  private Image firstActionIcon;

  [SerializeField]
  private GameObject secondAction;

  [SerializeField]
  private Image secondActionIcon;

  [SerializeField]
  private ActionHUDInfo[] actionsHUDInfo;
}
