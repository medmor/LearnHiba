using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuzzleLogic : MonoBehaviour
{
    public Transform StartPos;
    public Transform GoalPos;
    private NavMeshPath path;

    public ItemToLearn item;
    private List<char> chars = new List<char>();
    public NavMeshAgent agent;

    public GameObject textPerph;
    public Transform InitialPos;

    void Start()
    {
        path = new NavMeshPath();
        foreach (var c in item.ArName)
        {
            chars.Add(c);
        }
        foreach (var c in item.EnName)
        {
            chars.Add(c);
        }
        foreach (var c in item.FrName)
        {
            chars.Add(c);
        }
        agent.CalculatePath(GoalPos.position, path);
        agent.SetPath(path);
        agent.isStopped = true;
        var pos = GetRandomPathPositions();
        for (var i = 0; i < chars.Count; i++)
        {
            var txt = Instantiate(textPerph);
            txt.transform.position = pos[i];
            txt.GetComponent<PuzzleText>().SetTexts(chars[i].ToString());
        }
    }

    void Update()
    {
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

    List<Vector3> GetRandomPathPositions()
    {
        var randomPathPositions = new List<Vector3>();
        var initialPos = InitialPos.position;
        var averageDist = GetPathRemainingDistance() / chars.Count;
        var cornerIndex = 0;
        var remainingDistToCorner = Vector3.Distance(initialPos, path.corners[cornerIndex]);
        for (int i = 0; i < chars.Count; i++)
        {
            var dist = averageDist;

            while (remainingDistToCorner < dist && cornerIndex < path.corners.Length - 1)
            {
                dist -= remainingDistToCorner;
                initialPos = path.corners[cornerIndex];
                cornerIndex++;
                remainingDistToCorner = Vector3.Distance(initialPos, path.corners[cornerIndex]);
            }
            var dir = (path.corners[cornerIndex] - initialPos).normalized;

            randomPathPositions.Add(initialPos + dir * dist);
            initialPos = randomPathPositions[i];
            remainingDistToCorner -= dist;
        }


        return randomPathPositions;
    }
}
