using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
/*  L-Systems contains 3 main components:
        Alphabet - composition of characters which the program works with
        Axiom - an Initial state of the system using characters from alphabet
        Rules - rules of an L-system applied recursively (predecessor & successor)

        Example:
            Alphabet: A B
            Aciom: A
            Rules: (A -> AB), (B -> A)
    */
public class LSystem : MonoBehaviour {

    List<Branch> branches = new List<Branch>();

    public float branchLength = 7;
    public float branchAngle = Mathf.PI / 6;
    private int generation = 0;

    string current = "A";
    // Use this for initialization
    void Start () {
        Debug.Log("Generation " + generation + ": " + current);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
            NextGeneration();
    }

    void NextGeneration()
    {
    // Since the string can get very large it is recommended to use
    // StringBuilder instead of string as it deals with concatenating in
    // much nicer way than string
    StringBuilder next = new StringBuilder();
        for (int i = 0; i < current.Length; i++)
        {
            char c = current[i];
            if (c == 'A')
            {
                next.Append("AB");
            }
            else if (c == 'B')
            {
                next.Append("A");
            }
        }
        current = next.ToString();
        generation += 1;
        Debug.Log("Generation " + generation + ": " + current);
    }

    class Branch
    {
        Vector3 start;
        Vector3 end;

        public Branch(Vector3 _start, Vector3 _end)
        {
            start = _start;
            end = _end;
        }

        public Vector3 GetStart()
        {
            return start;
        }
        public Vector3 GetEnd()
        {
            return end;
        }
    }
}
