using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mass : MonoBehaviour {

    public float m;
    public Vector3 vel, force, gravitationalForce;

	// Use this for initialization
	void Start () {
        force = Vector3.zero;
        vel = Vector3.zero;
        gravitationalForce = new Vector3(0.0f, -9.81f, 0.0f);
	}

    public void ApplyForce(Vector3 f)
    {
        force += f;
    }


    // Update is called once per frame
    // This is basically Euler Method (Semi-Implicit)
    public void Simulate(float dt)
    {
        vel += (force / m) * dt;
        transform.position += vel * dt;

    }

    //public void Simulate(float dt)
    //{
    //    Vector3 k1, k2, k3, k4; // for acc.
    //    Vector3 l1, l2, l3, l4; // for vel

    //    k1 = (force/m);
    //    l1 = vel;

    //    k2 = (force / m);
    //    l2 = vel + 0.5f * dt * k1;

    //    k3 = (force/m);
    //    l3 = vel + 0.5f * dt * k2;

    //    k4 = (force / m);
    //    l4 = vel + dt * k3;

    //    // Propagate solution forward to next time step 
    //    vel += (dt / 6.0f) * (k1 + 2 * k2 + 2 * k3 + k4);
    //    transform.position += (dt / 6.0f) * (l1 + 2 * l2 + 2 * l3 + l4);
    //}

}
