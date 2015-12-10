using UnityEngine;
using System.Collections.Generic;
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
public class Turtle{

    float length;
    float angle;
    string alphabetToDraw;

    List<Branch> branches;
    Stack<Coord> coordStack;

    GameObject currentTree;
    Transform treeTransform;

    public Turtle(string a, float _length, float _angle, GameObject _currentTree)
    {
        currentTree = _currentTree;
        treeTransform = currentTree.transform;
        branches = new List<Branch>();
        coordStack = new Stack<Coord>();

        alphabetToDraw = a;
        length = _length;
        angle = _angle;
    }

    public void DrawPlant()
    {
        Vector3 currentPosition;
        // Make a new branches list
        branches = new List<Branch>();

        // Follow the action depending on current character in alphabet
        for (int i = 0; i < alphabetToDraw.Length; i++)
        {
            char c = alphabetToDraw[i];
            if (c == 'F')
            {
                currentPosition = treeTransform.position;
                treeTransform.Translate(Vector3.up * length);
                branches.Add(new Branch(currentPosition, treeTransform.position));
            }
            else if (c == '+')
            {
                treeTransform.Rotate(Vector3.forward * angle);
            }
            else if (c == '-')
            {
                treeTransform.Rotate(Vector3.forward * -angle);
            }
            else if (c == '[')
            {
                Coord currentCoord = new Coord(treeTransform.position, treeTransform.rotation);
                coordStack.Push(currentCoord);
            }
            else if (c == ']')
            {
                Coord lastCord = coordStack.Pop();
                treeTransform.position = lastCord.branchPos;
                treeTransform.rotation = lastCord.branchRot;
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
    public List<Branch> GetBranches()
    {
        return branches;
    }
}
