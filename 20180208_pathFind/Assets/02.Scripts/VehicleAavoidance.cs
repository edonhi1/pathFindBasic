using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAavoidance : MonoBehaviour {
    public float spd = 20f;
    public float mass = 5f;
    public float force = 50f;
    public float minimumDistToAvoid = 20f;

    private float curSpd;
    private Vector3 targetPoint;

	void Start () {
        mass = 5f;
        targetPoint = Vector3.zero;
	}

    private void OnGUI()
    {
        GUILayout.Label("Click anywhere to move the vehicle.");
    }

    void Update () {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButtonDown(0)&&Physics.Raycast(ray, out hit, 100f))
        {
            targetPoint = hit.point;
        }
        Vector3 dir = (targetPoint - transform.position);
        dir.Normalize();
        AvoidObstacles(ref dir);

        if (Vector3.Distance(targetPoint, transform.position) < 3f)
        {
            return;
        }
        curSpd = spd * Time.deltaTime;
        var rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5f * Time.deltaTime);

        transform.position += transform.forward * curSpd;
	}

    public void AvoidObstacles(ref Vector3 dir)
    {
        RaycastHit hit;
        int layerMask = 1 << 8;
        if (Physics.Raycast(transform.position, transform.forward, out hit, minimumDistToAvoid, layerMask))
        {
            Vector3 hitNormal = hit.normal;
            hitNormal.y = 0f;
            dir = transform.forward + hitNormal * force;
        }
    }
}
