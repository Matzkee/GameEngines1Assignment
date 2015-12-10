using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CirclePoints : MonoBehaviour {

    public float radius;
    public float thetaOffset;
    public int numberOfPoints;
    public float height;
    List<Vector3> points;

	// Use this for initialization
	void Start () {
        points = new List<Vector3>();

        float thetaInc = Mathf.PI * 2.0f / numberOfPoints;
        for (int i = 0; i < numberOfPoints; i++)
        {
            float theta = thetaOffset + (thetaInc * i);
            Vector3 newPoint = new Vector3();
            newPoint.x = transform.position.x + (Mathf.Sin(theta) * radius);
            newPoint.y = transform.position.y + (Mathf.Cos(theta) * radius);
            points.Add(newPoint);
            Debug.Log("Point Added: "+newPoint);
        }
	}
    public static void DrawLine(Vector3 start, Vector3 end, Color colour)
    {
        Debug.DrawLine(start, end, colour);
    }
    public static void DrawTarget(Vector3 target, Color colour)
    {
        float dist = 1;
        DrawLine(new Vector3(target.x - dist, target.y, target.z), new Vector3(target.x + dist, target.y, target.z), colour);
        DrawLine(new Vector3(target.x, target.y - dist, target.z), new Vector3(target.x, target.y + dist, target.z), colour);
        DrawLine(new Vector3(target.x, target.y, target.z - dist), new Vector3(target.x, target.y, target.z + dist), colour);
    }
    // Update is called once per frame
    void Update () {
        foreach (Vector3 v in points)
        {
            DrawTarget(v, Color.blue);
        }
	}
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void OnDrawGizmos()
    {
        
    }
}
