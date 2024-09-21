namespace Peacious.Framework.DSA.Graphs;

public class Graph
{
    private int _maxN;
    private List<List<int>> _adjacencyList;
    
    public Graph(int maxN)
    {
        _maxN = maxN + 1;
        _adjacencyList = new List<List<int>>(_maxN);
    }

    public void AddEdge(int u, int v)
    {
        if (_adjacencyList[u] is null) _adjacencyList[u] = new List<int>();
        
        _adjacencyList[u].Add(v);
    }

    public bool RemoveEdge(int u, int v)
    {
        if (_adjacencyList[u] is null) _adjacencyList[u] = new List<int>();

        return _adjacencyList[u].Remove(v);
    }

    public void Dfs(int u, Dictionary<int, bool> isVisited)
    {
        if (isVisited[u]) return;

        isVisited[u] = true;

        foreach (var v in _adjacencyList[u])
        {
            if (isVisited[v]) continue;

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

    public void Print()
    {
        for (int u = 0; u < _maxN; u++)
        {
            if (_adjacencyList[u] is null || _adjacencyList[u].Count == 0)
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
