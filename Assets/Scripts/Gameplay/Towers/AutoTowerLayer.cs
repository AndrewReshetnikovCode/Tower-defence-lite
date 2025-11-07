using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTowerLayer : MonoBehaviour
{
    static int counter = 1;
    void Start()
    {
        counter++;
        GetComponent<SpriteRenderer>().sortingOrder = counter;
    }
}
