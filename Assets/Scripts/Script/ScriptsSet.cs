using System;
using System.Collections.Generic;

namespace Script.Units.Script
{
public class ScriptsSet
{
    private readonly Stack<UnitScript> CommandStack = new Stack<UnitScript>();

    protected int Count => CommandStack.Count;

    public Func<ScriptState> Exe;

    public ScriptsSet() => Exe = Prepare;

    public void SeedScript(UnitScript script)
    {
        Exe = Prepare;
        CommandStack.Push(script);
    }

    private ScriptState Prepare()
    {
        var needTo = CommandStack.Peek().GetDetailScript();
        if (needTo is not null) CommandStack.Push(needTo);
        else Exe = Execute;

        return Exe();
    }

    protected virtual ScriptState Execute()
    {
        var command = CommandStack.Peek();
        var result = command.GetResult();

        if (result is ScriptState.Continue or ScriptState.Terminate) return result;

        Exe = Prepare;
        CommandStack.Pop();
        return CommandStack.Count == 0 ? ScriptState.End : ScriptState.Continue;
    }

    public void Start()
    {
        // Debug.Log("start");
        Hub.ScriptInvoker.AddCurFrameCommand(this);
    }

    public void Terminate() => CommandStack.Push(new TerminateScript());
}

// public class LifterSciptSet : ScriptsSet
// {
//     private readonly Lifter worker;
//
//     private readonly UnitScript order;
//
//     public LifterSciptSet(Lifter worker, UnitScript order)
//     {
//         this.worker = worker;
//         this.order = order;
//     }
//
//     protected override ScriptState Execute()
//     {
//         Worker worker = processor as Worker;
//
//         if (worker.CalledNow)
//         {
//             Hub.WorkerManager.AddOrder(order);
//             return ScriptState.Terminate;
//         }
//
//         var result = base.Execute();
//         if (result == ScriptState.End)
//         {
//             Hub.WorkerManager.AddWorker(worker);
//         }
//
//         return result;
//     }
// }
}