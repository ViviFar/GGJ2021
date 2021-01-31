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
                return speed;
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
