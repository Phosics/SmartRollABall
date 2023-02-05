using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S02Rotator : MonoBehaviour
{
    public Vector3 rotationOffset = Vector3.zero;

    private void FixedUpdate()
    {
        transform.Rotate(rotationOffset * Time.deltaTime);
    }
}
