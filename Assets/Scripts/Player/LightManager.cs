using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField]
    private Transform darkZone;

    [SerializeField]
    private Transform zoneEclairee;
    [SerializeField]
    private float limiteDeZoneSombre = -8;

    [SerializeField]
    private MonteCharge ascensceurToZoneSombre;

    private bool lumiereAllummee = false;
    // Start is called before the first frame update
    void Start()
    {
        zoneEclairee.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= limiteDeZoneSombre)
        {
            if (GameStateMachine.Instance.LampFound)
            {
                if (!lumiereAllummee)
                    StartCoroutine(QueLaLumiereSoit());
            }
            else
            {
                //more of a security than anything else: zoneEclairee should already be inactive and darkZone active
                zoneEclairee.gameObject.SetActive(false);
                darkZone.gameObject.SetActive(true);
                //TODO: launch a small animation saying it is too dark down there, I have to find a lamp
            }
        }
        else
        {
            zoneEclairee.gameObject.SetActive(false);
            darkZone.gameObject.SetActive(true);
            lumiereAllummee = false;
        }
    }

    private IEnumerator QueLaLumiereSoit()
    {
        lumiereAllummee = true;
        yield return new WaitForSeconds(2);
        zoneEclairee.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        darkZone.gameObject.SetActive(false);
    }
}
