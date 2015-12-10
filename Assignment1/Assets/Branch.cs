using UnityEngine;

public class Branch
{
    Vector3 start;
    Vector3 end;

    public Branch(Vector3 _start, Vector3 _end)
    {
        start = _start;
        end = _end;
    }

    public Vector3 GetStart()
    {
        return start;
    }
    public Vector3 GetEnd()
    {
        return end;
    }
    
}
