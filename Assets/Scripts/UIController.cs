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
    [SerializeField] private GameObject _scoresPanel;
    [SerializeField] private GameObject _endGamePanel;
    
    [SerializeField] private TextMeshProUGUI _timeTxt;

    [SerializeField] private TextMeshProUGUI _targetDestroyedTxt;
    [SerializeField] private TextMeshProUGUI _smallTargetTxt;
    [SerializeField] private TargetController _targetController;
    
    [SerializeField] private TextMeshProUGUI _endGameTxt;

    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _gameOverSound;
    [SerializeField] private AudioSource _mainTheme;
    private bool _mustPlayEndSound = true;
    
    private StarterAssetsInputs _inputs;

    [SerializeField] private float _time = 600f;
    private float _minutes;
    private float _seconds;
    private float _hundredth;

    private bool _isTimerActive = true;

    public TextMeshProUGUI SmallTargetTxt
    {
        get { return _smallTargetTxt; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        
        _aimPanel.SetActive(false);
        _endGamePanel.SetActive(false);
        _smallTargetTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _aimPanel.SetActive(_inputs.aim);
        
        if (_targetController.IsStartTimerFinished)
        {
            _scoresPanel.SetActive(true);
            
            if (_isTimerActive)
            {
                _time -= Time.deltaTime;
            }
            
            _timeTxt.text = "Time : " + FormatTime(_time);
        
            _targetDestroyedTxt.text = "Target Destroyed \n" + "[" +
                                       _targetController.TargetDestroyedNbr +
                                       " / " + _targetController.Targets.Length + "]";
        
            _smallTargetTxt.text = _targetDestroyedTxt.text;

            if (_time <= 0 || 
                _targetController.TargetDestroyedNbr == _targetController.Targets.Length)
            {
            
                _endGamePanel.SetActive(true);

                if (_targetController.TargetDestroyedNbr == _targetController.Targets.Length)
                {
                    _endGameTxt.text = "You Won !!!";
                    _isTimerActive = false;
                    
                    _mainTheme.Stop();
                    if (_mustPlayEndSound)
                    {
                        _winSound.Play();

                        _mustPlayEndSound = false;
                    }
                }
                
                else if (_time <= 0f)
                {
                    _endGameTxt.text = "Game Over...";
                    _isTimerActive = false;

                    _time = 0f;
                    
                    _mainTheme.Stop();
                    if (!_gameOverSound.isPlaying)
                    {
                        _gameOverSound.Play();
                        _mustPlayEndSound = false;
                    }
                }
            }
        }
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
