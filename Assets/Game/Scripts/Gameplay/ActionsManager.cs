using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour {

  public bool IsSmartObjectAlreadyUsedOnCurrentStage (SmartObject smartObject)
  {
    return actionsMadeByPlayerOnCurrentStage.Contains (smartObject);
  }

  public List<SmartObject> ActionsMadeByPlayerOnCurrentStage
  {
    get
    {
      return actionsMadeByPlayerOnCurrentStage;
    }
  }

  public List<List<SmartObject>> ActionsMadeByPlayerOnPreviousStages
  {
    get
    {
      return actionsMadeByPlayerOnPreviousStages;
    }
  }

  private void Awake ()
  {
    actionsMadeByPlayerOnCurrentStage = new List<SmartObject> ();
    actionsMadeByPlayerOnPreviousStages = new List<List<SmartObject>> ();
    SmartObject.OnPlayerInteraction += OnPlayerInteractedWithSmartObject;
    GameController.OnStageChanged += OnStageChanged;
  }

  private void OnPlayerInteractedWithSmartObject (SmartObject smartObject)
  {
    actionsMadeByPlayerOnCurrentStage.Add (smartObject);
  }

  private void OnStageChanged ()
  {
    actionsMadeByPlayerOnPreviousStages.Add (actionsMadeByPlayerOnCurrentStage);
    actionsMadeByPlayerOnCurrentStage = new List<SmartObject> ();
  }

  private List<SmartObject> actionsMadeByPlayerOnCurrentStage;
  private List<List<SmartObject>> actionsMadeByPlayerOnPreviousStages;
}
