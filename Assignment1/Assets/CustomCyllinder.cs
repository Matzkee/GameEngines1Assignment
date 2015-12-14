using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomCyllinder : MonoBehaviour {

    public float radius;
    public float thetaOffset;
    public int numberOfPoints;
    public float height;
    List<Circle> circles;

    public Material material;
    Mesh mesh;
    MeshRenderer meshRenderer;

    // Use this for initialization
    void Start()
    {
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        mesh.Clear();
        circles = new List<Circle>();
        Circle newCircle = new Circle(CreateCircleAt(transform, radius, numberOfPoints));
        circles.Add(newCircle);
        transform.Translate(Vector3.forward * (radius * 2));
        Circle anotherCircle = new Circle(CreateCircleAt(transform, radius, numberOfPoints));
        circles.Add(anotherCircle);
        transform.Translate(Vector3.back * (radius * 2));

        GenerateMesh(circles[0], circles[1]);
        meshRenderer.material = material;
    }

    List<Vector3> CreateCircleAt(Transform _centre, float _radius, int _numpoints)
    {
        List<Vector3> newPoints = new List<Vector3>();
        float theta = Mathf.PI * 2.0f / _numpoints;
        Quaternion prevRot = _centre.rotation;
        for (int i = 0; i < _numpoints; i++)
        {
            _centre.Rotate(Vector3.forward * (theta * Mathf.Rad2Deg));
            Vector3 newPoint = (_centre.position + (_centre.up * _radius));
            newPoints.Add(newPoint);
        }
        _centre.rotation = prevRot;
        return newPoints;
    }

    void GenerateMesh(Circle _c1, Circle _c2)
    {
        int numOfPoints = _c1.circlePoints.Count;
        // 3 Vertices per triangle, 2 triangles
        int verticesPerCell = 6;
        int vertexCount = (verticesPerCell * 2 * numOfPoints);

        // Alocate new arrays
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[vertexCount];
        Vector2[] uvs = new Vector2[vertexCount];

        int vertexIndex = 0;

        for (int i = 0; i < numOfPoints; i++)
        {
            Vector3 cellBottomLeft = _c1.circlePoints[i];
            Vector3 cellTopLeft = _c2.circlePoints[i];
            Vector3 cellTopRight = _c2.circlePoints[(i + 1)%numOfPoints];
            Vector3 cellBottomRight = _c1.circlePoints[(i + 1)%numOfPoints];

            int startVertex = vertexIndex;
            vertices[vertexIndex++] = cellTopLeft;
            vertices[vertexIndex++] = cellBottomLeft;
            vertices[vertexIndex++] = cellBottomRight;
            vertices[vertexIndex++] = cellTopLeft;
            vertices[vertexIndex++] = cellBottomRight;
            vertices[vertexIndex++] = cellTopRight;

            // Make triangles
            for (int j = 0; j < verticesPerCell; j++)
            {
                triangles[startVertex + j] = startVertex + j;
            }
        }

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].z, vertices[i].x);
        }

        // Assign values to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

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
