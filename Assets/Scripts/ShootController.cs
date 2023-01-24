using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Rig _aimRig;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _shootingOrigin;
    
    [SerializeField] private Projectile _projectile;
    [SerializeField] private GameObject _runningWeapon;
    [SerializeField] private GameObject _shootingWeapon;

    private Transform _shootWeaponTrans;
    
    [SerializeField] private ThirdPersonController _thirdPersonController;
    [SerializeField] private float _moveSensitivity = 4f;
    [SerializeField] private float _aimSenitivity = 2f;
    
    private StarterAssetsInputs _inputs;
    private Camera _mainCamera;
    [SerializeField]private CinemachineVirtualCamera _aimCamera;
    
    [SerializeField] private float _cooldownTime;

    [SerializeField] private GameObject _smokePrefab;
    [SerializeField] private GameObject _shootExplosionPrefab;
    
    [SerializeField] private AudioSource _shootSound;

    
    private float _nextFireTime;

    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _mainCamera = Camera.main;

        _shootWeaponTrans = _shootingWeapon.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the target point to viewport center.
        Vector3 targetPosition = _mainCamera.ViewportToWorldPoint(
            new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));

        // Fire a Ray from the camera and detected any intersection between the ray and an object.
        Ray targetRay = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));

        // The out keyword set a bool variable if there is a collision.
        if (Physics.Raycast(targetRay, out RaycastHit hit))
        {
            targetPosition = hit.point;

            if (_inputs.aim && _inputs.shoot)
            {
                Instantiate(_shootExplosionPrefab, _shootingOrigin.position, Quaternion.identity);
                
                Target target = hit.collider.GetComponent<Target>();
            
                if (target != null)
                {
                    StartCoroutine(DestroyTime(target));
                }
                
                Instantiate(_smokePrefab, hit.point, Quaternion.identity);
            }
        }
       
        _targetPoint.position = Vector3.Lerp(_targetPoint.position, targetPosition, 2f);

        if (_inputs.aim)
        {
            _aimRig.weight = 1;
            
            _shootingWeapon.SetActive(true);
            _runningWeapon.SetActive(false);
            
            _thirdPersonController.SetCamSensitivity(_aimSenitivity);
            
            _thirdPersonController.MoveSpeed = 3f;
            _thirdPersonController.SprintSpeed = 3f;
            
            Vector3 worldAimTarget = targetPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        
            transform.forward  = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            if (_inputs.shoot && Time.time > _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + _cooldownTime;
                
                _aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.4f;
                _aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.4f;

                _aimCamera.m_Lens.FieldOfView += 1f;
            }
            
            else if (!_inputs.shoot)
            {
                _aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
                _aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;

                _aimCamera.m_Lens.FieldOfView = 15;
            }
        }
        else
        {
            _aimRig.weight = 0;
            
            _shootingWeapon.SetActive(false);
            _runningWeapon.SetActive(true);
            
            _thirdPersonController.SetCamSensitivity(_moveSensitivity);
            _thirdPersonController.SetRotateOnMove(true);
            _thirdPersonController.MoveSpeed = 4f;
            _thirdPersonController.SprintSpeed = 8.335f;
        }
        
    }
    
    private void Shoot()
    {
        Instantiate(_projectile, _shootingOrigin.position,
                Quaternion.LookRotation(_targetPoint.position - _shootingOrigin.position));
        
        _shootSound.Play();

        _inputs.shoot = false;
    }

    private IEnumerator DestroyTime(Target target)
    {
        yield return new WaitForSeconds(0.01f);
        
        target.DestroySelf();
    }
    
}
