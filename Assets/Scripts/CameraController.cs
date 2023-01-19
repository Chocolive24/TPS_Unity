using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private StarterAssetsInputs _inputs;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        _virtualCamera.enabled = _inputs.aim;
    }
}
