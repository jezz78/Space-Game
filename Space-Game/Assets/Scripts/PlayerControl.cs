﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 5f;
    public float min_Y, max_Y;

    [SerializeField]
    private GameObject p_bullet_2;

    [SerializeField]
    private Transform attack_Point;

    public float attack_Timer = 0.35f;
    private float current_Attack_Timer;     //zmienna czasu pomiedzy strzalami
    private bool can_Attack;

    private AudioSource laserAudio;


    void Awake()
    {
        laserAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        current_Attack_Timer = attack_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Attack();
    }

    void MovePlayer()
    {
        if(Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > max_Y)
                temp.y = max_Y;

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < min_Y)
                temp.y = min_Y;

            transform.position = temp;
        }
    }

    void Attack()
    {
        attack_Timer += Time.deltaTime;     //czas pomiedzy strzalami
        if(attack_Timer > current_Attack_Timer)
        {
            can_Attack = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (can_Attack)
            {
                can_Attack = false;
                attack_Timer = 0f;          //reset zmiennej czasu
                Instantiate(p_bullet_2, attack_Point.position, Quaternion.identity);

                laserAudio.Play();
            }
            
        }
    }
}
