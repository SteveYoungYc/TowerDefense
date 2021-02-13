using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float HP;
    public float maxHP = 100f;
    public bool isDead;

    //private HitState state;

    private EnemyIndex enemyIndex;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Camera mainCamera;

    private GameObject target;
    public Vector3 aimPoint;
    
    public delegate void EnemyAttackEvent(int type);
    public static event EnemyAttackEvent AttackEvent;

    private enum HitState
    {
        None,
        Laser,
        Bullet
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        isDead = false;

        //enemyIndex = new EnemyIndex(); 这一条语句后于GetIndex中的
        animator = GetComponent<Animator>();
        animator.SetBool("die", false);
        animator.SetBool("run", true);

        navMeshAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag("Target").gameObject;

        Turret.ShootAction += Hit;

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            AnimationControl();
            Attack();
        }
        else
        {
            StopMove();
            StartCoroutine(Die());
        }
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

    private void StopMove()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    private void AnimationControl()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, target.transform.position));
        animator.SetBool("attack", target.activeInHierarchy);
        if (navMeshAgent.remainingDistance < 0.5f)
        {
            
        }
        else
        {

        }
    }

    private void Hit(int type, Vector3 pos)
    {
        if (pos != transform.position) return;
        if (type == 1)
        {
            HP -= Time.deltaTime * 20;
        }

        if (HP <= 0)
        {
            isDead = true;
            EnemyManager.OneEnemyDie(enemyIndex);
            MoneyManager.KillAnEnemy();
            animator.SetBool("die", true);
            Turret.ShootAction -= Hit;
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }

    public void GetIndex(int i, int j)
    {
        enemyIndex = new EnemyIndex(i, j);
    }

    private void Attack()
    {
        if (target.activeInHierarchy)
        {
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Z_Attack")
            {
                AttackEvent(1);
            }
            else
            {
                AttackEvent(0);
            }
        }
    }
}
