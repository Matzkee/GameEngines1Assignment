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

        Now Lets create our alphabet
        F: translate + add branch to the list
        G: 
        +: rotate(angle)
        -: rotate(-angle)
        [: save position & rotation
        ]: restore position & rotation
    */
public class LSystem : MonoBehaviour {

    List<Branch> branches = new List<Branch>();

    public float branchLength = 3.0f;
    public float branchAngle = 25.0f;

    private int generation = 0;

    string alphabet = "F";
    string ruleset = "FF+[+F-F-F]-[-F+F+F]";


    void Start () {
        Debug.Log("Generation " + generation + ": " + alphabet);
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            NextGeneration();
            DrawTree(branchLength);
            branchLength *= 0.5f;
        }
    }

    void NextGeneration()
    {
        StringBuilder next = new StringBuilder();
        for (int j = 0; j < alphabet.Length; j++)
        {
            char k = alphabet[j];
            if (k == 'F')
            {
                next.Append(ruleset);
            }
            else
            {
                next.Append(k);
            }
        }
        alphabet = next.ToString();
        generation += 1;
        Debug.Log("Generation " + generation + ": " + alphabet);
    }

    void DrawTree(float len)
    {
        Vector3 lastPosition = transform.position;     // Store the last position vector
        Vector3 currentPosition;
        Quaternion lastRotation = transform.rotation;    // Store the rotation


        for (int i = 0; i < alphabet.Length; i++)
        {
            char c = alphabet[i];
            if (c == 'F')
            {
                currentPosition = transform.position;
                transform.Translate(Vector3.up * len);
                branches.Add(new Branch(currentPosition, transform.position));
            }
            else if (c == 'F')
            {
                transform.Translate(Vector3.up * len);
            }
            else if (c == '+')
            {
                transform.Rotate(Vector3.forward * branchAngle);
            }
            else if (c == '-')
            {
                transform.Rotate(Vector3.forward * -branchAngle);
            }
            else if (c == '[')
            {
                lastPosition = transform.position;
                lastRotation = transform.rotation;
            }
            else if (c == ']')
            {
                transform.position = lastPosition;
                transform.rotation = lastRotation;
            }
        }
    }

    void OnDrawGizmos()
    {
        foreach (Branch b in branches)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(b.GetStart(), b.GetEnd());
        }
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
