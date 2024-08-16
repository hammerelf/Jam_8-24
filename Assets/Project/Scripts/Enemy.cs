//Created by: Ryan King

using HammerElf.Tools.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;
using Time = UnityEngine.Time;

public class Enemy : MonoBehaviour
{
    public int behavior = 0;
    public float moveDistance = 2;
    public float cooldown = 5;
    public Vector2 radii = new Vector2(1f, 2f);

    private Rigidbody rb;
    public Transform modelTransform;

    private float checkCooldown;
    private bool isActing;
    private float lastBeatTime = 0;



    [Space, Space, Space]
    public Vector3 Velocity = new Vector3(1, 0, 0);

    [Range(0, 5)]
    public float RotateSpeed = 1f;
    [Range(0, 5)]
    public float Radius = 1f;

    private Vector3 _center;
    private float _angle;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        GameManager.Instance.audioProcessor.onBeat.AddListener(onBeatDetected);
        //GameManager.Instance.audioProcessor.onSpectrum.AddListener(onSpectrum);

        _center = transform.position;
    }

    public void onBeatDetected()
    {

        //if (checkCooldown > cooldown)
        //{
        //    checkCooldown = 0;

            TakeAction();
        //}

        lastBeatTime = Time.time;
    }

    //public void onSpectrum(float[] spectrum)
    //{

    //}

    //private void Start()
    //{
    //    switch(behavior)
    //    {
    //        case 0:
    //            break;
    //        case 1:
    //            InvokeRepeating("MoveStraight", 5, 5);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private void Update()
    {
        //transform.LookAt(GameManager.Instance.player.transform);
        checkCooldown += Time.deltaTime;
        //if(behavior == 3) SpiralMove();
    }

    private void TakeAction()
    {
        switch(behavior)
        {
            case 0:
                break;
            case 1:
                MoveStraight();
                break;
            case 2:
                MoveAndTurn();
                break;
            case 3:
                SpiralMove();
                //StartCoroutine(MoveInACircle());
                break;
            case 4:
                MoveWithBeat();
                break;
            default:
                break;
        }
    }

    private void MoveWithBeat()
    {
        rb.MovePosition(transform.position + (transform.forward * (Time.time - lastBeatTime)));
    }

    private void MoveStraight()
    {
        rb.MovePosition(transform.forward * moveDistance);
    }

    private void MoveAndTurn()
    {
        transform.Rotate(Vector3.up, 90 * (Random.Range(0, 2) * -1));
        rb.MovePosition(transform.position + (transform.forward * (Time.time - lastBeatTime)));
    }

    private void SpiralMove()
    {
        //float angularVelocity = 1f;
        //Vector2 linearVelocity = new Vector2(0.5f, 0f);

        //float angle = angularVelocity * (Time.time - lastBeatTime);
        //Vector3 position = linearVelocity * Time.deltaTime * moveDistance;// (Time.time - lastBeatTime);
        //position += new Vector3(Mathf.Sin(angle) * radii.x, 0, Mathf.Cos(angle) * radii.y);
        //rb.MovePosition(position);


        _center += Velocity * (Time.time - lastBeatTime);// Time.deltaTime;

        _angle += RotateSpeed * (Time.time - lastBeatTime);// Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle)) * Radius;

        transform.position = _center + offset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.player.GetComponent<Player>().enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    //private IEnumerator MoveInACircle()
    //{
    //    if (isActing) yield break;

    //    isActing = true;

    //    float timer = 0;
    //    Vector2 radii = new Vector2(2f, 3f);
    //    float angularVelocity = 1f;
    //    Vector2 linearVelocity = new Vector2(0.5f, 0f);

    //    while(timer < 5)
    //    {
    //        yield return new WaitForEndOfFrame();

    //        float angle = angularVelocity * timer;
    //        Vector2 position = linearVelocity * timer;
    //        position += new Vector2(Mathf.Cos(angle) * radii.x, Mathf.Sin(angle) * radii.y);
    //        transform.position = position;

    //        timer += Time.deltaTime;
    //    }

    //    isActing = false;
    //    yield break;
    //}
}
