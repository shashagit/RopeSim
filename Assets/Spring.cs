using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    public Mass mass1, mass2;

    public float springConstant;
    public float frictionConstant;
    public float springLength;

	public void Solve()
    {
        Vector3 springVec = mass1.transform.position - mass2.transform.position;

        float l = springVec.magnitude;

        Vector3 force = Vector3.zero;

        // calculating spring force
        if(l != 0)
        {
            force += (springVec / l) * (l - springLength) * (-springConstant);
        }

        // calculating spring friction
        force += -(mass1.vel - mass2.vel) * frictionConstant;

        // apply force to both masses
        mass1.ApplyForce(force);
        mass2.ApplyForce(-force);
    }

    void Update()
    {
        Vector3 dir = mass1.transform.position - mass2.transform.position;

        transform.localPosition = Vector3.Normalize(dir) * springLength;
        // make the spring look at the mass it is connected to
        transform.LookAt(mass1.transform);
    }
}
