using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SpikeController : MonoBehaviour {

	void Start () {
    target = player;
    navMeshAgent = GetComponent<NavMeshAgent> ();
    animator.SetBool ("move", false);
    walkParticles.Stop ();
  }

  private void FixedUpdate ()
  {
    if (target == player)
    {

    } else
    {

    }
    navMeshAgent.destination = player.position;
    bool moving = (player.position - transform.position).sqrMagnitude > distanceThreshold;
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

  [SerializeField]
  private Transform player;

  [SerializeField]
  private Animator animator;

  [SerializeField]
  private float distanceThreshold;

  [SerializeField]
  private ParticleSystem walkParticles;

  [SerializeField]
  private Transform[] seatPositions;

  private NavMeshAgent navMeshAgent;
  private Transform target;
  private SmartObject playerTarget;

}
