using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SelectTarget : MonoBehaviour
{
    private GameObject barrel;
    private GameObject turretCenter;

    private Ray ray;

    public Vector3 posA;
    public Vector3 posB;

    private List<GameObject> enemiesInSight;
    // Start is called before the first frame update
    void Start()
    {
        barrel = transform.Find("gun barrel").gameObject;
        turretCenter = barrel.transform.Find("Cube").gameObject;
        enemiesInSight = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EnemiesInSight()
    {
        enemiesInSight = new List<GameObject>();
        for (int i = 0; i < EnemyManager.GetEnemies().Count; i++)
        {
            for (int j = 0; j < EnemyManager.GetEnemies()[0].Count; j++)
            {
                if (!EnemyManager.GetEnemyStates()[i][j]) continue;
                posA = turretCenter.transform.position;
                posB = EnemyManager.GetEnemies()[i][j].transform.position;
                ray = new Ray(posA,  posB - posA);
                if (!Physics.Raycast(ray, out var hit, Vector3.Distance(posA, posB), LayerMask.GetMask("Map")))
                {
                    enemiesInSight.Add(EnemyManager.GetEnemies()[i][j]);
                }
                else
                {
                    //enemiesInSight.Remove(EnemyManager.GetEnemies()[i][j]);
                }
            }
        }
    }

    public GameObject MostThreatening()
    {
        EnemiesInSight();
        
        return EnemyManager.MostThreatening(enemiesInSight);
    }
}
