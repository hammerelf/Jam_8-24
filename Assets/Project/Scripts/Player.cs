//Created by: Ryan King

using HammerElf.Tools.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveDistance = 2;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if(Keyboard.current.wKey.wasPressedThisFrame)
        {
            //transform.position += transform.forward * moveDistance;
            rb.MovePosition(transform.position + transform.forward * moveDistance);
        }
        else if(Keyboard.current.sKey.wasPressedThisFrame)
        {
            //transform.position -= transform.forward * moveDistance;
            rb.MovePosition(transform.position - transform.forward * moveDistance);
        }

        if(Keyboard.current.dKey.wasPressedThisFrame)
        {
            transform.Rotate(0, 90, 0);
            //transform.position += Vector3.right * moveDistance;
        }
        else if(Keyboard.current.aKey.wasPressedThisFrame)
        {
            transform.Rotate(0, -90, 0);
            //transform.position += Vector3.left * moveDistance;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            ConsoleLog.Log("You lose!!!");

            EditorApplication.isPlaying = false;
        }
    }
}
