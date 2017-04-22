using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour {

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
    foodLabel.text = foodAmount + "/" + PlayerManager.Instance.MaxFood;
    waterLabel.text = waterAmount + "/" + PlayerManager.Instance.MaxWater;
  }

  [SerializeField]
  private Text foodLabel;

  [SerializeField]
  private Text waterLabel;

  private int foodAmount;
  private int waterAmount;
}
