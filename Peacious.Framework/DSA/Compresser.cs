namespace Peacious.Framework.DSA;

public class Compressor<T> where T : class
{
    private readonly Dictionary<T, int> _mappedValues;
    private readonly Dictionary<int, T> _mappedKeys;

    private int _incrementalId = 0;

    public Compressor()
    {
        _mappedValues = new();
        _mappedKeys = new();
    }

    public int GetCompressedValue(T value)
    {
        if (_mappedValues.ContainsKey(value)) 
        { 
            return _mappedValues[value];
        }
        
        _incrementalId++;

        _mappedValues.Add(value, _incrementalId);

        return _incrementalId;
    }

    public T GetDecompressedValue(int compressedValue)
    {
        if (_mappedKeys.ContainsKey(compressedValue))
        {
            return _mappedKeys[compressedValue];
        }

        throw new Exception("Invalid Compressed Key");
    }
}
