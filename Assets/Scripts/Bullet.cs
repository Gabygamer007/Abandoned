using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform playerTransform;
    public float vitesse;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Joueur").transform;
        Vector2 direction = playerTransform.right; // assuming the player is facing along the x-axis
        direction.Normalize();
        rig.velocity = direction * vitesse;
        transform.right = direction;
    }

    void FixedUpdate()
    {
        rig.velocity = transform.right * vitesse;
    }
}
