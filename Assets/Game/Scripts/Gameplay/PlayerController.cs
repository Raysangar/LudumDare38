using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  private void Start ()
  {
    cachedTransform = transform;
  }

  private void OnSmartObjectClicked (SmartObject smartObject)
  {
    StopAllCoroutines ();
    target = smartObject;
    movementPerSecond = (smartObject.SmartPosition.transform.position - cachedTransform.position).normalized * speed;
    StartCoroutine (GoToTarget ());
  }

  private IEnumerator GoToTarget ()
  {
    while (!LookingToSmartPosition)
    {
      cachedTransform.LookAt (target.SmartPosition);
      yield return null;
    }

    while (!IsCloseToTarget)
    {
      cachedTransform.position += movementPerSecond * Time.deltaTime;
      yield return null;
    }

    while (!LookingToSmartObject)
    {
      cachedTransform.rotation = target.SmartPosition.rotation;
      yield return null;
    }

    target.Interact ();
  }

  private bool LookingToSmartPosition
  {
    get
    {
      return true;
    }
  }

  private bool LookingToSmartObject
  {
    get
    {
      return true;
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

  private SmartObject target;
  private Vector3 movementPerSecond;
  private Transform cachedTransform;
}
