using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S02PostProcessingManager : MonoBehaviour
{
    public GameObject volume;
    
    void Start()
    {
        volume.SetActive(false);
    }
    
    public void ToggleEffect()
    {
        volume.SetActive(!volume.activeSelf);
    }
}
