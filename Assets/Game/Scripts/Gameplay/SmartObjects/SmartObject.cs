using UnityEngine;

[RequireComponent (typeof (BoxCollider))]
public abstract class SmartObject : MonoBehaviour
{
  public Transform SmartPosition
  {
    get { return smartPosition; }
  }

  public abstract void Interact ();

  [SerializeField]
  private Transform smartPosition;
}
