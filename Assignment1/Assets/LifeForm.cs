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
    public float angleY = 30.0f;
    public float lengthRatio = 0.7f;
    public float startRadius;
    public int treeRoundness = 8;
    public string axiom;
    public char[] ruleChars;
    public string[] ruleStrings;

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

            GetTreeBranches();
            transform.position = currentP;
            transform.rotation = currentR;
        }
    }

    void GetTreeBranches()
    {
        branches = turtle.GetBranches();
    }
    // Draw life form segments
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
