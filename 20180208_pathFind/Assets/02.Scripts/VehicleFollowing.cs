using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleFollowing : MonoBehaviour {
    public Path path;
    public float spd = 20f;
    public float mass = 5f;

    public bool isLooping = true;

    private float curSpd;
    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;

    Vector3 velocity;

	void Start () {
        pathLength = path.length;
        curPathIndex = 0;
        velocity = transform.forward;
	}


    void Update()
    {
        curSpd = spd * Time.deltaTime;
        targetPoint = path.getPoint(curPathIndex);
        if (Vector3.Distance(transform.position, targetPoint) < path.radius)
        {
            if (curPathIndex < pathLength - 1)
            {
                curPathIndex++;
            }
            else
            {
                if (isLooping)
                    curPathIndex = 0;
                else
                    return;
            }
        }
        if (curPathIndex >= pathLength)
        {
            return;
        }
        if (curPathIndex >= pathLength - 1 && !isLooping)
        {
            velocity += steer(targetPoint, true);
        }
        else
        {
            velocity += steer(targetPoint);
        }
        transform.position += velocity;     //속도에 따라 차량 이동
        transform.rotation = Quaternion.LookRotation(velocity);     //속도에 따라 차량 회전
    }

    public Vector3 steer(Vector3 target, bool bFinalPoint = false)
    {
        Vector3 desiredVelocity = (target - transform.position);
        float dist = desiredVelocity.magnitude;
        desiredVelocity.Normalize();
        if (bFinalPoint && dist < 10f)
        {
            desiredVelocity *= (curSpd * (dist / 10f));
        }
        else
        {
            desiredVelocity *= curSpd;
        }
        Vector3 steeringForce = desiredVelocity - velocity;
        Vector3 acceleration = steeringForce / mass;
        return acceleration;
    }
}
