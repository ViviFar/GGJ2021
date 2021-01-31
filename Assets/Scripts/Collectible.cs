using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private LayerMask collectible;

    [SerializeField]
    private GameStates stateAfter;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TakeKey();
        }
    }

    private void TakeKey()
    {
        GameStateMachine.Instance.CurrentState = stateAfter;
        Destroy(this.gameObject);
    }
}
