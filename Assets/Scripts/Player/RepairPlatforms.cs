using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPlatforms : MonoBehaviour
{
    [SerializeField]
    private LayerMask repairableObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && GameStateMachine.Instance.CanRepair)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, (int)repairableObject);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "ConveyorBelt")
                {
                    hit.collider.gameObject.GetComponent<ConveyorBelt>().Repair();
                }
                else
                {
                    hit.collider.gameObject.GetComponent<MonteCharge>().Repair();
                }
            }
        }
    }
}
