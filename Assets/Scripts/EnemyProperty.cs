using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyProperty : MonoBehaviour
{
    public float HP;

    private HitState state;

    private EnemyIndexInfo enemyIndexInfo;

    private enum HitState
    {
        None,
        Laser,
        Bullet
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyIndexInfo = new EnemyIndexInfo();
        HP = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            EnemyGenerator.OneEnemyDie(enemyIndexInfo);
            //Destroy(gameObject);
        }
        
        //if (state == HitState.Laser)
        //{
            HP -= Time.deltaTime * 50;
        //}
    }

    private bool IsDead()
    {
        return HP <= 0;
    }

    public void GetInfo(int i, int j)
    {
        enemyIndexInfo.i = i;
        enemyIndexInfo.j = j;
        //numInfo.bornPointsNum = bornPointsNum;
        //numInfo.enemyPointsNum = enemyNum;
    }

    private EnemyIndexInfo DestroyEnemy()
    {
        return enemyIndexInfo;
    }
}
