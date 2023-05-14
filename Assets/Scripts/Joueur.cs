using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private Animator animFeet;
    public Transform cameraTransform;
    public GameObject feet;

    [SerializeField]
    private Transform prefabFlash;
    [SerializeField]
    private Transform prefabBullet;
    [SerializeField]
    private Transform bulletSource;
    [SerializeField]
    private float vitesse;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        animFeet = feet.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rig.velocity.magnitude > 0)
            anim.SetFloat("Bouger", rig.velocity.magnitude);
        if (Input.GetKeyDown(KeyCode.Mouse0)) { 
            anim.SetInteger("Tirer", 1);
            Instantiate(prefabBullet, bulletSource.position, Quaternion.identity);
            Instantiate(prefabFlash, bulletSource.position, Quaternion.identity);
        }
        else
            anim.SetInteger("Tirer", 0);
        animFeet.SetFloat("Bouger", rig.velocity.magnitude);

        Vector2 lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 5f, Vector3.forward);
        transform.rotation = rotation;

        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }

    private void FixedUpdate()
    {
        rig.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * vitesse);
    }
}
