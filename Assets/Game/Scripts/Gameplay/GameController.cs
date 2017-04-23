using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

  public static System.Action OnActionErrated = delegate { };

  private void Awake ()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    PlayerManager.OnPlayerDied += PauseGameplay;
  }

  private void OnStageFinished (ActionsManager.Stage stage)
  {
    int i = 0;
    while (i < playerActionsConsequences.Length && !playerActionsConsequences[i].StageMatchesActions(stage))
    {
      ++i;
    }
    if (i < playerActionsConsequences.Length)
    {
      playerActionsConsequences[i].Execute();
    }
    else
    {
      OnActionErrated();
    }
    PlayerManager.Instance.StageFinished ();
  }

  private void PauseGameplay ()
  {
    pointAndClick.enabled = false;
  }

  [SerializeField]
  private ActionsConsequence[] playerActionsConsequences;

  [SerializeField]
  private PointAndClickManager pointAndClick;
}
