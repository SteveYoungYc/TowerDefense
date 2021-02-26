using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static float moneySum;

    private const float LaserTurretCost = 200f;
    
    private const float MissileSiloCost = 500f;
    
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

    public static void BuildATurret(int type)
    {
        switch (type)
        {
            case 0: moneySum -= LaserTurretCost; break;
            case 1: moneySum -= MissileSiloCost; break;
        }
    }

    public static void KillAnEnemy()
    {
        moneySum += EnemyEarn;
    }

    public static float GetMoney()
    {
        return moneySum;
    }

    public static bool Affordable(int type)
    {
        switch (type)
        {
            case 0: return moneySum >= LaserTurretCost;
            case 1: return moneySum >= MissileSiloCost;
            default: return false;
        }
    }
}
