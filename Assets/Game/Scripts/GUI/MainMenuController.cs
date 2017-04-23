using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
  public void StartGame ()
  {
    SceneManager.LoadScene(1);
  }

  public void ShowMainMenu ()
  {
    StartCoroutine (ShowMainMenuAnimated ());
    ResetCreditsTweeners ();
  }

  public void ShowCredits ()
  {
    StartCoroutine (HideMainMenu ());
    creditsStartingTweener.PlayForward ();
  }

  public void Exit ()
  {
    Application.Quit ();
  }

  private void Start ()
  {
    logoMaterials = new List<Material> ();
    MeshRenderer[] renderers = logo.GetComponentsInChildren<MeshRenderer> ();
    foreach (MeshRenderer renderer in renderers)
    {
      logoMaterials.AddRange (renderer.sharedMaterials);
    }
    ShowMainMenu ();
  }

  private void ResetCreditsTweeners ()
  {
    foreach (BaseTweener tweener in creditsTweeners)
    {
      tweener.ResetToBeginning ();
    }
  }

  private IEnumerator HideMainMenu ()
  {
    float duration = 1;
    while (duration > 0)
    {
      yield return null;
      foreach (Material material in logoMaterials)
      {
        Color color = material.color;
        color.a = duration;
        material.color = color;
        mainMenuCanvasGroup.alpha = duration;
      }
      duration -= Time.deltaTime;
    }
  }

  private IEnumerator ShowMainMenuAnimated ()
  {
    float duration = 1;
    while (duration > 0)
    {
      yield return null;
      foreach (Material material in logoMaterials)
      {
        Color color = material.color;
        color.a = 1 - duration;
        material.color = color;
        mainMenuCanvasGroup.alpha = 1 - duration;
      }
      duration -= Time.deltaTime;
    }
  }

  [SerializeField]
  private GameObject logo;

  [SerializeField]
  private TweenPosition creditsStartingTweener;

  [SerializeField]
  private BaseTweener[] creditsTweeners;

  [SerializeField]
  private CanvasGroup mainMenuCanvasGroup;

  private List<Material> logoMaterials;
}
