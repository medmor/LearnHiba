
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    NavMeshAgent agent;
    Camera cam;
    Animator animator;

    public Texture2D TargetCursor;
    public Texture2D PointerCursor;

    private float remainingDistance;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.GetMask("Walkable", "Walls")))
        {
            if (hit.collider.gameObject.CompareTag("Walls"))
                Cursor.SetCursor(PointerCursor, Vector2.zero, CursorMode.Auto);
            else
            {

                Cursor.SetCursor(TargetCursor, Vector2.zero, CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    agent.isStopped = false;
                    agent.SetDestination(hit.point);
                }
            }
        }
        else
        {
            Cursor.SetCursor(PointerCursor, Vector2.zero, CursorMode.Auto);
        }

        var remainingDist = GetPathRemainingDistance();
        if (agent.isStopped || remainingDist == remainingDistance)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }
    }

    float GetPathRemainingDistance()
    {
        if (agent.pathPending ||
            agent.pathStatus == NavMeshPathStatus.PathInvalid ||
            agent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < agent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(agent.path.corners[i], agent.path.corners[i + 1]);
        }

        return distance;
    }
}
