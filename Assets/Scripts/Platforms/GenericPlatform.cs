using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlatform : MonoBehaviour
{
    [SerializeField]
    protected bool estCasse = false;
    public bool EstCasse
    {
        get { return estCasse; }
    }

    [SerializeField]
    protected Sprite platformBroken;
    [SerializeField]
    protected Sprite platformRepaired;

    protected bool playerIsOn = false;
    public bool PlayerIsOn
    {
        get { return playerIsOn; }
        set { playerIsOn = value; }
    }

    protected virtual void Start()
    {
        ChangeSprite();
    }

    public virtual void Repair()
    {
        estCasse = false;
        ChangeSprite();
    }

    protected void ChangeSprite()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        if (estCasse)
        {
            spr.sprite = platformBroken;
        }
        else
        {
            spr.sprite = platformRepaired;
        }
    }
}
