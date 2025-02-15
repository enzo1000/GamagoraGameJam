using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EyeSpawner : MonoBehaviour
{
    [Serializable]
    public struct EyeProba
    {
        public GameObject prefab;
        public float probability;
    }
    
    [Header("Eye Spawner: The total probability must be 100")]
    [SerializeField]
    private List<EyeProba> _eyePrefabs = new();

    [SerializeField] 
    private float _spawnInterval;

    [SerializeField] 
    private Vector2 _leftPoint;

    [SerializeField] 
    private Vector2 _rightPoint;
    
    [SerializeField] 
    [Range(0.0001f, 0.1f)] private float _timeDiminutionBetweenSpawn;
    
    private float _timer = 0f;
    private bool _isPlaying = false;
    private float _spawn;

    private void Start()
    {
        _spawn = _spawnInterval;
    }

    public bool IsPlaying
    {
        get => _isPlaying; 
        set => _isPlaying = value;
    }
    
    void Update()
    {
        if (!_isPlaying)
            return;
        
        if (_timer < 0f)
        {
            Vector3 spawnPos = new();
            
            spawnPos.x = Random.Range(_leftPoint.x, _rightPoint.x);
            spawnPos.y = Random.Range(_leftPoint.y, _rightPoint.y);
            spawnPos.z = 6.5f;

            float v = Random.Range(0, 100);
            EyeProba eyePrefab = _eyePrefabs[0];
            float probability = 0;
            
            for (int i = 0; i < _eyePrefabs.Count; i++)
            {
                if (v < _eyePrefabs[i].probability + probability)
                {
                    eyePrefab = _eyePrefabs[i];
                    break;
                }
                probability += _eyePrefabs[i].probability;
            }
            
            GameObject go = Instantiate(eyePrefab.prefab, new(), Quaternion.identity, transform);
            go.transform.localPosition = spawnPos;
            _timer = _spawnInterval;
        }
        _timer -= Time.deltaTime;
    }

    public void KillAllEye()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ResetSpawnInterval()
    {
        _spawnInterval = _spawn;
    }

    public void DiminueTimerAndDie()
    {
        _spawnInterval -= _timeDiminutionBetweenSpawn;
        if (GameManager.Instance.time < 180 && _spawnInterval <= 2)
        {
            _spawnInterval = 2;
        }
    }

    public float GetAllDamage()
    {
        float allDamage = 0f;
        foreach (Transform child in transform)
        {
            allDamage += child.GetComponent<Eye>().GetALotOfDamage();
        }
        return allDamage;
    }
}
