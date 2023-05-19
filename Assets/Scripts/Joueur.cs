using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private Animator animFeet;
    private bool invincible = false;
    private int vie = 3;
    public Transform cameraTransform;
    public GameObject feet;

    [SerializeField]
    private Transform coeur1;
    [SerializeField]
    private Transform coeur2;
    [SerializeField]
    private Transform coeur3;
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
        rig.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * vitesse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Zombie" && !invincible)
        {
            if (vie == 3)
            {
                vie -= 1;
                coeur3.GetComponent<SpriteRenderer>().color = Color.black;
                StartCoroutine(Invincible());
            }
            else if (vie == 2)
            {
                vie -= 1;
                coeur2.GetComponent<SpriteRenderer>().color = Color.black;
                StartCoroutine(Invincible());
            }
        }
    }

    IEnumerator Invincible()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer feetSprite = feet.GetComponent<SpriteRenderer>();
        invincible = true;
        Color changedColor = spriteRenderer.color;
        changedColor.a = 0.5f;
        spriteRenderer.color = changedColor;
        feetSprite.color = changedColor;
        yield return new WaitForSeconds(0.375f);
        changedColor.a = 1f;
        spriteRenderer.color = changedColor;
        feetSprite.color = changedColor;
        yield return new WaitForSeconds(0.375f);
        changedColor.a = 0.5f;
        spriteRenderer.color = changedColor;
        feetSprite.color = changedColor;
        yield return new WaitForSeconds(0.375f);
        changedColor.a = 1f;
        spriteRenderer.color = changedColor;
        feetSprite.color = changedColor;
        yield return new WaitForSeconds(0.375f);
        changedColor.a = 0.5f;
        spriteRenderer.color = changedColor;
        feetSprite.color = changedColor;
        yield return new WaitForSeconds(0.375f);
        changedColor.a = 1f;
        spriteRenderer.color = changedColor;
        feetSprite.color = changedColor;
        yield return new WaitForSeconds(0.375f);
        changedColor.a = 0.5f;
        spriteRenderer.color = changedColor;
        feetSprite.color = changedColor;
        yield return new WaitForSeconds(0.375f);
        changedColor.a = 1f;
        spriteRenderer.color = changedColor;
        feetSprite.color = changedColor;
        yield return new WaitForSeconds(0.375f);
        invincible = false;
    }
}
