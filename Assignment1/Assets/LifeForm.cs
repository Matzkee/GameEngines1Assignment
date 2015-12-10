using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeForm : MonoBehaviour {

    Rule[] ruleset;
    LSystem lsystem;
    Turtle turtle;
    List<Segment> branches;

    public float length = 5.0f;
    public float angleX = 22.5f;
    public float angleZ = 30.0f;
    public float lengthRatio = 0.7f;
    public string axiom;
    public char[] ruleChars;
    public string[] ruleStrings;

	void Start () {
        // Look up so we rotate the tree structure
        transform.LookAt(transform.up);
        if (ruleChars != null)
        {
            ruleset = new Rule[ruleChars.Length];
            for(int i = 0; i < ruleChars.Length; i++)
            {
                ruleset[i] = new Rule(ruleChars[i], ruleStrings[i]);
            }
        }
        lsystem = new LSystem(axiom,ruleset);

        turtle = new Turtle(lsystem.GetAlphabet(), length, angleX, angleZ, gameObject);
    }
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 currentP = transform.position;
            Quaternion currentR = transform.rotation;
            lsystem.Generate();
            turtle.SetAlphabet(lsystem.GetAlphabet());
            turtle.DrawPlant();

            turtle.ChangeLength(lengthRatio);

            GetTreeBranches();
            transform.position = currentP;
            transform.rotation = currentR;
        }
    }

    void GetTreeBranches()
    {
        branches = turtle.GetBranches();
    }
    void OnDrawGizmos()
    {
        if (branches != null)
        {
            foreach (Segment b in branches)
            {
                Gizmos.color = b.GetColor();
                Gizmos.DrawLine(b.GetStart(), b.GetEnd());
            }
        }
    }
}
