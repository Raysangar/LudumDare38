using UnityEngine;

[System.Serializable]
public class ActionsConsequence
{
  public bool StageMatchesActions (ActionsManager.Stage stage)
  {
    return (stage.FirstAction.Type == firstAction && stage.SecondAction.Type == secondAction) ||
      (stage.FirstAction.Type == secondAction && stage.SecondAction.Type == firstAction);
  }

  public void Execute ()
  {
    consequence.Invoke();
  }

  [SerializeField]
  private SmartObject.ObjectType firstAction;

  [SerializeField]
  private SmartObject.ObjectType secondAction;

  [SerializeField]
  private UnityEngine.Events.UnityEvent consequence;
}
