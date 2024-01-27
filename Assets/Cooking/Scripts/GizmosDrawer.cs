using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDrawer : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set Gizmo color
        Gizmos.DrawWireSphere(transform.position, Settings.dropRadius); // Draw a wireframe sphere
    }
}
