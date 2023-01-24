using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _patternTime;
    private float _time;
    
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        _rb.velocity = _velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;

        if (_time >= _patternTime)
        {
            _rb.velocity = -_rb.velocity;
            _time = 0f;
        }
    }
}
