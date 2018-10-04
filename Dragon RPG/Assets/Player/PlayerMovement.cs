using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
   

    //Stop movement within 20cm to stop animator from never reaching target
    [SerializeField] float walkMoveStopRadius = 0.2f;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    bool isInDirectMode = false; //TODO consider making static later

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics

    
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) //G for gamepad. TODO add to menu
        {
            isInDirectMode = !isInDirectMode; //toogle mode
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProccesMouseMovement();
        }
    }

    private void ProccesMouseMovement()
    {
        if (Input.GetMouseButton(1))
        {
            print("Cursor raycast hit layer: " + cameraRaycaster.layerHit);
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;  // So not set in default case
                    break;

                case Layer.Enemy:
                    print("Not moving enemy");
                    break;

                default:
                    print("Unexpected layer found");
                    return;

            }




        }

        var playerToClickPoint = currentClickTarget - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            m_Character.Move(playerToClickPoint, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);

        }
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;


        m_Character.Move(m_Move, false, false);
    }
}

