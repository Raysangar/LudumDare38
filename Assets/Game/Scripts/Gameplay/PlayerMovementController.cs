using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class PlayerMovementController : MonoBehaviour
{
  private void Start ()
  {
    navMeshAgent = GetComponent<NavMeshAgent> ();
    PointAndClickManager.OnSmartObjectClicked += OnSmartObjectClicked;
    animator.SetBool ("move", false);
  }

  private void OnSmartObjectClicked (SmartObject smartObject)
  {
    StopAllCoroutines ();
    target = smartObject;
    StartCoroutine (GoInteractWithTarget ());
  }

  private IEnumerator GoInteractWithTarget ()
  {
    animator.SetBool ("move", true);
    navMeshAgent.destination = target.SmartPosition.position;

    yield return new WaitUntil (IsCloseToTarget);

    float time = 0;
    Quaternion origin = transform.rotation;
    Quaternion targetRotation = target.SmartPosition.rotation;
    if (Quaternion.Angle (origin, targetRotation) > Quaternion.Angle (targetRotation, origin))
    {
      Quaternion aux = origin;
      origin = targetRotation;
      targetRotation = aux;
    }

    float duration = Quaternion.Angle (origin, targetRotation) / angularSpeed;
    while (time < duration)
    {
      transform.rotation = Quaternion.Slerp (origin, targetRotation, Mathf.InverseLerp (0, duration, time));
      yield return null;
      time += Time.deltaTime;
    }

    animator.SetBool ("move", false);
    target.Interact ();
  }

  private bool IsCloseToTarget ()
  {
    return (target.SmartPosition.position - transform.position).sqrMagnitude < distanceThreshold;
  }

  [SerializeField]
  private float angularSpeed;

  [SerializeField]
  private float distanceThreshold;

  [SerializeField]
  private Animator animator;

  private SmartObject target;
  private NavMeshAgent navMeshAgent;
}
