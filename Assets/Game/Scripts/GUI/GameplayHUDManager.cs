using UnityEngine;
using UnityEngine.UI;

public class GameplayHUDManager : MonoBehaviour {

  private void Start ()
  {
    foodAmount = PlayerManager.Instance.CurrentFood;
    waterAmount = PlayerManager.Instance.CurrentWater;
    UpdateHUD ();
  }

  private void OnStageStarted ()
  {
    foodAmount = PlayerManager.Instance.CurrentFood;
    waterAmount = PlayerManager.Instance.CurrentWater;
    UpdateHUD ();
  }

  private void UpdateHUD ()
  {
    foodLabel.text = foodAmount.ToString ();
    waterLabel.text = waterAmount.ToString ();
  }

  [SerializeField]
  private Text foodLabel;

  [SerializeField]
  private Text waterLabel;

  private int foodAmount;
  private int waterAmount;
}
