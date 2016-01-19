using UnityEngine;
using System.Collections;

public class Bot : Player {
	// Use this for initialization
	void Start () {
        gun.coolDown = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
        gun.Fire();
	}

}
