using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeMaker : MonoBehaviour {

	List<Segment> branches = new List<Segment> ();

	public float branchLength = 7;
	public float branchAngle = 20;

	Vector3 currentPos;
	// Use this for initialization
	void Start () {
		currentPos = transform.position;
        transform.Translate(Vector3.up * branchLength);
        Segment trunk = new Segment (currentPos, transform.position);
		branches.Add (trunk);
        BranchOut (branchLength);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void BranchOut(float len){
		Vector3 lastPosition; 	    // Store the starting vector location
        Quaternion lastRotation;    // Store the rotation

        //Debug.Log("New branch: "+previous+ " - "+next);
        lastPosition = transform.position;
        transform.Translate(Vector3.up * len);
        branches.Add(new Segment(lastPosition, transform.position));

        len *= 0.8f;

		if(len > 2){
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            transform.Rotate (Vector3.forward * branchAngle);
			BranchOut (len);
            transform.position = lastPosition;
            transform.rotation = lastRotation;

            lastPosition = transform.position;
            lastRotation = transform.rotation;
            transform.Rotate(Vector3.forward * -branchAngle);
            BranchOut(len);
            transform.position = lastPosition;
            transform.rotation = lastRotation;
        }
		//transform.position = lastPosition;
	}

	void OnDrawGizmos(){
		foreach(Segment b in branches){
			Gizmos.color = Color.green;
			Gizmos.DrawLine(b.GetStart(), b.GetEnd());
		}
	}
}
