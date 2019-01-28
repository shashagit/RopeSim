using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    public int numOfMasses;

    float springLength;

    public Transform massPrefab;
    public Transform springPrefab;

    public Transform[] tOfMasses;

    public bool rotateEnd = false;

    public Vector3 topPosition;
    public Vector3 topVelocity;
    public Vector3 centerPosition;
    public Vector3 centerVelocity;
    public Vector3 bottomPosition;
    public Vector3 bottomVelocity;

    public Spring[] springs;
    public Mass[] masses;

	// Use this for initialization
	void Start () {

        tOfMasses = new Transform[numOfMasses];
        masses = new Mass[numOfMasses];

        topPosition = Vector3.zero;
        topVelocity = Vector3.zero;

        centerPosition = Vector3.zero;
        centerVelocity = Vector3.zero;

        bottomPosition = Vector3.zero;
        bottomVelocity = Vector3.zero;

        springs = new Spring[numOfMasses-1];

        springLength = springPrefab.GetComponent<Spring>().springLength;

        // create all masses
        for (int i = 0; i < numOfMasses; ++i)
        {
            Transform t = Instantiate<Transform>(massPrefab);
            tOfMasses[i] = t;
            masses[i] = t.GetComponent<Mass>();
            t.parent = transform;
            t.position = new Vector3(i * springLength, 0.0f, 0.0f);

            //if(i <= numOfMasses/2)
            //t.position = new Vector3(i*springLength * Mathf.Cos(Mathf.PI/4), -Mathf.Cos(Mathf.PI / 4)* i * springLength, 0.0f); 
            //else
            //t.position = tOfMasses[i-1].position + new Vector3(springLength * Mathf.Cos(Mathf.PI / 4), Mathf.Cos(Mathf.PI / 4) * springLength, 0.0f);
        }

        centerPosition = tOfMasses[numOfMasses/3].position;
        bottomPosition = tOfMasses[numOfMasses - numOfMasses/3].position;

        // create springs and connect masses to them
        for (int i = 0; i < numOfMasses-1; ++i)
        {
            Transform t = Instantiate<Transform>(springPrefab);
            t.parent = tOfMasses[i+1];
            t.position = new Vector3(0.0f, t.parent.position.x - springLength/2, 0.0f);
            //t.localScale = new Vector3(1.0f, 1.0f, springLength);

            Spring s = t.GetComponent<Spring>();
            springs[i] = s;
            s.mass1 = masses[i].GetComponent<Mass>();
            s.mass2 = masses[i + 1].GetComponent<Mass>();
        }
    }

    public void Init()
    {
        for (int i = 0; i < numOfMasses; ++i)
        {
            masses[i].force = Vector3.zero;
        }
    }

    public void Solve()
    {
        // apply all spring forces to the system
        for (int i = 0; i < numOfMasses - 1; ++i)
        {
            springs[i].Solve();
        }

        // apply gravitational force to the system
        for (int i = 0; i < numOfMasses; ++i)
        {
            masses[i].ApplyForce(masses[i].gravitationalForce * masses[i].m);
        }
    }

    float timeCounter = 0.0f;

    public void Simulate(float dt)
    {

        for(int i = 0; i < numOfMasses; ++i)
        {
            masses[i].Simulate(dt);
        }

        // fix initial position and final position
        topPosition += topVelocity * dt;
        masses[0].vel = topVelocity;
        tOfMasses[0].position = topPosition;

        bottomPosition += bottomVelocity * dt;
        masses[numOfMasses - numOfMasses / 3].vel = bottomVelocity;
        tOfMasses[numOfMasses - numOfMasses / 3].position = bottomPosition;

        //start rotating center
        if (!rotateEnd)
        {
            centerPosition += centerVelocity * dt;
            masses[numOfMasses/3].vel = centerVelocity;
            tOfMasses[numOfMasses/3].position = centerPosition;
        }
        else
        {
            timeCounter += 15 * dt;
            float y = 15 * Mathf.Cos(timeCounter);
            float z = 15 * Mathf.Sin(timeCounter);
            float x = 0;
            centerVelocity = new Vector3(x, y, z);

            centerPosition += centerVelocity * dt;
            masses[numOfMasses / 3].vel = centerVelocity;
            tOfMasses[numOfMasses / 3].position = centerPosition;
        }
    }

}
