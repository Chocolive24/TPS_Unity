using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TutoController : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _tutoPanel;
    [SerializeField] private GameObject _scoresPanel;
    
    [SerializeField] private TextMeshProUGUI _tutoText;
    [SerializeField] private GameObject _tutoButton;

    [SerializeField] private Transform _switchTxtTrans;
    [SerializeField] private Transform _startGameTriggerTrans;
    [SerializeField] private Transform _doorTriggerTrans;
    [SerializeField] private Transform _endMoveTriggerTrans;

    private Vector3 _switchTxtPos;
    
    [SerializeField] private Target[] _targets;
    [SerializeField] private GameObject _door;
    
    private int _targetDestrNbr = 0;
    [SerializeField] private TextMeshProUGUI _smallTargetTxt;
    [SerializeField] private TextMeshProUGUI _globalTargetTxt;
    [SerializeField] private TextMeshProUGUI _destroyDoorTxt;
    

    [SerializeField] private AudioSource _hitTargetSound;
    
    private float _enableTextTime = 1f;

    public int TargetDestrDestroyedNbr { get { return _targetDestrNbr; } }
    
    public void IncreaseTargetDestrNbr() { _targetDestrNbr++; }


    // Start is called before the first frame update
    void Start()
    {
        _scoresPanel.SetActive(false);
        _tutoPanel.SetActive(true);
        
        _tutoText.text = "Use [W-A-S-D] to move.\n" +
                        "Use [SHIFT] to sprint.\n" +
                        "Use [SPACE] to jump.\n";

        _switchTxtPos = _switchTxtTrans.position;
        
        _smallTargetTxt.enabled = false;
        _globalTargetTxt.enabled = false;
        _destroyDoorTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.transform.position.z >= _switchTxtPos.z)
        {
            _tutoButton.SetActive(true);
            
            _tutoText.alignment = TextAlignmentOptions.Center;
            _tutoText.transform.localPosition = Vector3.zero;
            
            _tutoText.text = "Use [Right-Click] to aim and then [Left-Click] to shoot.";
        }
        
        else if (_player.transform.position.z >= _endMoveTriggerTrans.position.z &&
                 _player.transform.position.z < _switchTxtPos.z)
        {
            _tutoButton.SetActive(false);
        }
        
        if (_player.transform.position.z >= _doorTriggerTrans.position.z)
        {
            _destroyDoorTxt.enabled = true;
            _globalTargetTxt.enabled = true;
        }
        
        if (_player.transform.position.z >= _startGameTriggerTrans.position.z)
        {
            _tutoPanel.SetActive(false);
        }
        
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i].IsDestroyed)
            {
                _hitTargetSound.Play();
                
                StopCoroutine("EnableText");
                StartCoroutine("EnableText");
                _targets[i].SetIsDestroyed(false);
            }
        }
        
        _smallTargetTxt.text = "Target Destroyed \n" + "[" +
                                   _targetDestrNbr +
                                   " / " + _targets.Length + "]";
        
        _globalTargetTxt.text = _smallTargetTxt.text;

        if (_targetDestrNbr == _targets.Length - 1)
        {
            _tutoButton.SetActive(false);
        }
        
        if (_targetDestrNbr == _targets.Length)
        {
            _tutoButton.SetActive(true);
            
            Destroy(_door);

            _tutoText.fontSize = 40f;
            _tutoText.text = "Good, you are ready for the next step.\n" +
                             "When you leave this room, you'll have 10 minutes to destroy all the targets.";

            _globalTargetTxt.enabled = false;
            _destroyDoorTxt.enabled = false;
        }
    }
    
    public IEnumerator EnableText()
    {
        if (_player.transform.position.z >= _doorTriggerTrans.position.z)
        {
            _smallTargetTxt.enabled = true;
        }
        
        yield return new WaitForSeconds(_enableTextTime);
        _smallTargetTxt.enabled = false;
    }
}
