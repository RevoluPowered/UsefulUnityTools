using UnityEngine;

public class QuaternionMath
{
    /// <summary>
    /// This is designed to limit a specific axis within certain parameters to be used when setting exact rotation.
    /// Notes:
    /// The min max angles are the limiting angles,
    /// The axis specified should be normalised as pitch, yaw or roll.
    /// the current ref is the current tracking point, this is used to accurately handle the value on the next tick.
    /// </summary>
    /// <param name="rotation"></param>
    /// <param name="current"></param>
    /// <param name="movement"></param>
    /// <param name="axis"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Quaternion CalculateInLimits( Quaternion rotation, ref float current, float movement, Vector3 axis, float min = -70, float max= 60)
    {
        // Current movement clamped to the min max, to stop people hitting max movement in a single tick.
        movement = Mathf.Clamp(movement, min, max);
        current += movement;

        // Clamp value / axist movement between these values.
        float unclamped = current;
        current = Mathf.Clamp(current, min, max);

        // Calculate reduction rate.
        float reductionRate = (unclamped - current);

        // Calculate euler rotation based on the axis in use, please restrict values to [1,0,0] [0,1,0] and [0,0,1] otherwise it might not work within proper parameters.
        Quaternion EulerRot = Quaternion.Euler((movement - reductionRate) * axis.x, (movement - reductionRate) * axis.y, (movement - reductionRate) * axis.z);

        // Rotate the camera on the pitch axis, relative to the players head rotation.
        return Quaternion.Slerp(rotation, rotation * EulerRot, Time.time);
    }
}