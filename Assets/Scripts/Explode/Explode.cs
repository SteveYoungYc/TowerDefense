using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public delegate void ShootEvent(int type, Vector3 pos);
    public static event ShootEvent MissileAction;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var enemy in EnemyManager.EnemiesInRegion(transform.position, 3f))
        {
            MissileAction(2, enemy.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }
}
