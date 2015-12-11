using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomCyllinder : MonoBehaviour {

    public float radius;
    public float thetaOffset;
    public int numberOfPoints;
    public float height;
    List<Circle> circles;

    // Use this for initialization
    void Start()
    {
        circles = new List<Circle>();
        Circle newCircle = new Circle(CreateCircleAt(transform, radius, numberOfPoints));
        circles.Add(newCircle);
        transform.Translate(Vector3.forward * (radius * 2));
        Circle anotherCircle = new Circle(CreateCircleAt(transform, radius, numberOfPoints));
        circles.Add(anotherCircle);
    }

    List<Vector3> CreateCircleAt(Transform _centre, float _radius, int _numpoints)
    {
        Debug.Log("Current Position: "+_centre.position);
        Vector3 above = (_centre.up * _radius);
        Debug.Log("Point Above Me: "+ above);
        List<Vector3> newPoints = new List<Vector3>();
        float theta = Mathf.PI * 2.0f / _numpoints;
        Quaternion temp = _centre.rotation;
        for (int i = 0; i < _numpoints; i++)
        {
            _centre.Rotate(Vector3.forward * (theta * Mathf.Rad2Deg));
            Vector3 newPoint = (_centre.up * _radius);
            newPoints.Add(newPoint);
        }
        _centre.rotation = temp;
        return newPoints;
    }

    void OnDrawGizmos()
    {
        if (circles != null)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                for (int j = 1; j <= circles[i].circlePoints.Count; j++)
                {
                    Vector3 prev = circles[i].circlePoints[j - 1];
                    Vector3 next = circles[i].circlePoints[j % circles[i].circlePoints.Count];
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(prev, next);
                }
            }
        }
    }

    class Circle
    {
        public List<Vector3> circlePoints;

        public Circle(List<Vector3> _points)
        {
            circlePoints = _points;
        }
    }
}
