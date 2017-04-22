using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  private void Start ()
  {
    cachedTransform = transform;
    PointAndClickManager.OnSmartObjectClicked += OnSmartObjectClicked;
  }

  private void OnSmartObjectClicked (SmartObject smartObject)
  {
    StopAllCoroutines ();
    target = smartObject;
    movementDirection = (smartObject.SmartPosition.transform.position - cachedTransform.position).normalized;
    StartCoroutine (GoToTarget ());
  }

  private IEnumerator GoToTarget ()
  {
    // Look to smart position
    float time = 0;
    Vector3 origin = cachedTransform.forward;
    float duration = (movementDirection - origin).magnitude / angularSpeed;
    while (!LookingToSmartPosition)
    {
      cachedTransform.forward = Vector3.Lerp (origin, movementDirection, Mathf.InverseLerp (0, duration, time));
      yield return null;
      time += Time.deltaTime;
    }

    // Move to smart position
    Vector3 movementPerSecond = movementDirection * speed;
    while (!IsCloseToTarget)
    {
      cachedTransform.position += movementPerSecond * Time.deltaTime;
      yield return null;
    }

    // Look to smart object
    time = 0;
    origin = cachedTransform.forward;
    duration = (target.SmartPosition.forward - origin).magnitude / angularSpeed;
    while (!LookingToSmartObject)
    {
      cachedTransform.forward = Vector3.Lerp (origin, target.SmartPosition.forward, Mathf.InverseLerp (0, duration, time));
      yield return null;
      time += Time.deltaTime;
    }

    target.Interact ();
  }

  private bool LookingToSmartPosition
  {
    get
    {
      return (movementDirection - cachedTransform.forward).sqrMagnitude < angularThreshold;
    }
  }

  private bool LookingToSmartObject
  {
    get
    {
      return (target.SmartPosition.forward - cachedTransform.forward).sqrMagnitude < angularThreshold;
    }
  }

  private bool IsCloseToTarget
  {
    get
    {
      return (target.SmartPosition.position - cachedTransform.position).sqrMagnitude < distanceThreshold;
    }
  }

  [SerializeField]
  private float speed;

  [SerializeField]
  private float angularSpeed;

  [SerializeField]
  private float distanceThreshold;

  [SerializeField]
  private float angularThreshold;

  private SmartObject target;
  private Vector3 movementDirection;
  private Transform cachedTransform;
}
