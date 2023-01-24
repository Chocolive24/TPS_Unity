using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXObjects : MonoBehaviour
{
    [SerializeField] private float _livingTime = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        StartCoroutine(LivingTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LivingTime()
    {
        yield return new WaitForSeconds(_livingTime);
        
        Destroy(gameObject);
    }
}
