using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
public class PlayerMovementController : MonoBehaviour
{
  private void Start ()
  {
    navMeshAgent = GetComponent<NavMeshAgent> ();
    navMeshAgent.updateRotation = false;
    PointAndClickManager.OnSmartObjectClicked += OnSmartObjectClicked;
    animator.SetBool ("move", false);
    walkParicles.Stop ();
  }

  private void OnSmartObjectClicked (SmartObject smartObject)
  {
    StopAllCoroutines ();
    target = smartObject;
    StartCoroutine (GoInteractWithTarget ());
  }

  private IEnumerator GoInteractWithTarget ()
  {
    float time;
    Quaternion origin;
    Vector3 direction;
    float duration;
    Quaternion targetRotation;

    if (!IsCloseToTarget())
    {
      animator.SetBool ("move", true);

      time = 0;
      origin = transform.rotation;
      direction = target.SmartPosition.position - transform.position;
      direction.y = 0;
      targetRotation = Quaternion.LookRotation (direction);
      if (Quaternion.Angle (origin, targetRotation) > Quaternion.Angle (targetRotation, origin))
      {
        Quaternion aux = origin;
        origin = targetRotation;
        targetRotation = aux;
      }

      duration = Quaternion.Angle (origin, targetRotation) / angularSpeed;
      while (time < duration)
      {
        transform.rotation = Quaternion.Slerp (origin, targetRotation, Mathf.InverseLerp (0, duration, time));
        yield return null;
        time += Time.deltaTime;
      }

      navMeshAgent.destination = target.SmartPosition.position;
      walkParicles.Play ();

      yield return new WaitUntil (IsCloseToTarget);
    }

    time = 0;
    origin = transform.rotation;
    targetRotation = target.SmartPosition.rotation;
    if (Quaternion.Angle (origin, targetRotation) > Quaternion.Angle (targetRotation, origin))
    {
      Quaternion aux = origin;
      origin = targetRotation;
      targetRotation = aux;
    }

    duration = Quaternion.Angle (origin, targetRotation) / angularSpeed;
    while (time < duration)
    {
      transform.rotation = Quaternion.Slerp (origin, targetRotation, Mathf.InverseLerp (0, duration, time));
      yield return null;
      time += Time.deltaTime;
    }

    animator.SetBool ("move", false);
    walkParicles.Stop ();
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

  [SerializeField]
  private ParticleSystem walkParicles;

  private SmartObject target;
  private NavMeshAgent navMeshAgent;
}
