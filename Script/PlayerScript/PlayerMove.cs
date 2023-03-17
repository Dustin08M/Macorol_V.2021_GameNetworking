using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int _mSpeed;
    [SerializeField] Transform SpawnArea;
    [SerializeField] Transform reset;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        reset.transform.position = player.transform.position + Vector3.up;
        ControlKeys();
        ResetPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            player.transform.position = SpawnArea.transform.position;
        }
    }

    private void ControlKeys()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _mSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _mSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * _mSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _mSpeed * Time.fixedDeltaTime);
        }
    }

    private void ResetPlayer()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = reset.transform.position;
            transform.rotation = reset.transform.rotation;
        }
    }
}
