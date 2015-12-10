using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LSysTree : MonoBehaviour {

    Rule[] ruleset;
    LSystem lsystem;
    Turtle turtle;
    List<Branch> branches;

    public float length = 5.0f;
    public float angle = 22.5f;
    public float lengthRatio = 0.7f;

	void Start () {
        ruleset = new Rule[1];
        ruleset[0] = new Rule('F',"FF+[+F-F-F]-[-F+F+F]");

        lsystem = new LSystem("F",ruleset);

        turtle = new Turtle(lsystem.GetAlphabet(),length,angle, gameObject);
    }
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 current = transform.position;
            lsystem.Generate();
            turtle.SetAlphabet(lsystem.GetAlphabet());
            turtle.DrawPlant();

            turtle.ChangeLength(lengthRatio);

            GetTreeBranches();
            transform.position = current;
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
            foreach (Branch b in branches)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(b.GetStart(), b.GetEnd());
            }
        }
    }
}
