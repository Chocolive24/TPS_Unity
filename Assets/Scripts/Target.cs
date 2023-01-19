using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private TargetController _targetController;

    private bool _isDestroyed = false;
    
    public bool IsDestroyed
    {
        get { return _isDestroyed; }
    }

    public void SetIsDestroyed(bool isDestroyed)
    {
        _isDestroyed = isDestroyed;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroySelf()
    {
        _targetController.IncreaseTargetDestrNbr();
        _isDestroyed = true;
        //StartCoroutine(_targetController.EnableText());
        Destroy(gameObject);
    }
    
    
}
