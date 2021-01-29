using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStateMachine : MonoBehaviour
{
    private static GenericStateMachine instance;
    public static GenericStateMachine Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GenericStateMachine>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.AddComponent<GenericStateMachine>();
                    go.name = "StateMachine";
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
