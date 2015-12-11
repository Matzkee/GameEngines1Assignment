using UnityEngine;

/*
    This class is used to store information between 2 points in
    an L-System. It is mainly used to create branches
    */
public class Segment
{
    Vector3 start;
    Vector3 end;
    Color color;

    // Constructor
    public Segment(Vector3 _start, Vector3 _end)
    {
        start = _start;
        end = _end;
        color = Color.grey;
    }

    // Getters & Setters
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
