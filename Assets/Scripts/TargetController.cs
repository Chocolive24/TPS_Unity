using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Target[] _targets;

    [SerializeField] private UIController _uiController;
    [SerializeField] private float _enableTextTime = 1f;

    [SerializeField] private ThirdPersonController _player;
    
    [SerializeField] private Transform _startGameTriggerTrans;
    
    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private AudioSource _timerSound;
    
    private int _targetDestroyedNbr = 0;

    private bool _areTargetsCreated = false;
    private bool _isStartTimerFinished = false;

    public Target[] Targets { get { return _targets; } }
    public int TargetDestroyedNbr { get { return _targetDestroyedNbr; } }
    
    public void IncreaseTargetDestrNbr() { _targetDestroyedNbr++; }

    public bool IsStartTimerFinished { get { return _isStartTimerFinished; } }
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i].SetIsActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.transform.position.z >= _startGameTriggerTrans.position.z && 
            !_areTargetsCreated)
        {
            StartCoroutine(CreateTargetsTimer());

            _timerSound.Play();
        }
        
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i].IsDestroyed)
            {
                StopCoroutine("EnableText");
                StartCoroutine("EnableText");
                _targets[i].SetIsDestroyed(false);

                _hitSound.Play();
            }
        }
    }
    
    public IEnumerator EnableText()
    {
        _uiController.SmallTargetTxt.enabled = true;

        yield return new WaitForSeconds(_enableTextTime);
        _uiController.SmallTargetTxt.enabled = false;
    }

    private IEnumerator CreateTargetsTimer()
    {
        _areTargetsCreated = true;
        
        yield return new WaitForSeconds(3f);
        
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i] != null)
            {
                _targets[i].SetIsActive(true);
            }
        }

        _isStartTimerFinished = true;
    }
}
