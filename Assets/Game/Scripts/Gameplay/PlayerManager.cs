using UnityEngine;

public class PlayerManager : MonoBehaviour {

  public static System.Action OnPlayerDied = delegate { };

  public static PlayerManager Instance
  {
    get { return instance; }
  }

  public int CurrentFood
  {
    get
    {
      return currentFood;
    }
  }

  public int CurrentWater
  {
    get
    {
      return currentWater;
    }
  }

  public void Drink (int waterAmount)
  {
    currentWater += waterAmount;
  }

  public void Eat (int foodAmount, int waterAmount)
  {
    currentFood += foodAmount;
    currentWater += waterAmount;
  }

  public void StageFinished ()
  {
    currentFood -= foodLostEveryStage;
    currentWater -= waterLostEveryStage;

    if (currentFood < 0) { currentFood = 0; }
    if (currentWater < 0) { currentWater = 0; }
    
    if (currentFood == 0 || currentWater == 0)
    {
      OnPlayerDied ();
    }
  }

  private void Awake ()
  {
    instance = this;
    currentFood = initialFood;
    currentWater = initialWater;
  }

  [SerializeField]
  private int initialFood;

  [SerializeField]
  private int initialWater;

  [SerializeField]
  private int foodLostEveryStage;

  [SerializeField]
  private int waterLostEveryStage;

  private int currentFood;
  private int currentWater;

  private static PlayerManager instance;
}
