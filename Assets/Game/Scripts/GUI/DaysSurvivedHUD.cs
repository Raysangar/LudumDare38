using UnityEngine;
using UnityEngine.UI;

public class DaysSurvivedHUD : MonoBehaviour {

  private void Start ()
  {
    TransitionStageAnimationManager.OnAnimationDone += ChangeDays;
    days.text = "0";
  }

  private void OnDestroy ()
  {
    TransitionStageAnimationManager.OnAnimationDone -= ChangeDays;
  }

  private void ChangeDays ()
  {
    days.text = ActionsManager.Instance.ActionsMadeByPlayerOnPreviousStages.Count.ToString();
  }

  [SerializeField]
  private Text days;
}
