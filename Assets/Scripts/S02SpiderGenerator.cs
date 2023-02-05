using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S02SpiderGenerator : MonoBehaviour
{
    public S02SpiderController[] Spiders { get; private set; }
    
    public GameObject spiderPrefab;

    public void Start()
    {
        UpdateSpiders();
    }

    public void GenerateSpider()
    {
        GameObject newSpider = Instantiate(spiderPrefab);
        newSpider.transform.parent = transform;
        newSpider.transform.localPosition = new Vector3(0, 0, 0);
        newSpider.transform.rotation = Quaternion.identity;
        UpdateSpiders();
    }

    public void ResetSpiders()
    {
        foreach (var s in Spiders)
        {
            if(s != null)
                Destroy(s.gameObject);
        }
        GenerateSpider();
    }

    private void UpdateSpiders()
    {
        Spiders = GetComponentsInChildren<S02SpiderController>();
    }
}
