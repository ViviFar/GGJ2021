using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteCharge : GenericPlatform
{
    [SerializeField]
    private Transform startTr;
    [SerializeField]
    private Transform targetTr;

    [SerializeField]
    private float timeToTravel = 2.0f;

    private Vector3 starPos;
    private Vector3 targetPos;
    private bool isMoving = false;

    private void Awake()
    {
        starPos = startTr.position;
        targetPos = targetTr.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsOn = true;
            if (!estCasse && !isMoving)
            {
                collision.transform.SetParent(this.transform);
                StartCoroutine(MonterDescendre());
            }
        }
    }

    private IEnumerator MonterDescendre()
    {
        if (!isMoving)
        {
            isMoving = true;
            yield return new WaitForSeconds(0.5f);
            float timer = 0;
            while (timer < timeToTravel)
            {
                timer += Time.deltaTime;
                float newY = Mathf.Lerp(starPos.y, targetPos.y, timer / timeToTravel);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, targetPos.y, transform.position.z);
            yield return new WaitForSeconds(2.0f);
            timer = 0;
            while (timer < timeToTravel)
            {
                timer += Time.deltaTime;
                float newY = Mathf.Lerp(targetPos.y, starPos.y, timer / timeToTravel);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, starPos.y, transform.position.z);
        }
        isMoving = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsOn = false;
            collision.transform.SetParent(null);
        }
    }

    public override void Repair()
    {
        base.Repair();
        if (playerIsOn)
        {
            StartCoroutine(MonterDescendre());
        }
    }
}
