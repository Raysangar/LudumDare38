using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SpikeController : MonoBehaviour {

	void Start () {
    navMeshAgent = GetComponent<NavMeshAgent> ();
    animator.SetBool ("move", false);
    walkParticles.Stop ();
    PointAndClickManager.OnSmartObjectClicked += OnSmartObjectClicked;
  }

  private void OnSmartObjectClicked (SmartObject smartObject)
  {
    playerTarget = smartObject;
    FindClosestSeatPositionToPlayerTarget ();
  }

  private void FixedUpdate ()
  {
    Vector3 target = (playerTarget != null && player.IsCloseToTarget ()) ? closestSeatPosition.position : player.transform.position;
    navMeshAgent.destination = target;
    bool moving = (target - transform.position).sqrMagnitude > distanceThreshold;
    animator.SetBool ("move", moving);
    if (moving)
    {
      walkParticles.Play ();
    }
    else
    {
      walkParticles.Stop ();
    }
  }

  private void FindClosestSeatPositionToPlayerTarget ()
  {
    closestSeatPosition = null;
    float closestDistance = float.MaxValue;
    foreach (Transform seatPosition in seatPositions)
    {
      float distance = Vector3.Distance (seatPosition.position, playerTarget.SmartPosition.position);
      if (distance < closestDistance)
      {
        closestSeatPosition = seatPosition;
        closestDistance = distance;
      }
    }
  }

  [SerializeField]
  private PlayerMovementController player;

  [SerializeField]
  private Animator animator;

  [SerializeField]
  private float distanceThreshold;

  [SerializeField]
  private ParticleSystem walkParticles;

  [SerializeField]
  private Transform[] seatPositions;

  private Transform closestSeatPosition;
  private NavMeshAgent navMeshAgent;
  private SmartObject playerTarget;
}
