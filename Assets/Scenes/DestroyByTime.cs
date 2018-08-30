using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float lifetime = 3.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Lean.Pool.LeanPool.Despawn(this, lifetime);
	}
}
