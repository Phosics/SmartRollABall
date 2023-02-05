using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S02ObjectivePool : MonoBehaviour
{
    public S02Rotator[] Objectives
    {
        get
        {
            return GetComponentsInChildren<S02Rotator>();
        }
    }
}
