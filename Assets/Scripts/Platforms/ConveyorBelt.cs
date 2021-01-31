using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : GenericPlatform
{
    [SerializeField]
    private float speed = 2;
    public float Speed
    {
        get {
            if (estCasse)
                return 0;
            else
            {
                int signe = (int)(transform.localScale.x / (Mathf.Abs(transform.localScale.x)));
                return signe*speed;
            }
        }
    }

    private Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        anim.SetBool("IsBroken", estCasse);
    }

    public override void Repair()
    {
        base.Repair();
        anim.SetBool("IsBroken", estCasse);
        if (playerIsOn)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().UpdateSpeedModifier(speed);
        }
    }

}
