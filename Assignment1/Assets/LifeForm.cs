using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeForm : MonoBehaviour {

    Rule[] ruleset;
    LSystem lsystem;
    Turtle turtle;
    List<Branch> branches;

    public float length = 5.0f;
    public float angle = 22.5f;
    public float lengthRatio = 0.7f;
    public string axiom;
    public char[] ruleChars;
    public string[] ruleStrings;

	void Start () {
        if (ruleChars != null)
        {
            ruleset = new Rule[ruleChars.Length];
            for(int i = 0; i < ruleChars.Length; i++)
            {
                ruleset[i] = new Rule(ruleChars[i], ruleStrings[i]);
            }
        }
        lsystem = new LSystem(axiom,ruleset);

        turtle = new Turtle(lsystem.GetAlphabet(),length,angle, gameObject);
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
            foreach (Branch b in branches)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(b.GetStart(), b.GetEnd());
            }
        }
    }
}
