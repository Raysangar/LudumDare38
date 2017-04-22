using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class SmartObject : MonoBehaviour
{
  public static System.Action<SmartObject> OnPlayerInteraction = delegate { };

  public enum ObjectType
  {
    Wood, Water, House, Garden, Ranch
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

  public int CurrentUsage
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

  public virtual void Interact ()
  {
    OnPlayerInteraction (this);
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

  private int currentUsage;
}
