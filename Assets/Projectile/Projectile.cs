using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    
    private Rigidbody _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_rb.velocity = transform.forward * _speed;
        StartCoroutine("TimeOut");
    }

    // Update is called once per frame
    void Update()
    {
        // Define a new Position, so we must use transform and not vector.
        _rb.MovePosition(transform.position + transform.forward * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("col");
        if (other.GetComponent<Target>() != null)
        {
            Debug.Log("target col");
            other.GetComponent<Target>().DestroySelf();
        }
        
        Destroy(gameObject);
    }

    private IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
