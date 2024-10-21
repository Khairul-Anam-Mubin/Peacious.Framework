namespace Peacious.Framework.DSA.DisjointSetUnion;

public class DSU
{
    private int[] _parent = [];
    private int[] _setSize = [];
    private int _maxN = 0;

    // Initialize DSU for `n` elements
    public void Initialize(int n)
    {
        // If new n exceeds current capacity, resize arrays
        if (_maxN < n)
        {
            Array.Resize(ref _parent, n * 2 + 1);
            Array.Resize(ref _setSize, n * 2 + 1);
        }

        // Initialize sets and sizes for the elements
        for (var i = 1; i <= n; i++)
        {
            _parent[i] = n + i;
            _parent[n + i] = n + i;
            _setSize[n + i] = 1;
        }

        _maxN = n;  // Update maxN
    }

    // Union operation: merges sets containing `u` and `v`
    public void Union(int u, int v)
    {
        u = FindParent(u);
        v = FindParent(v);

        if (u == v) return;  // They are already in the same set

        // Union by size: smaller set joins larger set
        if (_setSize[u] < _setSize[v]) (u, v) = (v, u);

        _parent[v] = u;
        _setSize[u] += _setSize[v];
    }

    // Find operation: finds the parent (root) of `u`
    public int FindParent(int u)
    {
        if (_parent[u] == u) return u;  // u is the root of its set
        return _parent[u] = FindParent(_parent[u]);  // Path compression
    }

    // Check if `u` and `v` belong to the same set
    public bool IsInSameSet(int u, int v)
    {
        return FindParent(u) == FindParent(v);
    }

    // Get the size of the set containing `u`
    public int GetSetSize(int u)
    {
        return _setSize[FindParent(u)];
    }

    // Move `u` from its current set to the set containing `v`
    public void MoveUToSetV(int u, int v)
    {
        if (IsInSameSet(u, v)) return;  // Already in the same set

        var parentU = FindParent(u);
        var parentV = FindParent(v);

        _setSize[parentU]--;  // Decrease size of `u`'s original set
        _setSize[parentV]++;  // Increase size of `v`'s set

        _parent[u] = parentV;  // Move `u` under the root of `v`'s set
    }
}
