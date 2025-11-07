using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;

    public Vector3 GetPoint(int index) => waypoints[index].position;
    public int Count => waypoints?.Length ?? 0;

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Length - 1; i++)
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
    }
}
