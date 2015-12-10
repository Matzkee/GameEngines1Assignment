using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turtle : MonoBehaviour {

    float length;
    float angle;
    string alphabetToDraw;
    Stack<Coord> coordStack = new Stack<Coord>();

    public Turtle(string a, float _length, float _angle)
    {
        alphabetToDraw = a;
        length = _length;
        angle = _angle;
    }

    public void drawPlant()
    {
        for (int i = 0; i < alphabet.Length; i++)
        {
            char c = alphabet[i];
            if (c == 'F')
            {
                currentPosition = transform.position;
                transform.Translate(Vector3.up * len);
                branches.Add(new Branch(currentPosition, transform.position));
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

    class Coord
    {
        public Vector3 branchPos;
        public Quaternion branchRot;

        public Coord(Vector3 _branchPos, Quaternion _branchRot)
        {
            branchPos = _branchPos;
            branchRot = _branchRot;
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
