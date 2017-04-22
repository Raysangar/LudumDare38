using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class PlayerMovementController : MonoBehaviour
{
  private void Start ()
  {
    navMeshAgent = GetComponent<NavMeshAgent> ();
    cachedTransform = transform;
    PointAndClickManager.OnSmartObjectClicked += OnSmartObjectClicked;
  }

  private void OnSmartObjectClicked (SmartObject smartObject)
  {
    StopAllCoroutines ();
    target = smartObject;
    StartCoroutine (GoInteractWithTarget ());
  }

  private IEnumerator GoInteractWithTarget ()
  {
    navMeshAgent.destination = target.SmartPosition.position;

    yield return new WaitUntil (IsCloseToTarget);

    float time = 0;
    Vector3 origin = cachedTransform.forward;
    float duration = (target.SmartPosition.forward - origin).magnitude / angularSpeed;
    while (!LookingToSmartObject)
    {
      cachedTransform.forward = Vector3.Lerp (origin, target.SmartPosition.forward, Mathf.InverseLerp (0, duration, time));
      yield return null;
      time += Time.deltaTime;
    }

    target.Interact ();
  }

  private bool LookingToSmartObject
  {
    get
    {
      return (target.SmartPosition.forward - cachedTransform.forward).sqrMagnitude < angularThreshold;
    }
  }

  private bool IsCloseToTarget ()
  {
    return (target.SmartPosition.position - cachedTransform.position).sqrMagnitude < distanceThreshold;
  }

  [SerializeField]
  private float angularSpeed;

  [SerializeField]
  private float distanceThreshold;

  [SerializeField]
  private float angularThreshold;

  private SmartObject target;
  private Transform cachedTransform;
  private NavMeshAgent navMeshAgent;
}
