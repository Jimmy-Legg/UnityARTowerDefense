using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        // Set the target to the end point
        target = GameObject.FindGameObjectWithTag("EndPoint").transform;
    }

    void Update()
    {
        // Move towards the target
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        // Check if reached the target
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            // If reached, reduce player health and destroy the enemy
            PlayerStats.health -= enemy.health;
            Destroy(gameObject);
        }

        // Reset speed
        enemy.speed = enemy.startSpeed;
    }
}
