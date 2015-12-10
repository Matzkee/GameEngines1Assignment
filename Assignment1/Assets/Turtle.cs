using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turtle : MonoBehaviour {

    float length;
    float angle;
    string alphabetToDraw;

    List<Branch> branches;
    Stack<Coord> coordStack;

    public Turtle(string a, float _length, float _angle)
    {
        branches = new List<Branch>();
        coordStack = new Stack<Coord>();

        alphabetToDraw = a;
        length = _length;
        angle = _angle;
    }

    public void drawPlant()
    {
        // Make a new branches list
        branches = new List<Branch>();
        Vector3 currentPosition;

        // Follow the action depending on current character in alphabet
        for (int i = 0; i < alphabetToDraw.Length; i++)
        {
            char c = alphabetToDraw[i];
            if (c == 'F')
            {
                currentPosition = transform.position;
                transform.Translate(Vector3.up * length);
                branches.Add(new Branch(currentPosition, transform.position));
            }
            else if (c == '+')
            {
                transform.Rotate(Vector3.forward * angle);
            }
            else if (c == '-')
            {
                transform.Rotate(Vector3.forward * -angle);
            }
            else if (c == '[')
            {
                Coord currentCoord = new Coord(transform.position, transform.rotation);
                coordStack.Push(currentCoord);
            }
            else if (c == ']')
            {
                Coord lastCord = coordStack.Pop();
                transform.position = lastCord.branchPos;
                transform.rotation = lastCord.branchRot;
            }
        }
    }


    public class Coord
    {
        public Vector3 branchPos;
        public Quaternion branchRot;

        public Coord(Vector3 _branchPos, Quaternion _branchRot)
        {
            branchPos = _branchPos;
            branchRot = _branchRot;
        }
    }

    public class Branch
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
    public void SetLength(float newLength)
    {
        length = newLength;
    }
    public void ChangeLength(float changeRatio)
    {
        length *= changeRatio;
    }
    public void SetAngle(float newAngle)
    {
        angle = newAngle;
    }
    public void SetAlphabet(string newAlphabet)
    {
        alphabetToDraw = newAlphabet;
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
