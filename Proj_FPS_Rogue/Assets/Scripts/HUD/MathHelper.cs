using UnityEngine;

public static class MathHelper // TODO where to put this?
{
    public static float SignedAngle(float a, float b)
    {
        var difference = b - a;
        while (difference < -180) difference += 360;
        while (difference > 180) difference -= 360;
        return difference;
    }

    public static float DamageAngle(Transform from, Transform to)
    {
        //calculate damage angle from projectile to player
        return Vector3.Angle(from.forward, to.forward);
    }
}