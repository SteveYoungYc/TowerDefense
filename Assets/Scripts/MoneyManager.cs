using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static float moneySum;

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

    public static float GetMoney()
    {
        return moneySum;
    }

    public static bool Affordable()
    {
        return moneySum >= TurretCost;
    }
}
