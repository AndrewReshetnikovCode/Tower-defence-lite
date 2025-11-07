using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTower : MonoBehaviour
{
    [SerializeField] Transform _square;
    [SerializeField] float _scale;
    public void StartHL() 
    {
        _square.localScale *= _scale;
    }
    public void StopHL()
    {
        _square.localScale /= _scale;

    }
}
