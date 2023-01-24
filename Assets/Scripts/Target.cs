using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TargetType
{
    Tuto,
    Normal,
}

public class Target : MonoBehaviour
{
    [SerializeField] private TargetController _targetController;
    [SerializeField] private TutoController _tutoController;

    [SerializeField] private TargetType _targetType;

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
        if (_targetType == TargetType.Normal)
        {
            _targetController.IncreaseTargetDestrNbr();
        }
        else if (_targetType == TargetType.Tuto)
        {
            _tutoController.IncreaseTargetDestrNbr();
        }
        
        _isDestroyed = true;
        Destroy(gameObject);
    }

    public void SetIsActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    
}
