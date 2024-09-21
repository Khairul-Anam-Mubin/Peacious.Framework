namespace Peacious.Framework.DSA;

public class Compresser
{
    private readonly Dictionary<string, int> _mappedValues;
    private int _incrementalId = 0;

    public Compresser()
    {
        _mappedValues = new Dictionary<string, int>();
    }

    public int GetCompressedKey(string key)
    {
        if (_mappedValues.ContainsKey(key)) 
        { 
            return _mappedValues[key];
        }
        
        _incrementalId++;

        _mappedValues.Add(key, _incrementalId);

        return _incrementalId;
    }
}
