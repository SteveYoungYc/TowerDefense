using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AnimatorController animatorController;
    
    private static List<List<GameObject>> Enemies {set;get;}
    private static List<List<bool>> enemyStates;
    
    private static List<Vector3> bornPoints;
    
    private static int enemyNum = 20;

    private static Vector3 destination;

    private static Dictionary<EnemyIndex, float> distanceList;
    
    // Start is called before the first frame update
    void Awake()
    {
        Enemies = new List<List<GameObject>>();
        enemyStates = new List<List<bool>>();
        bornPoints = new List<Vector3>();
        distanceList = new Dictionary<EnemyIndex, float>();

        destination = GameObject.FindWithTag("Target").gameObject.transform.position;
        bornPoints.Add(new Vector3(-25f, 2f, -40f));
        //bornPoints.Add(new Vector3(86.16f, 2.99f, 22.39f));
        //bornPoints.Add(new Vector3(44.11f, 3.17f, 23.33f));
        //bornPoints.Add(new Vector3(92.40f, 3.17f, -20.53f));

        for (int i = 0; i < bornPoints.Count; i++)
        {
            Enemies.Add(new List<GameObject>());
            enemyStates.Add(new List<bool>());
            for (int j = 0; j < enemyNum; j++)
            {
                Enemies[i].Add(Instantiate(enemyPrefab, bornPoints[i], Quaternion.identity));
                Enemies[i][j].tag = "Enemy";
                Enemies[i][j].AddComponent<NavMeshAgent>();
                Enemies[i][j].AddComponent<Enemy>();
                Enemies[i][j].GetComponent<Enemy>().GetIndex(i, j);
                Enemies[i][j].GetComponent<Animator>().runtimeAnimatorController = animatorController;
                Enemies[i][j].GetComponent<NavMeshAgent>().destination = destination;
                
                
                enemyStates[i].Add(true);
                
                distanceList.Add(new EnemyIndex(i, j), Vector3.Distance(Enemies[i][j].transform.position, destination));
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
        return Enemies[index.Key.i][index.Key.j];
    }
    
    public static GameObject MostThreatening(List<GameObject> objects)
    {
        if (objects.Count == 0) return null;
        Dictionary<int, float> distances = new Dictionary<int, float>();
        for (int i = 0; i < objects.Count; i++)
        {
            //distances.Add(i, objects[i].GetComponent<NavMeshAgent>().remainingDistance);
            distances.Add(i, Vector3.Distance(objects[i].transform.position, destination));
        }
        return objects[distances.OrderBy(kvp => kvp.Value).First().Key];
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
                distanceList.Add(new EnemyIndex(i, j), Vector3.Distance(Enemies[i][j].transform.position, destination));
            }
        }
    }

    public static List<List<GameObject>> GetEnemies()
    {
        return Enemies;
    }

    public static List<List<bool>> GetEnemyStates()
    {
        return enemyStates;
    }
}