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
    private GameObject _canvaRules;
    
    [SerializeField] 
    private GameObject _canvaEndGame;

    [SerializeField] private Image _lifeBar;

    public Material enviroUn;
    public Material enviroDeux;
    public Material enviroTrois;

    public Light lighUn;
    public Light lighDeux;
    public Light lighTrois;

    public GameObject enviroPlane;

    private float _time;
    private bool _isPlaying;
    private int _score;

    public float time
    {
        get => _time;
    }

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
        _lifeBar.fillAmount = 1f;
        spawner.IsPlaying = true;
        MusicScript.instance.PlayBoutonMenuSFX();
        gameObject.GetComponent<Draw>().IsPlaying = true;
        gameObject.GetComponent<Draw>().ResetGame();
        _canvaMenu.SetActive(false);
        spawner.KillAllEye();
        spawner.ResetSpawnInterval();
        RandomScene();
    }

    public void RandomScene()
    {
        int rand = Mathf.FloorToInt(UnityEngine.Random.Range(0, 3));
        if (rand == 0)
        {
            lighUn.gameObject.SetActive(true);
            enviroPlane.GetComponent<MeshRenderer>().material = enviroUn;
            StartCoroutine(MusicScript.instance.PlayRandomAudioClipFirstEnviro());
        }
        else if (rand == 1)
        {
            lighDeux.gameObject.SetActive(true);
            enviroPlane.GetComponent<MeshRenderer>().material = enviroDeux;
            StartCoroutine(MusicScript.instance.PlayRandomAudioClipSecondEnviro());
        }
        else
        {
            lighTrois.gameObject.SetActive(true);
            enviroPlane.GetComponent<MeshRenderer>().material = enviroTrois;
            StartCoroutine(MusicScript.instance.PlayRandomAudioClipThirdEnviro());
        }
    }

    public void EndGameBecauseUNoob()
    {
        StopAllCoroutines();
        MusicScript.instance.PlayDefeateSFX();
        _isPlaying = false;
        spawner.IsPlaying = false;
        gameObject.GetComponent<Draw>().IsPlaying = false;
        gameObject.GetComponent<Draw>().DestroyTheCrayAndFlavien();
        _canvaEndGame.SetActive(true);
        _canvaEndGame.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "You've succumbed to the evil spirit in " + (int)_time + " seconds";
    }

    public void GoBackToMenuAndRetryNoob()
    {
        MusicScript.instance.PlayMenuMusique();
        _canvaMenu.SetActive(true);
        _canvaEndGame.SetActive(false);
        _canvaRules.SetActive(false);
    }

    public void GoBackToMenu()
    {
        _canvaMenu.SetActive(true);
        _canvaEndGame.SetActive(false);
        _canvaRules.SetActive(false);
    }

        public void HowToPlayToThisGame()
    {
        _canvaMenu.SetActive(false);
        _canvaRules.SetActive(true);
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
