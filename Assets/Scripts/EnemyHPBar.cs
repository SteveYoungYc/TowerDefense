using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    private Camera mainCamera;
    private Transform enemyClone;
    private Enemy enemy;
    private Slider slider;

    private void Start()
    {
        mainCamera = Camera.main;
        enemyClone = transform.parent;
        enemy = enemyClone.GetComponent<Enemy>();
        slider = transform.GetChild(0).gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        transform.LookAt(mainCamera.transform);
        slider.value = enemy.HP / enemy.maxHP;
    }
}