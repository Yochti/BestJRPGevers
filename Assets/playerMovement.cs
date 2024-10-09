using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 5f;
    public float rotationSpeed = 500f;
    public float dashDistance = 5f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 1.5f;
    public Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;

   


    void Update()
    {


        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.Z))
        {
            movement.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y -= 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }

        movement = movement.normalized;
        rb.velocity = movement * moveSpeed;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        transform.up = direction;

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }




    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector2 startPosition = transform.position;
        Vector2 dashDirection = transform.up; // Utilise la direction vers laquelle le joueur regarde
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            transform.Translate(dashDirection * (dashDistance / dashDuration) * Time.deltaTime, Space.World);
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
