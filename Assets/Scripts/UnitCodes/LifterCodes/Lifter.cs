using System;
using Script;
using Script.Units.Script;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnitCodes.LifterCodes
{
public class Lifter : MonoBehaviour
{
    public LifterSetting LifterSetting;
    private Pad pad;
    
    
    public Storage allocatedStorage;
    private float _speed;

    public float GetTimePredict(Vector3 p1)
    {
        
    }
    
    public float GetTimePredictAccelToMaxSpeed(Vector3 p1, Vector3 p2, float moveTime = 0)
    {
        float distance = Vector3.Distance(p1, p2);
        float accel = LifterSetting.acceleration;
        float maxSpeed = LifterSetting.maxSpeed;
        float maxSpeedTime = maxSpeed / accel;
        float fullDistance = LifterSetting.GetDistanceAccelToMaxSpeed(moveTime) + distance;
        float maxSpeedDistance = LifterSetting.GetDistanceAccelToMaxSpeed(maxSpeedTime);
        
        if (moveTime <= maxSpeedTime || fullDistance <= maxSpeedDistance)
        {
            return MathF.Sqrt((2 * distance + accel * moveTime * moveTime) / accel);
        }

        if (moveTime <= maxSpeedTime || fullDistance > maxSpeedDistance)
        {
            return (distance + accel * moveTime * moveTime / 2) / maxSpeed;
        }

        if (moveTime > maxSpeedTime || fullDistance > maxSpeedDistance)
        {
            return distance / maxSpeed + moveTime;
        }

        throw new Exception();
    }

    public float GetTimePredict(Vector3 p1, Vector3 p2, float moveTime)
    {
        
    }
    
    public class LifterWorkScript : UnitScript
    {
        private Lifter _lifter;

        public LifterWorkScript(Lifter lifter) => this._lifter = lifter;
        
        public override UnitScript GetDetailScript()
        {
            
        }

        public override ScriptState GetResult()
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class GetPacket : UnitScript
    {
        public override UnitScript GetDetailScript()
        {
            throw new System.NotImplementedException();
        }

        public override ScriptState GetResult()
        {
            throw new System.NotImplementedException();
        }
    }

    public class GoConvey : UnitScript
    {
        private readonly Lifter _lifter;
        
        public GoConvey(Lifter lifter)
        {
            _lifter = lifter;
        }
        
        public override UnitScript GetDetailScript()
        {
            throw new System.NotImplementedException();
        }

        public override ScriptState GetResult()
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class GoWait : UnitScript
    {
        public override UnitScript GetDetailScript()
        {
            throw new System.NotImplementedException();
        }

        public override ScriptState GetResult()
        {
            throw new System.NotImplementedException();
        }
    }
}
}
