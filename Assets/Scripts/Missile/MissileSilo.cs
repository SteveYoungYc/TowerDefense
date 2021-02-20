using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MissileSilo : MonoBehaviour
{
    public GameObject missilePrefab;
    private GameObject missileObj;
    private Missile missile;
    private float shootCDTime = 5;

    private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Fire()
    {
        while (true)
        {
            if (EnemyManager.MostThreatening() != null)
            {
                targetPos = EnemyManager.MostThreatening().transform.position;
                shootCDTime = 5;
            }
            else
            {
                shootCDTime = float.MaxValue;
            }
            missileObj = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missileObj.transform.parent = transform;
            missile = missileObj.GetComponent<Missile>();
            missile.SetTarget(targetPos);
            
            yield return new WaitForSeconds(shootCDTime);
        }
    }
}
