using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float speed;

    private float x, y;
    public bool StopMovement { get; set; }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (StopMovement)
            return;
        transform.position += new Vector3(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);
    }
}
