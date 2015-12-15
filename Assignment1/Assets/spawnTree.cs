using UnityEngine;
using System.Collections.Generic;

public class spawnTree : MonoBehaviour {

    public GameObject[] trees;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= 0.5)
        {
            Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
            Instantiate(trees[Random.Range(0, trees.Length - 1)], pos, new Quaternion());
            Destroy(this.gameObject);
        }
	}
}
