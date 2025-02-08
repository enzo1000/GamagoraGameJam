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
    [Range(0.000000001f, 0.001f)] private float _damage;

    [SerializeField] 
    private int _nbDraw;

    [SerializeField] 
    private EyeType _eyeType;

    private float spawnTime;

    private void Start()
    {
        spawnTime = 0;
        GetComponent<Animator>().SetInteger("life", _nbDraw);
    }

    //Inflige les degats
    public float GetALotOfDamage()
    {
        spawnTime += Time.deltaTime;
        return _damage * spawnTime;
    }

    //Prend les degats dans le prefab
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
        GetComponent<Animator>().SetInteger("life", _nbDraw);
    }
}
