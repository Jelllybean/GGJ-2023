using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggableItem : MonoBehaviour
{

    public float radius;
    public float innerRadius;
    private CircleCollider2D sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<CircleCollider2D>();
        sphereCollider.radius = radius * 2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
    }
}
