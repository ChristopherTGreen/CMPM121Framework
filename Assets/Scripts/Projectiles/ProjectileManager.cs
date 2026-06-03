using UnityEngine;
using System;

public class ProjectileManager : MonoBehaviour
{
    public GameObject[] projectiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.projectileManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateProjectile(int which, string trajectory, Vector3 where, Vector3 direction, float speed, Action<Hittable,Vector3, int, bool> onHit)
    {
        GameObject new_projectile = Instantiate(projectiles[which], where + direction.normalized*1.1f, Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg));
        new_projectile.GetComponent<ProjectileController>().movement = MakeMovement(trajectory, speed);
        new_projectile.GetComponent<ProjectileController>().OnHit += onHit;
    }

    public void CreateProjectile(int which, string trajectory, Vector3 where, Vector3 direction, float speed, Action<Hittable, Vector3, int, bool> onHit, float lifetime, int pierce, int bounce, int damage, float size, bool secondary = false)
    {
        // hardcoded to 0 for projectiles, but if we want different styles of projectiles, change this
        GameObject new_projectile = Instantiate(projectiles[0], where + direction.normalized * 1.1f, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        new_projectile.GetComponent<ProjectileController>().SetSize(size);
        new_projectile.GetComponent<ProjectileController>().movement = MakeMovement(trajectory, speed);
        new_projectile.GetComponent<ProjectileController>().OnHit += onHit;
        new_projectile.GetComponent<ProjectileController>().SetLifetime(lifetime);
        new_projectile.GetComponent<ProjectileController>().SetPierce(pierce);
        new_projectile.GetComponent<ProjectileController>().SetBounce(bounce);
        new_projectile.GetComponent<ProjectileController>().SetDamage(damage);
        new_projectile.GetComponent<ProjectileController>().SetSecondary(secondary);
    }

    public ProjectileMovement MakeMovement(string name, float speed)
    {
        if (name == "straight")
        {
            return new StraightProjectileMovement(speed);
        }
        if (name == "homing")
        {
            return new HomingProjectileMovement(speed);
        }
        if (name == "spiraling")
        {
            return new SpiralingProjectileMovement(speed);
        }
        if (name == "withering")
        {
            return new WitheringProjectileMovement(speed);
        }
        throw new Exception("Projectile Manager: Invalid Projectile Movement");
        return null;
    }

}
