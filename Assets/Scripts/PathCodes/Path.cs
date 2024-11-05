using System;
using System.Collections.Generic;
using UnitCodes.LifterCodes;
using UnityEngine;

namespace PathCodes
{
public class Path
{
    public Vector3 MoveDirection;
    public Vector3 RotateDirection;

    private LifterSetting _setting;
    
    public float StartTime;
    public float EndTime;

    public Vector3 from;
    public Vector3 dest;

    public Path(Vector3 from, Vector3 moveDirection, float startTime, LifterSetting setting)
    {
        this.MoveDirection = moveDirection;
        this.from = from;
        this.StartTime = startTime;
        this._setting = setting;
    }

    public Vector3 GetPositionAtTime(float time) 
        => from + MoveDirection * _setting.GetPositionAtTime(Vector3.Distance(from, dest), time);

    public Path CompletePath(Vector3 destPosition)
    {
        Path newPath = new Path(
            from,
            MoveDirection,
            StartTime,
            _setting
        );
        newPath.dest = destPosition;
        newPath.EndTime = StartTime + _setting.GetTimeAccelAndStop(from, destPosition);
        return newPath;
    }
}
}