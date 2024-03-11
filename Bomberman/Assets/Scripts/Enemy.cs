using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject player;
    private bool hasLightOfSight = false;

    private void Start() => player = GameObject.FindGameObjectWithTag("Player");

    private void Update()
    {
        if (hasLightOfSight)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (ray.collider != null)
        {
            hasLightOfSight = ray.collider.CompareTag("Player");
            Debug.DrawRay(transform.position, player.transform.position - transform.position, hasLightOfSight ? Color.green : Color.red);
        }
    }
}
