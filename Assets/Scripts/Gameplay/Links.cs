using UnityEngine;

public static class Links
{
    public static Transform projectilesContainer;
    public static void Init()
    {
        projectilesContainer = GameObject.Find("ProjContainer").transform;
    }
}
