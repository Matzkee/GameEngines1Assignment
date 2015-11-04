using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeMaker : MonoBehaviour {

	List<Branch> branches = new List<Branch> ();

	public float branchLength = 7;
	public float branchAngle = Mathf.PI / 6;

	Branch nextBranch;

	Vector3 currentPos;
	// Use this for initialization
	void Start () {
		currentPos = transform.position;
		Branch trunk = new Branch (currentPos, Vector3.up * branchLength);
		branches.Add (trunk);
		BranchOut (branchLength);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void BranchOut(float len){
		Vector3 previous; 	// Store the starting vector location
		Vector3 next; 		// Store the next vector location

		previous = transform.position;
		transform.Translate (Vector3.up * len);

		len *= 0.7f;

		if(len >= 3){
			// Rotate vector to the left
			transform.Rotate (Vector3.forward * branchAngle);
			next = transform.position;
			nextBranch = new Branch (previous, next);
			branches.Add (nextBranch);
			
			BranchOut (len);
			
			//transform.Translate (Vector3.up * len);
			
			transform.position = previous;
			
			// Rotate vector to the right
			transform.Rotate (Vector3.forward * -branchAngle * 2);
			next = transform.position;
			nextBranch = new Branch (previous, next);
			branches.Add (nextBranch);
			
			BranchOut (len);
			
			//transform.Translate (Vector3.up * len);
			transform.position = previous;
		}
	}

	void OnDrawGizmos(){
		foreach(Branch b in branches){
			Gizmos.color = Color.green;
			Gizmos.DrawLine(b.GetStart(), b.GetEnd());
		}
	}

	class Branch{
		Vector3 start;
		Vector3 end;

		public Branch(Vector3 _start, Vector3 _end){
			start = _start;
			end = _end;
		}

		public Vector3 GetStart(){
			return start;
		}
		public Vector3 GetEnd(){
			return end;
		}
	}
}
