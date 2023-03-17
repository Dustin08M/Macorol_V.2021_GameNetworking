using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{
    public bool grounded = false;
    public float groundcheckdistance;
    public float buffercheckdistance = 0.1f;
    public int jumpvalue = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        groundcheckdistance = 1.1f;
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * jumpvalue, ForceMode.Impulse);
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundcheckdistance))
        {
            grounded = true;
        }
        else grounded = false;
    }
}
