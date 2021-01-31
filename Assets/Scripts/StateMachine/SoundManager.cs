using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
    [SerializeField]
    private AudioSource aSource;

    [SerializeField]
    private AudioClip aClip;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        aSource.clip = aClip;
        aSource.loop = true;
        aSource.Play();
    }

    private void Update()
    {
    }
}
