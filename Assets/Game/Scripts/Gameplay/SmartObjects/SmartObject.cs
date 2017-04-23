using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class SmartObject : MonoBehaviour
{
  public static System.Action<SmartObject> OnPlayerInteraction = delegate { };

  public enum ObjectType
  {
    Wood, Water, House, Garden, Ranch, Monument
  }

  public Transform SmartPosition
  {
    get { return smartPosition; }
  }

  public Transform MeshTransform
  {
    get { return meshTransform; }
  }

  public ObjectType Type
  {
    get { return type; }
  }

  public int MaxUsage
  {
    get { return maxUsage; }
  }

  public virtual int CurrentUsage
  {
    get { return currentUsage; }
  }

  public int ThirstFactor
  {
    get { return thirstFactor; }
  }

  public int HungerFactor
  {
    get { return hungerFactor; }
  }

  public GameObject HoldObject
  {
    get { return holdObject; }
  }

  public virtual void Interact()
  {
    OnPlayerInteraction(this);
  }

  public virtual void AddUsage()
  {
    if (currentUsage == -1)
    {
      return;
    }
    else
    {
      SetMaxUsage();
    }
  }

  public virtual void SetGraphicByUsage()
  {

  }

  public virtual void LaunchErrorAction()
  {
    Debug.Log("Meeeeec");
  }

  private void Awake()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    TransitionStageAnimationManager.OnHalfAnimationDone += OnHalfAnimationDone;
    GameController.OnActionErrated += LaunchErrorAction;
  }

  void OnHalfAnimationDone()
  {
    SetGraphicByUsage();
  }

  public void SetMaxUsage()
  {
    currentUsage = 3;
  }

  public void BreakObject()
  {
    currentUsage = -1;
  }

  public void SpendUsage()
  {
    currentUsage--;
  }

  private void OnStageFinished(ActionsManager.Stage stage)
  {
    if (type == stage.FirstAction.type || type == stage.SecondAction.type)
    {
      PlayerManager.Instance.EatAndDrink(-hungerFactor, -thirstFactor);
    }
    if (holdObject != null)
    {
      holdObject.SetActive(false);
    }
  }

  [SerializeField]
  private Transform smartPosition;

  [SerializeField]
  private ObjectType type;

  [SerializeField]
  private Transform meshTransform;

  [SerializeField]
  private int maxUsage;

  [SerializeField]
  private int hungerFactor;

  [SerializeField]
  private int thirstFactor;

  [SerializeField]
  private GameObject holdObject;

  private int currentUsage = -1;
}
