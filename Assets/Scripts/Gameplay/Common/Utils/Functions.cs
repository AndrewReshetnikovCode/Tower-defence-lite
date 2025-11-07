using UnityEngine;

public static class Functions
{
    public static void Rotate2DTowards(Transform transform, Vector3 position)
    {
        Vector3 tp = transform.position;
        Vector3 dir = (position - tp).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
    public static void Rotate2DTowards(Transform transform, Vector3 position, Vector3 eulerOffset)
    {
        Vector3 tp = transform.position;
        Vector3 dir = (position - tp).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(eulerOffset.x, eulerOffset.y, z + eulerOffset.z);
    }
    public static void Rotate2DTowards(Transform transform, Vector3 position, float speed)
    {
        Vector3 tp = transform.position;
        Vector3 dir = (position - tp).normalized;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, z), speed * Time.deltaTime);
    }
}
