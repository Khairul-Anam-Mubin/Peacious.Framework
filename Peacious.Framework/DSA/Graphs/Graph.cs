namespace Peacious.Framework.DSA.Graphs;

public class Graph
{
    private int _numberOfNodes;
    private List<List<int>> _adjacencyList;
    private Dictionary<int, int> _outDegree;

    public Graph(int numberOfNodes)
    {
        _numberOfNodes = numberOfNodes + 1;
        _adjacencyList = new List<List<int>>(_numberOfNodes);
        _outDegree = new();

        // Initialize each node's adjacency list as an empty list
        for (int i = 0; i < _numberOfNodes; i++)
        {
            _adjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int u, int v)
    {
        if (u >= _numberOfNodes || v >= _numberOfNodes || u < 0 || v < 0)
        {
            Console.WriteLine($"Invalid edge ({u} -> {v}). Node out of bounds.");
            return;
        }
        
        if (_outDegree.ContainsKey(u))
        {
            _outDegree[u]++;
        }
        else
        {
            _outDegree.Add(u, 1);
        }

        _adjacencyList[u].Add(v);
    }

    public bool RemoveEdge(int u, int v)
    {
        if (u >= _numberOfNodes || v >= _numberOfNodes || u < 0 || v < 0)
        {
            Console.WriteLine($"Invalid edge ({u} -> {v}). Node out of bounds.");
            return false;
        }

        var isRemoved = _adjacencyList[u].Remove(v);

        if (isRemoved)
        {
            if (_outDegree.ContainsKey(u))
            {
                _outDegree[u]--;
            }
            else
            {
                throw new Exception("No outDegree but edge removed. Inconsitant state");
            }
        }

        return isRemoved;
    }

    public void Dfs(int u, Dictionary<int, bool> isVisited)
    {
        if (isVisited.ContainsKey(u) && isVisited[u]) return;

        isVisited.Add(u, true);

        foreach (var v in _adjacencyList[u])
        {
            if (isVisited.ContainsKey(v) && isVisited[v]) continue;

            Dfs(v, isVisited);
        }
    }

    public List<int> GetVisitedNodes(int u)
    {
        var visitedNodes = new List<int>();
        var isVisited = new Dictionary<int, bool>();

        Dfs(u, isVisited);

        foreach (var kv in isVisited)
        {
            if (kv.Value)
            {
                visitedNodes.Add(kv.Key);
            }
        }

        return visitedNodes;
    }

    public bool IsLeafNode(int u)
    {
        if (_outDegree.ContainsKey(u))
        {
            return _outDegree[u] == 0;
        }

        return true;
    }

    public void Print()
    {
        for (int u = 0; u < _numberOfNodes; u++)
        {
            if (_adjacencyList[u] == null || _adjacencyList[u].Count == 0)
            {
                continue;
            }

            Console.Write($"Node {u} : ");

            foreach (var v in _adjacencyList[u])
            {
                Console.Write($"{v}, ");
            }

            Console.WriteLine();
        }
    }
}