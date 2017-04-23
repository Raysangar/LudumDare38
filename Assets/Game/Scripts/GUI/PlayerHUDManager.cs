using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour {

  private void Start ()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    foodAmount = PlayerManager.Instance.CurrentFood;
    waterAmount = PlayerManager.Instance.CurrentWater;
    UpdateHUD ();
    foreach (BaseTweener tweener in foodDifferenceTweeners)
    {
      tweener.ResetToBeginning ();
    }
    foreach (BaseTweener tweener in waterDifferenceTweeners)
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
    if (waterAmount != PlayerManager.Instance.CurrentWater)
    {
      SetDifferenceTextAnimation (PlayerManager.Instance.CurrentWater - waterAmount, waterDifferenceLabel, waterDifferenceTweeners);
    }
    foodAmount = PlayerManager.Instance.CurrentFood;
    waterAmount = PlayerManager.Instance.CurrentWater;
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
    waterLabel.text = waterAmount + "/" + PlayerManager.Instance.MaxWater;
  }

  [SerializeField]
  private Text foodLabel;

  [SerializeField]
  private Text waterLabel;

  [SerializeField]
  private Text foodDifferenceLabel;

  [SerializeField]
  private Text waterDifferenceLabel;

  [SerializeField]
  private Color positiveDifferenceColor = Color.green;

  [SerializeField]
  private Color negativeDifferenceColor = Color.red;

  [SerializeField]
  private BaseTweener[] foodDifferenceTweeners;

  [SerializeField]
  private BaseTweener[] waterDifferenceTweeners;

  private int foodAmount;
  private int waterAmount;
}
