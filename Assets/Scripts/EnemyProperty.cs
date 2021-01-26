using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyProperty : MonoBehaviour
{
    public float HP;

    private HitState state;

    private EnemyIndexInfo enemyIndexInfo;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Camera mainCamera;
    private enum HitState
    {
        None,
        Laser,
        Bullet
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = 100f;
        
        enemyIndexInfo = new EnemyIndexInfo();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            EnemyGenerator.OneEnemyDie(enemyIndexInfo);
            Destroy(gameObject);
        }
        
        //if (state == HitState.Laser)
        //{
            //HP -= Time.deltaTime * 20;
        //}

        Move();
        AnimationControl();
    }

    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map"));
            if (isCollider)
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    private void AnimationControl()
    {
        animator.SetBool("startRun", !(Vector3.Distance(transform.position, navMeshAgent.destination) < 1));
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
