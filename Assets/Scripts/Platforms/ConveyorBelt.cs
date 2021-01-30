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

    public override void Repair()
    {
        base.Repair();
        if (playerIsOn)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().UpdateSpeedModifier(speed);
        }
    }

}
