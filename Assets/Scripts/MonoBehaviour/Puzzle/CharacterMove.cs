
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            print(agent.destination);
            var v = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            agent.destination = Camera.main.ScreenToViewportPoint(v);
            print(agent.destination);
            print(Input.mousePosition);
        }
    }
}
