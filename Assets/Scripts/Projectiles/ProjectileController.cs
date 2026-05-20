using UnityEngine;
using System;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public float lifetime;
    public int pierceAmount = 1;
    public int bounceAmount = 0;
    public event Action<Hittable,Vector3> OnHit;
    public ProjectileMovement movement;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.Movement(transform);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile")) return;
        if (collision.gameObject.CompareTag("unit"))
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            if (ec != null)
            {
                //Debug.Log("Damage loop");
                //Debug.Log(ec.hp.hp);
                OnHit(ec.hp, transform.position);
                //Debug.Log(ec.hp.hp);
                pierce();
                while (ec.hp.hp > 0 && bounceAmount > 0 && pierceAmount > 0)
                {
                    //Debug.Log(ec.hp.hp);
                    OnHit(ec.hp, transform.position);
                    pierce();

                }
                
            }
            else
            {
                var pc = collision.gameObject.GetComponent<PlayerController>();
                if (pc != null)
                {
                    OnHit(pc.hp, transform.position);
                }
            }


            if (bounceAmount > 0) bounce();
        }

        

        if (pierceAmount <= 0) Destroy(gameObject);
    }
    public void pierce()
    {
        pierceAmount -= 1;
    }
    public void bounce()
    {
        GameObject closest = GameManager.Instance.GetClosestEnemy(transform.position);
        if (closest == null) return;
        // original angle and direction

        Vector3 new_direction = (closest.transform.position - transform.position).normalized;


        //new_direction = new Vector3(Mathf.Cos(new_angle), Mathf.Sin(new_angle), 0);
        transform.right = new_direction;
        
        
    }

    public void SetLifetime(float lifetime)
    {
        StartCoroutine(Expire(lifetime));
    }
    public void SetPierce(int pierce)
    {
        pierceAmount = pierce;
    }
    public void SetBounce(int bounce)
    {
        bounceAmount = bounce;
    }

    IEnumerator Expire(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
