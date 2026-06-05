using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform target;
    public int speed;
    public int damage;
    public Hittable hp;
    public HealthBar healthui;
    public bool dead;

    public float last_attack;

    public Vector3 targetPosition;
    const int distanceBeforeNewTarget = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameManager.Instance.player.transform;
        hp.OnDeath += Die;
        healthui.SetHealth(hp);
        targetPosition = GameManager.Instance.navPointManager.GetClosestNavPoint(transform.position).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = targetPosition - transform.position;

        if (direction.magnitude < 2f)
        {
            DoAttack();
        }
        else
        {
            GetComponent<Unit>().movement = direction.normalized * speed;
        }

        NextTarget();
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
    void NextTarget()
    {
        if (targetPosition == null || (targetPosition - transform.position).sqrMagnitude < distanceBeforeNewTarget)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPosition);
            // did not hit anything, clear goal line to the player
            if (hit.collider != null) targetPosition = GameManager.Instance.player.transform.position;
            else targetPosition = GameManager.Instance.navPointManager.GetNextNavPoint(transform.position);
        }
        // nothing, absolutely nothing except stay on target - Gold Five
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
