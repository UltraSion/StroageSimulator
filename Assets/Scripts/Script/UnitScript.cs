using System;
using PathCodes;
using UnitCodes.LifterCodes;
using UnityEngine;

namespace Script
{
public abstract class UnitScript // : IPawnCommand
{
    public abstract UnitScript GetDetailScript();

    public abstract ScriptState GetResult();
}

public class TerminateScript : UnitScript
{
    public override UnitScript GetDetailScript() => null;

    public override ScriptState GetResult() => ScriptState.Terminate;
}

public class RestartScript : UnitScript
{
    public override UnitScript GetDetailScript() => null;

    public override ScriptState GetResult() => ScriptState.End;
}

// public class MoveScript : UnitScript
// {
//     private readonly Lifter lifter;
//     private readonly Vector3 dest;
//     private readonly float speed;
//
//     public MoveScript(Lifter lifter, Vector3 dest, float speed)
//     {
//         this.lifter = lifter;
//         this.dest = dest;
//         this.speed = speed;
//     }
//
//     public override UnitScript GetDetailScript() => null;
//
//     public override ScriptState GetResult()
//     {
//         lifter.Position = Vector3.MoveTowards(lifter.Position, dest, Hub.CurrentFrameInfo.deltaTime * speed);
//         return lifter.Position != dest ? ScriptState.Continue : ScriptState.End;
//     }
// }
//
// public class FollowPathScript : UnitScript
// {
//     private readonly Unit unit;
//     private readonly float speed;
//     private readonly UnitPath path;
//
//     public FollowPathScript(Unit unit, UnitPath path)
//     {
//         if (unit.UnitDef is not UnitDefMovable)
//         {
//             Debug.Log(unit.UnitDef.TypeName + "Can not Move");
//             return;
//         }
//
//         this.unit = unit;
//         this.speed = ((UnitDefMovable)(unit.UnitDef)).Speed;
//         this.path = path;
//     }
//
//     public override UnitScript GetDetailScript()
//     {
//         PathNode pos = path.GetNextNode();
//         if (pos == null) return null;
//         return new MoveScript(unit, pos.position, speed);
//     }
//
//     public override ScriptState GetResult() => ScriptState.End;
// }
//
//
// public class WaitScript : UnitScript
// {
//     private readonly Timer timer;
//     private readonly Action todo;
//     private readonly Action toExecute;
//
//     public WaitScript(float goalTime, Action toExecute = null)
//     {
//         timer = new Timer(goalTime);
//         this.toExecute = toExecute;
//     }
//
//     public WaitScript(float goalTime, Action todo, Action toExecute)
//     {
//         timer = new Timer(goalTime);
//         this.todo = todo;
//         this.toExecute = toExecute;
//     }
//
//     public override UnitScript GetDetailScript() => null;
//
//     public override ScriptState GetResult()
//     {
//         timer.UpdateTime(Hub.CurrentFrameInfo.deltaTime);
//         todo?.Invoke();
//         
//         if (!timer.IsCompleted)
//         {
//             return ScriptState.Continue;
//         }
//
//         toExecute?.Invoke();
//         return ScriptState.End;
//     }
// }
}