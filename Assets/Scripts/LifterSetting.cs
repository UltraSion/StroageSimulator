using System;
using UnityEngine;

namespace UnitCodes.LifterCodes
{
public class LifterSetting
{
    public float maxSpeed;
    public float RotateSpeed;
    public float acceleration;
    public float brakePower;
    public float packageDorpSpeed;

    public float radius;

    public float QuarterRotateTime => 90 / RotateSpeed;
    public float EnterTime => (float)(0.5 - radius) / maxSpeed;
    public float LeftTime => (float)(0.5 + radius) / maxSpeed;

    public float TimeOfAccelToMaxSpeed
        => maxSpeed / acceleration;

    public float TimeOfBrakeFromMaxSpeed
        => maxSpeed / brakePower;

    public float DistanceOfAccelToMaxSpeed
        => maxSpeed * TimeOfAccelToMaxSpeed / 2;

    public float DistanceOfBrakeFromMaxSpeed
        => maxSpeed * TimeOfBrakeFromMaxSpeed / 2;

    public float TimeOfAccelToMaxAndBrakeFromMaxSpeed
        => TimeOfAccelToMaxSpeed + TimeOfBrakeFromMaxSpeed;

    public float DistanceOfAccelToMaxAndBrakeFromMaxSpeed
        => DistanceOfAccelToMaxSpeed + DistanceOfBrakeFromMaxSpeed;

    public float GetTimeAccelAndStop(Vector3 from, Vector3 dest)
        => GetTimeAccelAndStop(Vector3.Distance(from, dest));

    public float GetTimeAccelAndStop(float distance)
    {
        float accelBrakeDistance = DistanceOfAccelToMaxAndBrakeFromMaxSpeed;
        if (distance <= accelBrakeDistance)
        {
            float a = acceleration;
            float b = brakePower;
            float d = distance;
            float tq = MathF.Sqrt(2 * b * d / (a * b + a * a));
            return tq * (1 + a / b);
        }

        return TimeOfAccelToMaxSpeed + (distance - accelBrakeDistance) / maxSpeed + TimeOfBrakeFromMaxSpeed;
    }

    private float GetDistanceOfAccelTime(float time)
    {
        if (time > TimeOfAccelToMaxSpeed)
            throw new Exception("outOfRange");
        
        return 0.5f * acceleration * time * time;
    }

    public float GetDistanceOfBrakeTime(float speed, float time)
    {
        float maxT = speed / brakePower;
        if (time > maxT)    
            return -0.5f * brakePower * maxT * maxT + speed * maxT;
        return -0.5f * brakePower * time * time + speed * time;
    }

    public float GetPositionAtTime(float distance, float time)
    {
        float t = time;

        float m = maxSpeed;
        float a = acceleration;
        float b = brakePower;
        float d = distance;

        if (d <= DistanceOfAccelToMaxAndBrakeFromMaxSpeed)
        {
            float tq = MathF.Sqrt(2 * b * d / (a * b + a * a));
            if (t <= tq)
                return GetDistanceOfAccelTime(t);
            else
            {
                float accelDistance = GetDistanceOfAccelTime(tq);
                float brakeDistance = GetDistanceOfBrakeTime(acceleration * tq, t - tq);
                
                return accelDistance + brakeDistance;
            }
        }
        else
        {
            float accelEndTime = TimeOfAccelToMaxSpeed;
            float brakeStartTime = (m / 2) * (1 / a - 1 / b) + b / m;
            float fullAccelTime = brakeStartTime - accelEndTime;
            return DistanceOfAccelToMaxSpeed + m * fullAccelTime + GetDistanceOfBrakeTime(m, time - brakeStartTime);
        }
    }

    public float GetRotateTime(Vector3 d1, Vector3 d2)
    {
        if (d1 == d2)
            return 0;

        if (d1 + d2 == Vector3.zero)
            return QuarterRotateTime * 2;

        return QuarterRotateTime;
    }

    public float GetTimeOfMoveDistanceMaxSpeed(float distance)
        => distance / maxSpeed;
}
}