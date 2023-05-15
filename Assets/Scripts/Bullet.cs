using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform playerTransform;
    public float vitesse;

    [SerializeField]
    private Transform prefabWallSpark;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (LayerMask.LayerToName(collision.gameObject.layer) == "Zombie")
        {
            Destroy(collision.gameObject);
        }

        if (LayerMask.LayerToName(collision.gameObject.layer) == "Mur")
        {
            Instantiate(prefabWallSpark, transform.position, Quaternion.identity);
        }
    }
}
