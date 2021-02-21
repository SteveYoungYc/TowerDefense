﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float HP { get; private set; }
    public readonly float maxHP = 100f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-25, 2, -60);
        HP = maxHP;
        Enemy.AttackEvent += Hit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Hit(int type)
    {
        if (type == 1)
        {
            HP -= Time.deltaTime * 5;
        }

        if (HP <= 0)
        {
            gameObject.SetActive(false);
            Enemy.AttackEvent -= Hit;
        }
    }
}
