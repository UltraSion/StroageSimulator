using System.Collections.Generic;

namespace PathCodes
{
public class Path
{
    public readonly LinkedList<PathNode> PathNodes;

    public IEnumerator<PathNode> GetEnumerator() => PathNodes.GetEnumerator();
    
    public float Time { get; }
}
}