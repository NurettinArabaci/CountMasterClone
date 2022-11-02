using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public List<GameObject> Levels = new List<GameObject>();

    public int levelCount;

    protected override void Awake()
    {
        base.Awake();

        foreach (var item in Resources.LoadAll<GameObject>("Levels"))
        {
            Levels.Add(item);
        }

    }

    private void Start()
    {
        LevelCreated();
    }

    public void LevelCreated()
    {
        ResetLevels();
        Instantiate(Levels[levelCount-1], transform);
    }

    void ResetLevels()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i));
            }
        }

    }


}
