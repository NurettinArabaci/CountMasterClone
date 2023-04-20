using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoSingleton<ParticleManager>
{
    [System.Serializable]

    public class ParticlePool
    {
        public string objectTag;
        public ParticleSystem prefab;


    }

    public List<ParticlePool> particlePools;
    public Dictionary<string, ParticleSystem> poolDict;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        poolDict = new Dictionary<string, ParticleSystem>();

        foreach (ParticlePool item in particlePools)
        {
            poolDict.Add(item.objectTag, item.prefab);
        }
    }

   
    public ParticleSystem GetSpawnParticle(string tag, Vector3 pos)
    {
        ParticleSystem obj = Instantiate(poolDict[tag]);
        obj.transform.position = pos;
        //obj.transform.SetPositionAndRotation(pos, rot);

        return obj;

    }



}
