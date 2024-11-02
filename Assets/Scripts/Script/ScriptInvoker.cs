using System.Collections.Generic;

namespace Script.Units.Script
{
public class ScriptInvoker
{
    private Queue<ScriptsSet> currentFrameQueue = new();

    private Queue<ScriptsSet> nextFrameQueue = new();

    public void ExecuteCommands()
    {
        ScriptsSet scriptsSet;
        while (currentFrameQueue.Count != 0)
        {
            scriptsSet = currentFrameQueue.Dequeue();
            if (scriptsSet.Exe() == ScriptState.Continue) nextFrameQueue.Enqueue(scriptsSet);
        }

        currentFrameQueue = nextFrameQueue;
        nextFrameQueue = new Queue<ScriptsSet>();
    }

    public void AddCurFrameCommand(ScriptsSet scripts) => currentFrameQueue.Enqueue(scripts);
}
}