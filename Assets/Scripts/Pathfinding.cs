using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Nodes nodes;
    
    private List<Node> openList;
    private List<Node> closedList;

    public Pathfinding(Nodes nodes)
    {
        this.nodes = nodes;
    }

    public bool InRange(Node startNode, Node endNode, int range)
    {
        int distanceX = Mathf.Abs(startNode.x - endNode.x);
        int distanceY = Mathf.Abs(startNode.y - endNode.y);

        return distanceX <= range && distanceY <= range;
    }

    public List<Node> FindPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();

        openList = new List<Node> { startNode };
        closedList = new List<Node> { };

        for (int i = 0; i < nodes.GetWidth(); i++)
        {
            for (int j = 0; j < nodes.GetHeight(); j++)
            {
                Node node = nodes.GetNode(i, j);
                node.gCost = int.MaxValue;
                node.CalculateFCost();
                node.previousNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openList);

            if (currentNode == endNode)
            {
                path = CalculatePath(endNode);
                return path;
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) { continue; }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        return path;
    }

    public List<Node> FindPath(int startX, int startY, int endX, int endY)
    {
        List<Node> path = new List<Node>();

        Node startNode = nodes.GetNode(startX, startY);
        Node endNode = nodes.GetNode(endX, endY);

        openList = new List<Node> { startNode };
        closedList = new List<Node> { };

        for (int i = 0; i < nodes.GetWidth(); i++)
        {
            for (int j = 0; j < nodes.GetHeight(); j++)
            {
                Node node = nodes.GetNode(i, j);
                node.gCost = int.MaxValue;
                node.CalculateFCost();
                node.previousNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openList);

            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) { continue; }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList
        return null;
    }

    List<Node> GetNeighbourList(Node currentNode)
    {
        List<Node> neighbourList = new List<Node>();

        // Left
        if (currentNode.x - 1 >= 0) { neighbourList.Add(nodes.GetNode(currentNode.x - 1, currentNode.y)); }
        // Right
        if (currentNode.x + 1 < nodes.GetWidth()) { neighbourList.Add(nodes.GetNode(currentNode.x + 1, currentNode.y)); }
        // Down
        if (currentNode.y - 1 >= 0) { neighbourList.Add(nodes.GetNode(currentNode.x, currentNode.y - 1)); }
        // Up
        if (currentNode.y + 1 < nodes.GetHeight()) { neighbourList.Add(nodes.GetNode(currentNode.x, currentNode.y + 1)); }

        return neighbourList;
    }

    List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentNode = endNode;
        while (currentNode.previousNode)
        {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }

        path.Reverse();
        return path;
    }

    int CalculateDistanceCost(Node a, Node b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    Node GetLowestFCostNode(List<Node> nodeList)
    {
        Node lowestFCostNode = nodeList[0];
        for (int i = 1; i < nodeList.Count; i++)
        {
            if (nodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = nodeList[i];
            }
        }

        return lowestFCostNode;
    }
}
