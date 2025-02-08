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
    
    
    private float _timer = 0f;
    
    void Update()
    {
        if (_timer < 0f)
        {
            Vector2 spawnPos = new();
            
            spawnPos.x = Random.Range(_leftPoint.x, _rightPoint.x);
            spawnPos.y = Random.Range(_leftPoint.y, _rightPoint.y);

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
}
