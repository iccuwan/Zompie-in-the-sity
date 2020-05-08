using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float HP;
    public float MoveSpeed;
    public bool Enemy;

    void Update()
    {
        
    }
    

    public enum UnitType
    {
        Spec,
        Zombie
    }
}
