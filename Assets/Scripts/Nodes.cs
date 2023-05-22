using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    
    private int nodeCountX, nodeCountY;
    private float camPosX, camPosY;
    [SerializeField] private Node[,] nodeArray;

    public void Initialize(int nodeCountX, int nodeCountY)
    {
        this.nodeCountX = nodeCountX;
        this.nodeCountY = nodeCountY;

        nodeArray = new Node[nodeCountX, nodeCountY];

        Vector3 spriteSize = nodePrefab.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 collider2D = nodePrefab.GetComponent<BoxCollider2D>().size;

        for (int i = 0; i < nodeCountX; i++)
        {
            for (int j = 0; j < nodeCountY; j++)
            {
                nodeArray[i, j] = Instantiate(
                    original: nodePrefab, 
                    position: new Vector2(transform.position.x + (i * spriteSize.x), transform.position.y - (j * spriteSize.y)),
                    rotation: transform.rotation, transform).GetComponent<Node>();
                nodeArray[i, j].x = i;
                nodeArray[i, j].y = j;
            }
        }

        // Move the position of camera to the center of node parent
        float xPos = 0.5f * spriteSize.x * (nodeCountX - 1);
        float yPos = 0.5f * spriteSize.y * (1 - nodeCountY);

        Camera.main.transform.position = new Vector3(xPos, yPos, Camera.main.transform.position.z);
    }

    public int GetWidth()
    {
        return nodeArray.GetLength(0);
    }

    public int GetHeight()
    {
        return nodeArray.GetLength(1);
    }

    // Used to get node by indices during spawning
    public Node GetNode(int x, int y)
    {
        return nodeArray[x, y];
    }
}
