using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AnimatorController animatorController;

    private GameObject[] enemyClone;

    private int enemyNum = 3;

    private Animator[] enemyAnimator;

    private NavMeshAgent nav;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        enemyClone = new GameObject[enemyNum];
        enemyAnimator = new Animator[enemyNum];
        enemyClone[0] = Instantiate(enemyPrefab, new Vector3(0, -0.1f, 0), Quaternion.identity);
        enemyClone[1] = Instantiate(enemyPrefab, new Vector3(2, -0.1f, 0), Quaternion.identity);
        enemyClone[2] = Instantiate(enemyPrefab, new Vector3(4, -0.1f, 0), Quaternion.identity);
        for (int i = 0; i < enemyNum; i++)
        {
            enemyAnimator[i] = enemyClone[i].GetComponent<Animator>();
            enemyAnimator[i].runtimeAnimatorController = animatorController;
            enemyClone[i].AddComponent<NavMeshAgent>();
        }

        mainCamera = Camera.main;
        //nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPath();
        if (Input.GetMouseButton(1)) //右键
        {
            //enemyAnimator[0].SetBool("startRun", true);
        }
        else
        {
            //enemyAnimator[0].SetBool("startRun", false);
        }
    }

    private void FindPath()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map"));
            if (isCollider)
            {
                enemyClone[0].GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }
        }
        enemyAnimator[0].SetBool("startRun",
            !(Vector3.Distance(enemyClone[0].transform.position,
                enemyClone[0].GetComponent<NavMeshAgent>().destination) < 0.1));
    }
}