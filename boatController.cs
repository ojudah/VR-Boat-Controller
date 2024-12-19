using UnityEngine;
using System.Collections;

public class BoatController : MonoBehavior {
    CharacterController cc;
    CharacterMotor cm;
    GameObject player;
    Transform defaultPlayerTransfrom;

    float startY;

    bool isDriving = false;

    void Start () {
        cc = GameObject.FindObjectOfType<CharacterController>();
        cm = GameObject.FindObjectOfType<CharacterMotor>();
        player = cm.gameObject;
        defaultPlayerTransfrom = player.transform.parent;

        startY=gameObject.transform.position.y;
    }

    bool IsPlayerCloseToBoat()
    {
        return Vector3.Distance(gameObject.transform.position, player.transform.position)<2;
    }

    void SetDriving(bool isDriving){
        this.isDriving = isDriving;

        cm.enabled = !isDriving;
        cc.enabled = !isDriving;

        if (isDriving)
            player.transform.parent = gameObject.transform;
        else
            player.transform.parent = defaultPlayerTransfrom;

    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerCloseToBoat())
        SetDriving(!isDriving);

        if (isDriving)
        {
            float forwardThrust = 0;
            if(Input.GetKey(KeyCode.W))
                forwardThrust = 3
            if(Input.GetKey(KeyCode.S))
                forwardThrust = -1
            
            rigidbody.AddForce(gameObject.transform.forward*forwardThrust);

            float turnThrust = 0;
            if(Input.GetKey(KeyCode.A))
                turnThrust = -1
            if(Input.GetKey(KeyCode.D))
                turnThrust = 1

            rigidbody.AddRelativeTorque(Vector3.up*turnThrust);
        }

        rigidbody.velocity=Vector3.ClampMagnitude(rigidbody.velocity, 5);

        Vector3 newPosition=gameObject.transform.position;
        newPosition.y=startY+MathF.Sin(Time.timeSinceLevelLoad*2)/8;
        gameObject.transform.position=newPosition;
    }
}