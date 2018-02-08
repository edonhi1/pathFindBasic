using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugs : MonoBehaviour {
    public float mSpd;
    public float rSpd;
    public float senserLength;
    public float turnCool;
    public float turnDelay;

    public enum BUGSTATE
    {
        IDLE,
        LEFT,
        RIGHT,
        FORWARD
    }
    public BUGSTATE bugState;

	void Update () {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, senserLength))
        {
            if(hit.transform.tag == "Wall" && bugState!=BUGSTATE.LEFT && bugState!=BUGSTATE.RIGHT)
            {
                int ranState = Random.Range(0, 2);
                switch (ranState)
                {
                    case 0:
                        bugState = BUGSTATE.LEFT;
                        break;
                    case 1:
                        bugState = BUGSTATE.RIGHT;
                        break;
                }
            }
            //else
            //{
            //    bugState = BUGSTATE.FORWARD;
            //}
        }
        switch (bugState)
        {
            case BUGSTATE.IDLE:
                break;
            case BUGSTATE.LEFT:
                transform.Rotate(0, rSpd, 0);
                turnCool += Time.deltaTime;
                if (turnCool > turnDelay)
                {
                    bugState = BUGSTATE.FORWARD;
                }
                break;
            case BUGSTATE.RIGHT:
                transform.Rotate(0, -rSpd, 0);
                turnCool += Time.deltaTime;
                if (turnCool > turnDelay)
                {
                    bugState = BUGSTATE.FORWARD;
                }
                break;
            case BUGSTATE.FORWARD:
                turnCool = 0;
                transform.Translate(0, 0, mSpd * Time.deltaTime);
                break;
        }
    }
}
