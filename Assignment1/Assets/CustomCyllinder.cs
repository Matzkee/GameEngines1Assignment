using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CirclePoints : MonoBehaviour {

    public float radius;
    public float thetaOffset;
    public int numberOfPoints;
    public float height;
    List<Circle> circles;

    // Use this for initialization
    void Start()
    {
        circles = new List<Circle>();
    }

    Circle CreateCircleAt(Transform _centre, float _radius, int _numpoints)
    {
        List<Vector3> newPoints = new List<Vector3>();
        float theta = Mathf.PI * 2.0f / _numpoints;
        for (int i = 0; i < _numpoints; i++)
        {
            _centre.Rotate(Vector3.forward * theta);
            Vector3 newPoint = (_centre.up * _radius);
            newPoints.Add(newPoint);
        }

        return new Circle(newPoints);
    }

    void OnDrawGizmos()
    {
        if (circles != null)
        {
            foreach (Circle c in circles)
            {
                for (int j = 0; j < c.circlePoints.Count; j++)
                {

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
