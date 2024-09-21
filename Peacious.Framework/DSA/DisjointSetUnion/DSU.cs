namespace Peacious.Framework.DSA.DisjointSetUnion;

public class DSU
{
    private int[] _parent = [];
    private int[] _setSize = [];
    private int _maxN = 0;

    public void Initialize(int n)
    {
        if (_maxN < n)
        {
            Array.Copy(new int[n * 2 + 1], _parent, n * 2 + 1);
            Array.Copy(new int[n * 2 + 1], _setSize, n * 2 + 1);
            //_parent = new int[n * 2 + 1];
            //_setSize = new int[n * 2 + 1];
        }

        for (var i = 1; i <= n; i++)
        {
            _parent[i] = n + i;
            _parent[n + i] = n + i;
            _setSize[n + i] = 1;
        }

        _maxN = n;
    }

    public void Union(int u, int v)
    {
        u = FindParent(u);
        v = FindParent(v);

        if (u == v) return;

        if (_setSize[u] < _setSize[v]) (u, v) = (v, u);

        _parent[v] = u;
        _setSize[u] += _setSize[v];
    }

    public int FindParent(int u)
    {
        if (_parent[u] == u) return u;
        return _parent[u] = FindParent(_parent[u]);
    }

    public bool IsInSameSet(int u, int v)
    {
        return FindParent(u) == FindParent(v);
    }

    public int GetSetSize(int u)
    {
        return _setSize[FindParent(u)];
    }

    public void MoveUToSetV(int u, int v)
    {
        if (IsInSameSet(u, v)) return;

        var x = FindParent(u);
        var y = FindParent(v);

        _setSize[x]--;
        _setSize[y]++;

        _parent[u] = y;
    }
}
