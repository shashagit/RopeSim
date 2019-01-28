using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSimulator : MonoBehaviour {

    public Transform rope;

    public int numOfMasses;

    public float gravity;

    public float maxPossible_dt;

    Rope myRope;

	// Use this for initialization
	void Start () {
        // create rope
        Transform t = Instantiate<Transform>(rope);
        myRope = t.GetComponent<Rope>();
        myRope.numOfMasses = numOfMasses;

        
    }
	
    

	// Update is called once per frame
	void Update () {

        float dt = Time.deltaTime;                                      

        int numOfIterations = (int)(dt / maxPossible_dt) + 1; // Calculate Number Of Iterations To Be Made At This Update Depending On maxPossible_dt And dt

        //Debug.Log(numOfIterations);
        if (numOfIterations != 0) // Avoid Division By Zero
            dt = dt / numOfIterations;

        for (int i = 0; i < numOfIterations; ++i)
        {
            myRope.Init(); // setting all forces to zero
            myRope.Solve();// calculating all forces
            myRope.Simulate(dt); // using Integrator to propagate the solution
        }

        // Input
        float velocityx = 0.0f;
        float velocityy = 0.0f;
        float velocityz = 0.0f;
        float velCenx = 0.0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocityx -= 3f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            velocityx += 3f;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            velocityy += 3f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            velocityy -= 3f;
        }
        else if (Input.GetKey(KeyCode.PageUp))
        {
            velocityz += 3f;
        }
        else if (Input.GetKey(KeyCode.PageDown))
        {
            velocityz -= 3f;
        }
        else if (Input.GetKey(KeyCode.Comma))
        {
            velCenx -= 3f;
        }
        else if (Input.GetKey(KeyCode.Period))
        {
            velCenx += 3f;
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            myRope.rotateEnd = true;
        }
        myRope.topVelocity.x = velocityx;
        myRope.centerVelocity.z = velocityz;
        myRope.centerVelocity.x = velCenx;
        myRope.bottomVelocity.y = velocityy;
	}
}
