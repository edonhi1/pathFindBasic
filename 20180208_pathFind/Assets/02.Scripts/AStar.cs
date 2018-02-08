using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar {
    public static PriorityQueue closedList, openList;

    private static float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        Vector3 vecCost = curNode.position - goalNode.position;
        return vecCost.magnitude;
    }

    public static ArrayList FindPath(Node start, Node goal)
    {
        openList = new PriorityQueue();
        openList.Push(start);
        start.nodeTotalCost = 0f;
        start.estimatedCost = HeuristicEstimateCost(start, goal);

        closedList = new PriorityQueue();
        Node node = null;

        while (openList.length != 0)
        {
            node = openList.First();
            if (node.position == goal.position)
            {
                return CalculatePath(node);
            }
            ArrayList neighbours = new ArrayList();

            GridManager.instance.GetNeighbours(node, neighbours);

            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = (Node)neighbours[i];
                if (!closedList.contains(neighbourNode))
                {
                    float cost = HeuristicEstimateCost(node, neighbourNode);
                    float totalCost = node.nodeTotalCost + cost;
                    float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);
                    neighbourNode.nodeTotalCost = totalCost;
                    neighbourNode.parent = node;
                    neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
                    if (!openList.contains(neighbourNode))
                    {
                        openList.Push(neighbourNode);
                    }
                }
            }
            //현재 노드를 closedList에 추가한다.
            closedList.Push(node);
            //그리고 openList에서는 제거한다.
            openList.Remove(node);
        }
        //아래 코드의 위치는 어디인가?
        if (node.position != goal.position)
        {
            Debug.LogError("Goal Not Found");
            return null;
        }
        return CalculatePath(node);
    }

    private static ArrayList CalculatePath(Node node)
    {
        ArrayList list = new ArrayList();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }
}
