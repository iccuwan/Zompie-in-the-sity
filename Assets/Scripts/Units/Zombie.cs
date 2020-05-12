using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Unit
{
    public float attackDistance = 1f;

    private List<Spec> targetList = new List<Spec>();
    private Spec target = null;
    private readonly UnitType unitType = UnitType.Zombie;

    void Start()
    {

    }

    void Update()
    {
        if (targetList.Count > 0)
        {
            Spec nearest = null;
            float nearestDistance = 0f;
            foreach (Spec s in targetList)
            {
                float distance = Vector2.Distance(transform.position, s.transform.position);
                if (nearest != null)
                {
                    if (nearestDistance > distance)
                    {
                        nearest = s;
                        nearestDistance = distance;
                        continue;
                    }
                    continue;
                }
                nearest = s;
                nearestDistance = distance;
            }
            target = nearest;
            Move(target.transform.position);
        }
        if (target != null)
        {
            // По хорошему это надо сделать через Unit.cs, как сделано для Spec
            float rad = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x);
            float deg = (180 / Mathf.PI) * rad;
            if (Vector2.Distance(transform.position, goTo) > attackDistance)
            {
                unitStatus = UnitStatus.Walking;
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, deg), rotationSpeed * Time.deltaTime);
            }
            else
            {
                unitStatus = UnitStatus.Attacking;
                Debug.Log("Zombie attacking");
            }
        }
        else
        {
            unitStatus = UnitStatus.Idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spec")
        {
            targetList.Add(collision.GetComponent<Spec>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Spec")
        {
            Spec s = collision.GetComponent<Spec>();
            if (s == target)
            {
                target = null;
            }
            targetList.Remove(s);
        }
    }
}
