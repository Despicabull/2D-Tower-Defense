using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class Node : MonoBehaviour
{
    [HideInInspector] public int x, y;

    [HideInInspector] public Node previousNode;
    [HideInInspector] public bool isOccupied;
    [HideInInspector] public int gCost, hCost, fCost;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        isOccupied = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = spriteRenderer.bounds.size;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        collider2D.GetComponent<EntityClass>().node = this;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
