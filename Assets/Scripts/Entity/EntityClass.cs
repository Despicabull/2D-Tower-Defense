using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public abstract class EntityClass : MonoBehaviour
    {
        [HideInInspector] public Node node; // The node which the entity is on
        [HideInInspector] public List<Node> movePath;

        protected Pathfinding pathfinding;
        protected Animator animator;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Spawn(int spawnX, int spawnY)
        {
            pathfinding = new Pathfinding(FindObjectOfType<Nodes>());
            node = pathfinding.nodes.GetNode(spawnX, spawnY);
            transform.position = new Vector2(
                node.transform.position.x,
                node.transform.position.y);
        }
    }
}
