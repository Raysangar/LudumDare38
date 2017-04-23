using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SpikeController : MonoBehaviour {

	void Start () {
    navMeshAgent = GetComponent<NavMeshAgent> ();
    animator.SetBool ("move", false);
    walkParticles.Stop ();
  }

  private void FixedUpdate ()
  {
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

  private NavMeshAgent navMeshAgent;

}
