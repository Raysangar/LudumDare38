using UnityEngine;

[RequireComponent (typeof (BoxCollider))]
public abstract class SmartObject : MonoBehaviour
{
  public static System.Action<SmartObject> OnPlayerInteraction = delegate { };
  public static System.Action OnPlayerFailedAction = delegate { };

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

  public bool BrokenOnCurrentStage
  {
    get { return brokenOnCurrentStage; }
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

  public virtual void Interact ()
  {
    OnPlayerInteraction (this);
  }

  public virtual void AddUsage ()
  {
    currentUsage++;
  }

  public virtual void SetGraphicByUsage ()
  {

  }

  public virtual void LaunchErrorAction (ObjectType type)
  {
    if (this.type == type)
    {
      OnPlayerFailedAction ();
    }
  }

  public void Start ()
  {
    ActionsManager.OnStageFinished += OnStageFinished;
    TransitionStageAnimationManager.OnHalfAnimationDone += OnHalfAnimationDone;
    GameController.OnActionErrated += LaunchErrorAction;
  }

  private void OnDestroy ()
  {
    ActionsManager.OnStageFinished -= OnStageFinished;
    TransitionStageAnimationManager.OnHalfAnimationDone -= OnHalfAnimationDone;
    GameController.OnActionErrated -= LaunchErrorAction;
  }

  protected virtual void OnHalfAnimationDone ()
  {
    SetGraphicByUsage ();
    brokenOnCurrentStage = false;
  }

  public void SetMaxUsage ()
  {
    currentUsage = maxUsage;
  }

  public void BreakObject ()
  {
    currentUsage = -1;
    brokenOnCurrentStage = true;
  }

  public void SpendUsage ()
  {
    currentUsage--;
  }

  private void OnStageFinished (ActionsManager.Stage stage)
  {
    if (type == stage.FirstAction.type || type == stage.SecondAction.type)
    {
      PlayerManager.Instance.EatAndDrink (-hungerFactor, -thirstFactor);
    }
    if (holdObject != null)
    {
      holdObject.SetActive (false);
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
  private bool brokenOnCurrentStage = false;
}
