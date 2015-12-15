using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeForm : MonoBehaviour {
    
    Rule[] ruleset;
    LSystem lsystem;
    Turtle turtle;
    List<Segment> branches;
    List<Circle> circles;

    List<MeshFilter> filters;
    List<GameObject> treeBranches;
    public Material treeBark;

    public float length = 5.0f;
    public float angleX = 22.5f;
    public float angleY = 30.0f;
    public float lengthRatio = 0.7f;
    public float startRadius = 1.0f;
    public int treeRoundness = 8;
    public float widthRatio = 0.7f;
    public string axiom;
    public char[] ruleChars;
    public string[] ruleStrings;
    public bool skeletonLines = false;
    public bool skeletonCircles = false;

    public int generations = 0;
    private bool rotated = false;

	void Start () {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        treeBranches = new List<GameObject>();

        // Look up so we rotate the tree structure
        transform.Rotate(Vector3.right * -90.0f);
        // Rules can be applied in an inspector, once game is started all information is
        // taken from an editor
        if (ruleChars != null)
        {
            ruleset = new Rule[ruleChars.Length];
            for(int i = 0; i < ruleChars.Length; i++)
            {
                ruleset[i] = new Rule(ruleChars[i], ruleStrings[i]);
            }
        }
        lsystem = new LSystem(axiom,ruleset);

        turtle = new Turtle(startRadius, treeRoundness, lsystem.GetAlphabet(), length, angleX, angleY, gameObject);

        // Draw the tree
        for (int i = 0; i < generations; i++)
        {
            if (rotated)
            {
                transform.Rotate(Vector3.right * -90.0f);
                rotated = false;
            }
            Vector3 currentP = transform.position;
            Quaternion currentR = transform.rotation;

            // Generate the alphabet & pass it to the turtle
            lsystem.Generate();
            turtle.SetAlphabet(lsystem.GetAlphabet());
            turtle.DrawPlant();
            // Adjust turtle ratios
            turtle.ChangeLength(lengthRatio);
            turtle.ChangeWidth(widthRatio);
            // Get vector arrays
            GetTreeBranches();
            transform.position = currentP;
            transform.rotation = currentR;

            DestroyTree();
            RenderTree();
            CombineMeshes();

            // Due to combining meshes the transform appears to be rotated
            // so rotate it back upwards
            transform.Rotate(Vector3.right * 90.0f);
            rotated = true;
        }
        // Future Work: The tree combining of branch meshes is messing up with the positioning
        // of the rendered component, for now this piece of code is subtracting current position
        // from itself to position the render with debug lines
        transform.position -= transform.position;
    }

    // Destroy all created branch objects
    void DestroyTree()
    {
        foreach (GameObject g in treeBranches)
        {
            Destroy(g);
        }
    }
    // Make new objects with rendered meshes
    void RenderTree()
    {
        filters = new List<MeshFilter>();
        foreach (Segment e in branches)
        {
            MakeBranch(e);
        }
    }
    // Get vector lists
    void GetTreeBranches()
    {
        branches = turtle.GetBranches();
        circles = turtle.GetCircles();
    }

    void CombineMeshes()
    {
        // Method taken from Unity's combine meshes reference page
        // http://docs.unity3d.com/ScriptReference/Mesh.CombineMeshes.html
        // This method combines the meshes previously accumulated in a list
        CombineInstance[] combine = new CombineInstance[filters.Count];
        int i = 0;
        while (i < filters.Count)
        {
            combine[i].mesh = filters[i].sharedMesh;
            combine[i].transform = filters[i].transform.localToWorldMatrix;
            filters[i].gameObject.SetActive(false);
            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.GetComponent<MeshRenderer>().material = treeBark;
        transform.gameObject.SetActive(true);

        // Destroy the leftover objects
        DestroyTree();
    }

    // Make new object for each branch with mesh and material applied
    void MakeBranch(Segment s)
    {
        GameObject branch = new GameObject();
        Mesh mesh;
        MeshFilter filter;
        MeshRenderer meshRenderer;

        filter = branch.AddComponent<MeshFilter>();
        mesh = filter.mesh;
        meshRenderer = branch.AddComponent<MeshRenderer>();
        mesh.Clear();

        int numOfPoints = s.startCircle.circlePoints.Count;
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
            Vector3 cellBottomLeft = s.startCircle.circlePoints[i];
            Vector3 cellTopLeft = s.endCircle.circlePoints[i];
            Vector3 cellTopRight = s.endCircle.circlePoints[(i + 1) % numOfPoints];
            Vector3 cellBottomRight = s.startCircle.circlePoints[(i + 1) % numOfPoints];

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
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        // Assign values to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        meshRenderer.material = treeBark;

        branch.transform.parent = transform;
        treeBranches.Add(branch);
        filters.Add(filter);
    }

    // Draw debug lines
    void OnDrawGizmos()
    {
        
        if (branches != null && skeletonLines)
        {
            foreach (Segment b in branches)
            {
                Gizmos.color = b.color;
                Gizmos.DrawLine(b.start, b.end);
            }
        }
        
        if (circles != null && skeletonCircles)
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
}
