using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlatform : MonoBehaviour
{
    [SerializeField]
    protected bool estCasse = false;

    protected bool playerIsOn = false;
    public bool PlayerIsOn
    {
        get { return playerIsOn; }
        set { playerIsOn = value; }
    }

    public virtual void Repair()
    {
        estCasse = false;
    }
}
