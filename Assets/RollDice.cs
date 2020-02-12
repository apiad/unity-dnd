using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    Rigidbody body;
    float force;
    bool dragging;

    public float PerSecondTorque = 1;
    public float PerSecondSpeed = 1;
    public float PullForce = 10;
    public float Damping = 0.95f;

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

        if (Input.GetMouseButtonDown(0))
        {
            this.body.useGravity = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.body.useGravity = true;
        }
        else if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(mousePos);

            float enter;

            if (new Plane(Vector3.up, -4).Raycast(ray, out enter))
            {
                var point = ray.GetPoint(enter);
                var dir = point - this.transform.position;

                this.body.AddForce(dir * this.PullForce);
                this.body.AddTorque(t, t, t);
                this.body.velocity *= this.Damping;
            }

            return;
        }

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
