using UnityEngine;

public class Segment
{
    Vector3 start;
    Vector3 end;
    Color color;

    public Segment(Vector3 _start, Vector3 _end)
    {
        start = _start;
        end = _end;
        color = Color.grey;
    }
    public void SetColor(Color c)
    {
        color = c;
    }
    public Color GetColor()
    {
        return color;
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
