using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SpikeController : MonoBehaviour {

	void Start () {
    navMeshAgent = GetComponent<NavMeshAgent> ();
    animator.SetBool ("move", false);
  }

  private void FixedUpdate ()
  {
    navMeshAgent.destination = player.position;
    animator.SetBool ("move", (player.position - transform.position).sqrMagnitude > distanceThreshold);
  }

  [SerializeField]
  private Transform player;

  [SerializeField]
  private Animator animator;

  [SerializeField]
  private float distanceThreshold;

  private NavMeshAgent navMeshAgent;

}
