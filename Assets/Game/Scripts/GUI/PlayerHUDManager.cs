using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour {

  private void Start ()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    foodAmount = PlayerManager.Instance.CurrentFood;
    UpdateHUD ();
    foreach (BaseTweener tweener in foodDifferenceTweeners)
    {
      tweener.ResetToBeginning ();
    }
  }

  private void OnStageFinished (ActionsManager.Stage stage)
  {
    if (foodAmount != PlayerManager.Instance.CurrentFood)
    {
      SetDifferenceTextAnimation (PlayerManager.Instance.CurrentFood - foodAmount, foodDifferenceLabel, foodDifferenceTweeners);
    }
    foodAmount = PlayerManager.Instance.CurrentFood;
    UpdateHUD ();
  }

  private void SetDifferenceTextAnimation (int difference, Text text, BaseTweener[] tweeners)
  {
    text.color = (difference < 0) ? negativeDifferenceColor : positiveDifferenceColor;
    text.text = difference.ToString ("+#;-#;0");
    foreach (BaseTweener tweener in tweeners)
    {
      tweener.PlayForward ();
    }
  }

  private void UpdateHUD ()
  {
    foodLabel.text = foodAmount + "/" + PlayerManager.Instance.MaxFood;
  }

  [SerializeField]
  private Text foodLabel;

  [SerializeField]
  private Text foodDifferenceLabel;

  [SerializeField]
  private Color positiveDifferenceColor = Color.green;

  [SerializeField]
  private Color negativeDifferenceColor = Color.red;

  [SerializeField]
  private BaseTweener[] foodDifferenceTweeners;

  private int foodAmount;
}
