using System;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [Serializable]
    public enum EyeType
    {
        Other,
        Malefic
    }
    
    [SerializeField] 
    private float _damage;

    [SerializeField] 
    private int _nbDraw;

    [SerializeField] 
    private EyeType _eyeType;

    private float spawnTime;

    private void Start()
    {
        spawnTime = 0;
    }

    public float GeALotOfDamage()
    {
        spawnTime += Time.deltaTime;
        return _damage * spawnTime;
    }

    public void DoALotOfDamage()
    {
        if (_eyeType == EyeType.Other)
        {
            Destroy(this.gameObject);
            return;
        }

        _nbDraw--;
        if (_nbDraw == 0)
        {
            Destroy(this.gameObject);
            return;
        }
        
        //ChangeAnimator
    }
}
