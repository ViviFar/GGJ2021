using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackZone : MonoBehaviour
{
    [SerializeField]
    private GameObject itsTooDarkInHere;

    private void Start()
    {
        itsTooDarkInHere.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameStateMachine.Instance.LampFound)
        {
            Destroy(this.gameObject);
        }
        else
        {
            itsTooDarkInHere.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            itsTooDarkInHere.SetActive(false);
        }
    }
}
