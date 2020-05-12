using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float hp;
    public float moveSpeed;
    public float rotationSpeed;
    public bool enemy;

    [HideInInspector]
    public float lookAtDeg = 0f;
    [HideInInspector]
    public Vector2 goTo;
    [HideInInspector]
    public UnitStatus unitStatus = UnitStatus.Idle;

    public void Move(Vector2 destination)
    {
        unitStatus = UnitStatus.Walking;
        float angleRad = Mathf.Atan2(destination.y - transform.position.y, destination.x - transform.position.x);
        lookAtDeg = (180 / Mathf.PI) * angleRad;
        goTo = destination;
    }
    

    public enum UnitType
    {
        Spec,
        Zombie
    }

    public enum UnitStatus
    {
        Idle,
        Walking,
        Attacking,
        Reloading
    }
}
