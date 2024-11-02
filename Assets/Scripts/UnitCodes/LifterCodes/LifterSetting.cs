public class LifterSetting
{
    public readonly float maxSpeed;
    public readonly float RotateSpeed;
    public readonly float acceleration;
    public readonly float breakPower;

    public readonly float padDorpSpeed;

    public float QuarterRotateTime => 90 / RotateSpeed;
    
    public float GetStartSpeed(float time) // 가속 상황의 속도 함수 a(x)
    {
        float speed = acceleration * time;
        return speed < maxSpeed ? speed : maxSpeed;
    }

    public float GetStopSpeed(float currSpeed, float breakTime) // 브레이크 상황의 속도 함수 b(x)
    {
        float speed = currSpeed - breakPower * breakTime;
        return speed > 0 ? speed : 0;
    }

    public float GetDistanceAccelToMaxSpeed(float moveTime) //a(x) 적분 A(x)
    {
        float maxSpeedTime = maxSpeed / acceleration;

        if (moveTime <= maxSpeedTime) return acceleration * moveTime * moveTime / 2;
        return maxSpeed * moveTime - acceleration * maxSpeedTime * maxSpeedTime / 2;
    }

    public float GetDistanceAccelAndStop(float moveTime)
    {
        
    }
}