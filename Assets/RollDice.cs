using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    Rigidbody body;
    float force;

    public float PerSecondTorque = 1;
    public float PerSecondSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        this.body = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float d = Time.deltaTime;
        float t = d * this.PerSecondTorque;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.body.MovePosition(new Vector3(0, 4, 0));
            this.body.MoveRotation(Random.rotation);
            this.body.velocity = Vector3.zero;
            this.body.angularVelocity = Vector3.zero;
            this.force = 0;
            this.body.useGravity = false;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            this.body.AddTorque(t, 0, t);
            this.force += d * this.PerSecondSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            this.body.AddForce(this.force * new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f));
            this.body.useGravity = true;
        }
    }
}
