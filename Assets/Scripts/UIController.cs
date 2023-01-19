using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _aimPanel;
    
    [SerializeField] private TextMeshProUGUI _timeTxt;

    [SerializeField] private TextMeshProUGUI _targetDestroyedTxt;
    [SerializeField] private TextMeshProUGUI _smallTargetTxt;
    [SerializeField] private TargetController _targetController;
    
    private StarterAssetsInputs _inputs;

    private float _time;
    private float _minutes;
    private float _seconds;
    private float _hundredth;
    
    public TextMeshProUGUI SmallTargetTxt
    {
        get { return _smallTargetTxt; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        
        _aimPanel.SetActive(false);
        _smallTargetTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _aimPanel.SetActive(_inputs.aim);
        
        _time += Time.deltaTime;


        _timeTxt.text = "Time : " + FormatTime(_time);
        
        _targetDestroyedTxt.text = "Target Destroyed \n" + "[" +
                                   _targetController.TargetDestroyedNbr +
                                   " / " + _targetController.Targets.Length + "]";
        
        _smallTargetTxt.text = _targetDestroyedTxt.text;
    }

    private String FormatTime(float time)
    {
        int roundTime = (int)(time * 100.0f);
        int minutes = roundTime / (60 * 100);
        int seconds = (roundTime % (60 * 100)) / 100;
        int hundredths = roundTime % 100;
        
        return String.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
    }
}
