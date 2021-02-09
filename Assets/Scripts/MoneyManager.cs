using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static float moneySum;

    private const float TurretCost = 200f;
    private const float EnemyEarn = 20f;
    // Start is called before the first frame update
    void Start()
    {
        moneySum = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void BuildATurret()
    {
        moneySum -= TurretCost;
    }

    public static void KillAnEnemy()
    {
        moneySum += EnemyEarn;
    }
}
