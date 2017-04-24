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

  public void StartPlaying ()
  {
    StartCoroutine (HideTutorial ());
  }

  private void Awake ()
  {
    PlayerManager.OnPlayerDied += OnPlayerDied;
  }

  private void OnDestroy ()
  {
    PlayerManager.OnPlayerDied -= OnPlayerDied;
  }

  private void Start ()
  {
    gameOverTweener.ResetToBeginning ();
    gameOverTweener.SetOnFinishedCallback (OnGameOverTweenerFinished);
    continueButton.SetActive (false);
    pausePanel.SetActive (false);
    StartCoroutine (ShowTutorial ());

#if !UNITY_STANDALONE && !UNITY_EDITOR
    pauseExitButton.SetActive (false);
#endif
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

  private IEnumerator ShowTutorial ()
  {
    tutorialCanvasGroup.interactable = true;
    tutorialCanvasGroup.blocksRaycasts = true;
    GameController.Instance.PauseGameplay ();
    float time = 0;
    while (time < 1)
    {
      tutorialCanvasGroup.alpha = time;
      yield return null;
      time += Time.deltaTime;
    }
  }

  private IEnumerator HideTutorial ()
  {
    float duration = 1;
    while (duration > 0)
    {
      tutorialCanvasGroup.alpha = duration;
      yield return null;
      duration -= Time.deltaTime;
    }
    tutorialCanvasGroup.interactable = false;
    tutorialCanvasGroup.blocksRaycasts = false;
    GameController.Instance.ResumeGameplay ();
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

  [SerializeField]
  private CanvasGroup tutorialCanvasGroup;

  [SerializeField]
  private GameObject pauseExitButton;
}
