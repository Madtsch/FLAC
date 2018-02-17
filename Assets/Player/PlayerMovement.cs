using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;
    Vector3 currentDestination, clickPoint;
    AICharacterControl aiCharacterControl = null;
    GameObject walkTarget = null;

    bool isInDirectMode = false;

    // Todo - get rid of problem const + serialized
    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;

    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
        aiCharacterControl = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("walkTarget");

        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        print("Click");
        switch (layerHit)
        {
            case enemyLayerNumber:
                // navigate to the enemy
                print("Click on enemy");
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;
            case walkableLayerNumber:
                // navigate to point on the ground
                print("Click on ground");
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
            default:
                Debug.LogWarning("Warning: Can not move to unknown position!");
                return;
        }

    }

    // Todo make this get called again
    void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    
}

