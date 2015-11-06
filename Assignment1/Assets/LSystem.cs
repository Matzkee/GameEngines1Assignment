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

    string alphabet = "A";

    void Start () {
        Debug.Log("Generation " + generation + ": " + alphabet);
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
            NextGeneration(branchLength);
    }

    void NextGeneration(float len)
    {
        Vector3 lastPosition; 	    // Store the starting vector location
		Vector3 currentPosition; 	// Store the next vector location
        Quaternion lastRotation;    // Store the rotation
    // Now Lets create our alphabet
    // F: translate + add branch to the list
    // G: 
    // +: rotate(angle)
    // -: rotate(-angle)
    // [: save position & rotation
    // ]: restore position & rotation
        StringBuilder next = new StringBuilder();
        for (int i = 0; i < alphabet.Length; i++)
        {
            char c = alphabet[i];

            if (c == 'F')
            {
                lastPosition = transform.position;
                transform.Translate(Vector3.up * len);
                currentPosition = transform.position;
                branches.Add(new Branch(lastPosition, currentPosition));
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
        alphabet = next.ToString();
        generation += 1;
        Debug.Log("Generation " + generation + ": " + alphabet);
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

    void OnDrawGizmos()
    {
        foreach (Branch b in branches)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(b.GetStart(), b.GetEnd());
        }
    }
}
