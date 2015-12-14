using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeForm : MonoBehaviour {
    
    Rule[] ruleset;
    LSystem lsystem;
    Turtle turtle;
    List<Segment> branches;
    List<Circle> circles;

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
    public bool debugLines = false;
    public bool debugCircles = false;

	void Start () {
        // Look up so we rotate the tree structure
        transform.Rotate(Vector3.right * -90);
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
    }
	
	
	void Update () {
        // For now set the generations to be applied each time clicked
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 currentP = transform.position;
            Quaternion currentR = transform.rotation;
            lsystem.Generate();
            turtle.SetAlphabet(lsystem.GetAlphabet());
            turtle.DrawPlant();

            turtle.ChangeLength(lengthRatio);
            turtle.ChangeWidth(0.8f);

            GetTreeBranches();
            transform.position = currentP;
            transform.rotation = currentR;
        }
    }

    void GetTreeBranches()
    {
        branches = turtle.GetBranches();
        circles = turtle.GetCircles();

        Debug.Log("Number of Segments: "+branches.Count+" Number of Circle Points: "+circles.Count * treeRoundness);
    }
    // Draw debug lines
    void OnDrawGizmos()
    {
        
        if (branches != null && debugLines)
        {
            foreach (Segment b in branches)
            {
                Gizmos.color = b.color;
                Gizmos.DrawLine(b.start, b.end);
            }
        }
        
        if (circles != null && debugCircles)
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
