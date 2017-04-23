using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GUIGameplayManager : MonoBehaviour
{
  public static System.Action OnPause = delegate { };
  public static System.Action OnResume = delegate { };

  public void BackToMainMenu ()
  {
    SceneManager.LoadScene (0);
  }

  public void Exit ()
  {
    Application.Quit ();
  } 

  public void ResumeGameplay ()
  {
    pausePanel.SetActive (false);
    OnResume ();
  }

  public void PauseGameplay ()
  {
    pausePanel.SetActive (true);
    OnPause ();
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
    pausePanel.SetActive (false);
  }

  private void OnStageFinished (ActionsManager.Stage stage)
  {
  }

  private void OnPlayerDied ()
  {
    StartCoroutine (GameOver ());
  }

  private IEnumerator GameOver ()
  {
    pauseButton.enabled = false;
    result.text = string.Format ("You survived {0} days", ActionsManager.Instance.ActionsMadeByPlayerOnPreviousStages.Count);
    yield return new WaitForSeconds (2);
    gameOverTweener.PlayForward ();
  }

  private void OnGameOverTweenerFinished ()
  {
    continueButton.SetActive (true);
  }

  [SerializeField]
  private Text result;

  [SerializeField]
  private TweenAlpha gameOverTweener;

  [SerializeField]
  private GameObject continueButton;

  [SerializeField]
  private GameObject pausePanel;

  [SerializeField]
  private Button pauseButton;
}
