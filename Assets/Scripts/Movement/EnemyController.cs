using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform target;
    public int speed;
    public int damage;
    public Hittable hp;
    public HealthBar healthui;
    public bool dead;

    private enum state
    {
        inSight,
        outSight
    }
    private state enemyState = state.outSight;

    public float last_attack;

    public Vector3 targetPosition;
    const int distanceBeforeNewTarget = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameManager.Instance.player.transform;
        hp.OnDeath += Die;
        healthui.SetHealth(hp);
        targetPosition = GameManager.Instance.navPointManager.GetClosestNavPoint(transform.position).transform.position;
        //Debug.Log(transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = targetPosition - transform.position;
        Vector3 distanceToTarget = GameManager.Instance.player.transform.position - transform.position;

        if (distanceToTarget.magnitude < 2f)
        {
            DoAttack();
        }
        else
        {

            GetComponent<Unit>().movement = direction.normalized * speed;
        }

        NextTarget(direction);
    }
    
    void DoAttack()
    {
        if (last_attack + 2 < Time.time)
        {
            last_attack = Time.time;
            // warning assumes this target is player
            PlayerController targetObject = target.gameObject.GetComponent<PlayerController>();
            targetObject.hp.Damage(new Damage(5, Damage.Type.PHYSICAL));
            //Debug.Log(targetObject);
            EventBus.Instance.DoDamageTaken(target.transform.position, targetObject);
        }
    }
    // update for getting a new target position, if need be
    void NextTarget(Vector3 direction)
    {
        switch (enemyState) 
        {
            case state.inSight:
                inSightCheck(direction);
                break;
            case state.outSight:
                outSightCheck(direction);
                break;
        }
    }

    void inSightCheck(Vector3 direction)
    {
        Vector3 safeStartPos = transform.position + (direction.normalized * 1.0f);

        Vector3 actualTargetPosition = GameManager.Instance.player.transform.position;

        RaycastHit2D hit = Physics2D.Linecast(safeStartPos, target.position, LayerMask.GetMask("Non-AI Default"));
        //Debug.DrawLine(safeStartPos, target.position, Color.red);

        if (hit.collider == null || !hit.collider.CompareTag("unit"))
        {
            enemyState = state.outSight;
            targetPosition = GameManager.Instance.navPointManager.GetNextNavPoint(transform.position);
            return;
        }
        else
        {
            targetPosition = GameManager.Instance.player.transform.position;
            return;
        }
    }

    void outSightCheck(Vector3 direction)
    {

        Vector3 safeStartPos = transform.position + (direction.normalized * 1.0f);

        Vector3 actualTargetPosition = GameManager.Instance.player.transform.position;

        if (targetPosition == null || (targetPosition - transform.position).sqrMagnitude < distanceBeforeNewTarget)
        {
            //Debug.Log(targetPosition);
            //Debug.Log("getting next nav point");
            targetPosition = GameManager.Instance.navPointManager.GetNextNavPoint(transform.position);
            //Debug.Log(targetPosition);
            RaycastHit2D hit = Physics2D.Linecast(safeStartPos, target.position, LayerMask.GetMask("Non-AI Default"));
            //Debug.DrawLine(safeStartPos, target.position, Color.red);

            if (hit.collider != null && !hit.collider.CompareTag("World") && hit.collider.CompareTag("unit"))
            {
                enemyState = state.inSight;
                targetPosition = GameManager.Instance.player.transform.position;
                return;

            }
        }
    }


    void Die()
    {
        if (!dead)
        {
            // event calls
            hp.OnDeath += Die;

            EventBus.Instance.DoKill(target.position, target.gameObject.GetComponent<PlayerController>());

            GameManager.Instance.sessionStats.enemiesKilled += 1;
        
            dead = true;
            GameManager.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }
}
