using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

public class WitheringProjectileMovement : ProjectileMovement
{
    private readonly float shrinkPerSecond;
    private readonly float minScale;

    public WitheringProjectileMovement(float speed, float shrinkPerSecond = 0.6f, float minScale = 0.15f)
        : base(speed)
    {
        this.shrinkPerSecond = shrinkPerSecond;
        this.minScale = minScale;
    }

    public override void Movement(Transform transform)
    {
        // Move like straight projectile
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.Self);

        // Shrink over time
        float factor = 1f - shrinkPerSecond * Time.deltaTime;
        factor = Mathf.Clamp(factor, 0.01f, 1f);

        Vector3 s = transform.localScale * factor;
        float clampedX = Mathf.Max(minScale, s.x);
        float clampedY = Mathf.Max(minScale, s.y);
        float clampedZ = Mathf.Max(minScale, s.z);

        transform.localScale = new Vector3(clampedX, clampedY, clampedZ);
    }
}