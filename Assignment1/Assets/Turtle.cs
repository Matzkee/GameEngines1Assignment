﻿using UnityEngine;
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
    float angleX, angleZ;
    string alphabetToDraw;

    List<Segment> branches;
    Stack<Coord> coordStack;

    GameObject currentTree;
    Transform treeTransform;

    public Turtle(string a, float _length, float _angleX, float _angleZ, GameObject _currentTree)
    {
        currentTree = _currentTree;
        treeTransform = currentTree.transform;
        branches = new List<Segment>();
        coordStack = new Stack<Coord>();

        alphabetToDraw = a;
        length = _length;
        angleX = _angleX;
        angleZ = _angleZ;
    }

    public void DrawPlant()
    {
        Vector3 currentPosition;
        // Make a new branches list
        branches = new List<Segment>();

        // Follow the action depending on current character in alphabet
        for (int i = 0; i < alphabetToDraw.Length; i++)
        {
            char c = alphabetToDraw[i];
            if (c == 'F')
            {
                currentPosition = treeTransform.position;
                treeTransform.Translate(Vector3.forward * length);
                branches.Add(new Segment(currentPosition, treeTransform.position));
            }
            else if (c == '+')
            {
                treeTransform.Rotate(Vector3.right * angleX);
            }
            else if (c == '-')
            {
                treeTransform.Rotate(Vector3.right * -angleX);
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

            //Experimental
            else if (c == 'z')
            {
                treeTransform.Rotate(Vector3.forward * angleZ);
            }
            else if(c == 'a'){
                treeTransform.Rotate(Vector3.forward * -angleZ);
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
    public void SetAngles(float newAngleX, float newAngleZ)
    {
        angleX = newAngleX;
        angleZ = newAngleZ;
    }
    public void SetAlphabet(string newAlphabet)
    {
        alphabetToDraw = newAlphabet;
    }
    public List<Segment> GetBranches()
    {
        return branches;
    }
}
