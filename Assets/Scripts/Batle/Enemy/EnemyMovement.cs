using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target; //player
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float speed = 3f;

    void Start()
    {
        agent.speed = speed;
        InvokeRepeating(nameof(UpdatePath), 0f, 0.2f);   // 5 раз в секунду
    }

    void UpdatePath()
    {
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }
    }

}

