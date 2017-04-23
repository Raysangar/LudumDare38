using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

  public void BackToMainMenu ()
  {
    SceneManager.LoadScene (0);
  }

  private void Awake ()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    PlayerManager.OnPlayerDied += OnPlayerDied;
  }

  private void Start ()
  {
    gameOverTweener.ResetToBeginning ();
    gameOverTweener.SetOnFinishedCallback (OnGameOverTweenerFinished);
    continueButton.SetActive (false);
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
      playerActionsConsequences[i].Execute ();
    }
    PlayerManager.Instance.StageFinished ();
  }

  private void OnPlayerDied ()
  {
    pointAndClick.enabled = false;
    StartCoroutine (GameOver ());
  }

  private IEnumerator GameOver ()
  {
    result.text = string.Format ("You survived {0} days", ActionsManager.Instance.ActionsMadeByPlayerOnPreviousStages.Count);
    yield return new WaitForSeconds (2);
    gameOverTweener.PlayForward ();
  }

  private void OnGameOverTweenerFinished ()
  {
    continueButton.SetActive (true);
  }

  [SerializeField]
  private ActionsConsequence[] playerActionsConsequences;

  [SerializeField]
  private PointAndClickManager pointAndClick;

  [SerializeField]
  private Text result;

  [SerializeField]
  private TweenAlpha gameOverTweener;

  [SerializeField]
  private GameObject continueButton;
}
