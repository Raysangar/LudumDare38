using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class SmartObject : MonoBehaviour
{
  public static System.Action<SmartObject> OnPlayerInteraction = delegate { };

  public enum ObjectType
  {
    Tree, Lake, House, Moon
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
}
