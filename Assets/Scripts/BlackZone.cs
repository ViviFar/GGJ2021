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
        else if(collision.gameObject.tag=="Player")
        {
            Debug.Log("casse toi player");
            itsTooDarkInHere.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(OterTexte());
        }
    }

    private IEnumerator OterTexte()
    {
        yield return new WaitForSeconds(2);
        itsTooDarkInHere.SetActive(false);
    }
}
