using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

  private void Awake ()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    PlayerManager.OnPlayerDied += OnPlayerDied;
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
    print ("Player died");
    StartCoroutine (GameOver ());
  }

  private IEnumerator GameOver ()
  {
    yield return new WaitForSeconds (5);
    SceneManager.LoadScene (0);
  }

  [SerializeField]
  private ActionsConsequence[] playerActionsConsequences;
}
