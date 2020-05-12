using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Unit
{
    private Transform target;
    void Start()
    {

    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Spec").GetComponent<Transform>();
        if (Vector2.Distance(transform.position, target.position) < 10 && Vector2.Distance(transform.position, target.position) > 0.7)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
}
