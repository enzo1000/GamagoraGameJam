using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    
    [SerializeField] 
    private EyeSpawner spawner;

    [SerializeField] 
    private GameObject _canvaMenu;

    [SerializeField] 
    private GameObject _canvaEndGame;

    [SerializeField] private Image _lifeBar;
     
    private float _time;
    private bool _isPlaying;
    private int _score;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Update()
    {
        if (!_isPlaying)
            return;
        
        _time += Time.deltaTime;
        ReduceLifeBar();
        if (_lifeBar.fillAmount == 0f)
        {
            EndGameBecauseUNoob();
        }
    }

    public void AddToScore()
    {
        _score++;
    }
    
    private void ReduceLifeBar()
    {
        _lifeBar.fillAmount -= spawner.GetAllDamage();
    }

    public void StartThisGeniousGame()
    {
        _isPlaying = true;
        _time = 0;
        _score = 0;
        spawner.IsPlaying = true;
        MusicScript.instance.PlayBoutonMenuSFX();
        gameObject.GetComponent<Draw>().IsPlaying = true;
        _canvaMenu.SetActive(false);
    }

    public void EndGameBecauseUNoob()
    {
        _isPlaying = false;
        spawner.IsPlaying = false;
        gameObject.GetComponent<Draw>().IsPlaying = false;
        _canvaEndGame.SetActive(true);
        _canvaEndGame.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "You survived during " + (int)_time + " seconds";
        _canvaEndGame.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "You survived at " + _score + " eyes";
    }

    public void GoBackToMenuAndRetryNoob()
    {
        _canvaMenu.SetActive(true);
        _canvaEndGame.SetActive(false);
    }
    
    public void QuitThisFuckingGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
