using UnityEngine;
using System.Collections.Generic;

public class spawnTree : MonoBehaviour {

    public GameObject[] trees;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= 0.55f)
        {
            Vector3 pos = transform.position;
            pos.y = 0;
            Instantiate(trees[Random.Range(0, trees.Length - 1)], pos, new Quaternion());
            //newTree.transform.position = transform.position;
            Destroy(this.gameObject);
        }
	}
}
