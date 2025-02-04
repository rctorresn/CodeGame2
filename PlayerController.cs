﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static bool playerCreated;

    public float speed = 5.0f;
    

    private bool walking = false;
    private bool attacking = false;
    public Vector2 lastMovement = Vector2.zero;
    

    private const string AXIS_H = "Horizontal", AXIS_V = "Vertical", WALK = "Walking", ATT = "Attacking", LAST_H = "LastH", LAST_V = "LastV" ;
    private Rigidbody2D _rigidbody;
    public string nextUuid;
    private Animator _animator;

    public float attackTime;
    private float attackTimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        playerCreated = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.walking = false;

        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if(attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool(ATT, false);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                attacking = true;
                attackTimeCounter = attackTime;
                _rigidbody.velocity = Vector2.zero;
                _animator.SetBool(ATT, true);
            }
        }

        // spacio = Velocidad * Tiempo
        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
        {
            //Vector3 translation = new Vector3(Input.GetAxisRaw(AXIS_H) * speed * Time.deltaTime, 0, 0);
            //this.transform.Translate(translation);
            _rigidbody.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), _rigidbody.velocity.y).normalized*speed;
            this.walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }

        if (Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {
            //Vector3 translation = new Vector3(0,Input.GetAxisRaw(AXIS_V) * speed * Time.deltaTime, 0);
            //this.transform.Translate(translation);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Input.GetAxisRaw(AXIS_V)).normalized*speed;
            this.walking = true;
            lastMovement = new Vector2(0,Input.GetAxisRaw(AXIS_V));
        }

        

    }

    private void LateUpdate()
    {
        if (!walking)
        {
            _rigidbody.velocity = Vector2.zero;
        }

        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetBool("Walking", walking);
        _animator.SetFloat(LAST_H, lastMovement.x);
        _animator.SetFloat(LAST_V, lastMovement.y);
    }
}
