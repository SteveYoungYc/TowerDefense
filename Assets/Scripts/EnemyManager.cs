using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AnimatorController animatorController;
    
    private static List<List<GameObject>> enemies;
    private static List<List<bool>> enemyStates;
    
    private static List<Vector3> bornPoints;
    
    private static int enemyNum = 20;

    private static Vector3 destination;

    private static Dictionary<EnemyIndex, float> distanceList;
    
    // Start is called before the first frame update
    void Awake()
    {
        enemies = new List<List<GameObject>>();
        enemyStates = new List<List<bool>>();
        bornPoints = new List<Vector3>();
        distanceList = new Dictionary<EnemyIndex, float>();

        destination = new Vector3(-24.83f, 0.99f, -58.83f);
        bornPoints.Add(new Vector3(86.16f, 2.99f, 22.39f));
        //bornPoints.Add(new Vector3(44.11f, 3.17f, 23.33f));
        //bornPoints.Add(new Vector3(92.40f, 3.17f, -20.53f));

        for (int i = 0; i < bornPoints.Count; i++)
        {
            enemies.Add(new List<GameObject>());
            enemyStates.Add(new List<bool>());
            for (int j = 0; j < enemyNum; j++)
            {
                enemies[i].Add(Instantiate(enemyPrefab, bornPoints[i], Quaternion.identity));
                enemies[i][j].tag = "Enemy";
                enemies[i][j].AddComponent<NavMeshAgent>();
                enemies[i][j].AddComponent<Enemy>();
                
                enemies[i][j].GetComponent<Enemy>().GetIndex(i, j);
                enemies[i][j].GetComponent<Animator>().runtimeAnimatorController = animatorController;
                enemies[i][j].GetComponent<NavMeshAgent>().destination = destination;
                
                enemyStates[i].Add(true);
                
                distanceList.Add(new EnemyIndex(i, j), Vector3.Distance(enemies[i][j].transform.position, destination));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void OneEnemyDie(EnemyIndex index)
    {
        enemyStates[index.i][index.j] = false;
    }

    public static GameObject MostThreatening()
    {
        UpdateDistance();
        if (distanceList.Count == 0) return null;
        var index = distanceList.OrderBy(kvp => kvp.Value).First();
        return enemies[index.Key.i][index.Key.j];
    }

    private static void UpdateDistance()
    {
        distanceList = new Dictionary<EnemyIndex, float>();
        for (int i = 0; i < bornPoints.Count; i++)
        {
            for (int j = 0; j < enemyNum; j++)
            {
                if (!enemyStates[i][j])
                {
                    continue;
                }
                distanceList.Add(new EnemyIndex(i, j), Vector3.Distance(enemies[i][j].transform.position, destination));
            }
        }
    }
}