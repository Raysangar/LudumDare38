using UnityEngine;

public class GameController : MonoBehaviour {

  public static System.Action<SmartObject.ObjectType> OnActionErrated = delegate { };

  public static GameController Instance
  {
    get { return instance; }
  }

  public bool LastStageFailed
  {
    get
    {
      return HasStageFailed (ActionsManager.Instance.ActionsMadeByPlayerOnLastStage);
    }
  }

  public bool HasStageFailed (ActionsManager.Stage stage)
  {
    int i = 0;
    while (i < playerActionsConsequences.Length && !playerActionsConsequences[i].StageMatchesActions (stage))
    {
      ++i;
    }
    return (i == playerActionsConsequences.Length);
  }

  private void Awake ()
  {
    instance = this;
    ActionsManager.OnStageFinished += OnStageFinished;
    PlayerManager.OnPlayerDied += PauseGameplay;
    GUIGameplayManager.OnPause += PauseGameplay;
    GUIGameplayManager.OnResume += ResumeGameplay;
  }

  private void OnDestroy ()
  {
    ActionsManager.OnStageFinished -= OnStageFinished;
    PlayerManager.OnPlayerDied -= PauseGameplay;
    GUIGameplayManager.OnPause -= PauseGameplay;
    GUIGameplayManager.OnResume -= ResumeGameplay;
  }

  private void OnStageFinished (ActionsManager.Stage stage)
  {
    int i = 0;
    while (i < playerActionsConsequences.Length && !playerActionsConsequences[i].StageMatchesActions (stage))
    {
      ++i;
    }
    if (i < playerActionsConsequences.Length)
    {
      playerActionsConsequences[i].Execute();
    }
    else
    {
      OnActionErrated(stage.FirstAction.Type);
    }
    PlayerManager.Instance.StageFinished ();
  }

  private void PauseGameplay ()
  {
    pointAndClick.enabled = false;
  }

  private void ResumeGameplay ()
  {
    pointAndClick.enabled = true;
  }

  [SerializeField]
  private ActionsConsequence[] playerActionsConsequences;

  [SerializeField]
  private PointAndClickManager pointAndClick;

  private static GameController instance;
}
