using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : GenericSingleton<VideoManager>
{
    [SerializeField]
    private VideoClip vc;
    [SerializeField]
    private VideoPlayer vp;

    // Start is called before the first frame update
    void Start()
    {
        vp.clip = vc;
        StartCoroutine(PlayClip());
    }

    private IEnumerator PlayClip()
    {
        vp.Play();
        yield return new WaitForSeconds((float)vp.clip.length);
        GameStateMachine.Instance.CurrentState = GameStates.Menu;
    }
}
