using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Target[] _targets;

    [SerializeField] private UIController _uiController;
    [SerializeField] private float _enableTextTime = 1f;
    
    private int _targetDestroyedNbr = 0;

    public Target[] Targets { get { return _targets; } }
    public int TargetDestroyedNbr { get { return _targetDestroyedNbr; } }
    
    public void IncreaseTargetDestrNbr() { _targetDestroyedNbr++; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_targets.Length);
        
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i].IsDestroyed)
            {
                StopCoroutine("EnableText");
                StartCoroutine("EnableText");
                _targets[i].SetIsDestroyed(false);
            }
        }
    }
    
    public IEnumerator EnableText()
    {
        _uiController.SmallTargetTxt.enabled = true;

        yield return new WaitForSeconds(_enableTextTime);
        _uiController.SmallTargetTxt.enabled = false;
    }
    
}
