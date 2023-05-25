using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float longueur;

    private Vector3 direction;
    private Vector2 mouvement;

    [SerializeField]
    private string nomCible;

    private Transform cible;

    private Rigidbody2D rig;
    private Animator anim;
    public float vitesse;
    private bool estDetecte = false;

    public Transform prefabSang;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find(nomCible);

        if (obj != null)
        {
            cible = obj.transform;
        }

        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Horizontal", mouvement.x);
        anim.SetFloat("Vertical", mouvement.y);
    }

    public void FixedUpdate()
    {

        if (cible != null)
        {
            direction = cible.position - transform.position;
            direction.Normalize();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, longueur, LayerMask.GetMask("Joueur", "Mur", "Boite"));
            if (hit.collider != null && cible.GetComponent<BoxCollider2D>())
            {
                if (LayerMask.LayerToName(hit.transform.gameObject.layer) == "Joueur")
                {
                    mouvement = direction;
                    estDetecte = true;
                    Vector2 dir = hit.transform.gameObject.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    rig.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    if (estDetecte)
                        mouvement = new Vector2();
                    estDetecte = false;
                }
            }
            else
            {
                if (estDetecte)
                    mouvement = new Vector2();
                estDetecte = false;
            }
            rig.velocity = mouvement * vitesse;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Bullet")
        {
            Destroy(gameObject);
            Instantiate(prefabSang, gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

}
