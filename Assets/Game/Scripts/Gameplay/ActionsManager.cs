using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
  public static System.Action<Stage> OnStageFinished = delegate { };
  public static System.Action OnPlayerPerformedAction = delegate{ };

  public class Stage
  {
    public SmartObject FirstAction;
    public SmartObject SecondAction;
  }

  public static ActionsManager Instance
  {
    get { return instance; }
  }

  public bool IsSmartObjectInteractableOnCurrentStage (SmartObject smartObject)
  {
    return (actionsMadeByPlayerOnCurrentStage.FirstAction != null && actionsMadeByPlayerOnCurrentStage.FirstAction != smartObject) ||
      (actionsMadeByPlayerOnCurrentStage.FirstAction == null && smartObject.Type != SmartObject.ObjectType.House);
  }

  public Stage ActionsMadeByPlayerOnCurrentStage
  {
    get
    {
      return actionsMadeByPlayerOnCurrentStage;
    }
  }

  public Stage ActionsMadeByPlayerOnLastStage
  {
    get { return actionsMadeByPlayerOnPreviousStages[actionsMadeByPlayerOnPreviousStages.Count - 1]; }
  }

  public List<Stage> ActionsMadeByPlayerOnPreviousStages
  {
    get
    {
      return actionsMadeByPlayerOnPreviousStages;
    }
  }

  private void Awake ()
  {
    instance = this;
    actionsMadeByPlayerOnCurrentStage = new Stage ();
    actionsMadeByPlayerOnPreviousStages = new List<Stage> ();
    SmartObject.OnPlayerInteraction += OnPlayerInteractedWithSmartObject;
  }

  private void OnPlayerInteractedWithSmartObject (SmartObject smartObject)
  {
    if (actionsMadeByPlayerOnCurrentStage.FirstAction == null)
    {
      actionsMadeByPlayerOnCurrentStage.FirstAction = smartObject;
      OnPlayerPerformedAction ();
    }
    else
    {
      actionsMadeByPlayerOnCurrentStage.SecondAction = smartObject;
      actionsMadeByPlayerOnPreviousStages.Add (actionsMadeByPlayerOnCurrentStage);
      actionsMadeByPlayerOnCurrentStage = new Stage ();
      OnPlayerPerformedAction ();
      OnStageFinished (ActionsMadeByPlayerOnLastStage);
    }
  }

  private Stage actionsMadeByPlayerOnCurrentStage;
  private List<Stage> actionsMadeByPlayerOnPreviousStages;

  private static ActionsManager instance;
}
