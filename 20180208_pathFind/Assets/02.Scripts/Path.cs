using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
    public bool bDebug = true;
    public float radius = 2f;
    public Vector3[] pointA;

    public float length
    {
        get
        {
            return pointA.Length;
        }
    }

    public Vector3 getPoint(int index)
    {
        return pointA[index];
    }

    private void OnDrawGizmos()
    {
        if (!bDebug) return;
        for (int i = 0; i < pointA.Length; i++)
        {
            if (i + 1 < pointA.Length)
            {
                Debug.DrawLine(pointA[i], pointA[i + 1], Color.red);
            }
        }
    }
}
