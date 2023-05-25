using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Joueur : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private Animator animFeet;
    private bool invincible = false;
    public int vie = 3;
    public Transform cameraTransform;
    public GameObject feet;
    private SpriteRenderer rendu;
    private bool mort = false;
    public int ballesRestantes = 16;
    private bool peuxTirer = true;

    public Transform fondu;

    [SerializeField]
    private float taux;
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
    [SerializeField]
    private Transform texteBallesRestantes;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        animFeet = feet.GetComponent<Animator>();

        rendu = fondu.GetComponent<SpriteRenderer>();

        texteBallesRestantes.GetComponent<TMP_Text>().text = ballesRestantes.ToString();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_2"))
        {
            ballesRestantes = PlayerPrefs.GetInt("nbBalles");
            vie = PlayerPrefs.GetInt("nbVie");
            if (PlayerPrefs.GetString("choix") == "balles")
            {
                ballesRestantes = 16;
            }
            else if (PlayerPrefs.GetString("choix") == "vie")
            {
                vie = 3;
            }
        }

        StartCoroutine(FonduEntre());
    }

    // Update is called once per frame
    void Update()
    {
        if (!mort)
        {
            if (rig.velocity.magnitude > 0)
                anim.SetFloat("Bouger", rig.velocity.magnitude);
            if (Input.GetKeyDown(KeyCode.Mouse0) && peuxTirer)
            {
                anim.SetInteger("Tirer", 1);
                if (ballesRestantes > 0)
                {
                    Instantiate(prefabBullet, bulletSource.position, Quaternion.identity);
                    Instantiate(prefabFlash, bulletSource.position, Quaternion.identity);
                    ballesRestantes -= 1;
                    StartCoroutine(AttendreTir());
                }

            }
            else
                anim.SetInteger("Tirer", 0);
            animFeet.SetFloat("Bouger", rig.velocity.magnitude);

            Vector2 lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 5f, Vector3.forward);
            transform.rotation = rotation;

            cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
            setCoeurs();
            setBalles();
        }
    }

    private void FixedUpdate()
    {
        rig.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * vitesse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Zombie" && !invincible)
        {
            vie -= 1;
            StartCoroutine(Invincible());
        }
    }

    private void setCoeurs()
    {
        if (vie < 3)
        {
            coeur3.GetComponent<SpriteRenderer>().color = Color.black;
        }
        if (vie < 2)
        {
            coeur2.GetComponent<SpriteRenderer>().color = Color.black;
        }
        if (vie < 1)
        {
            coeur1.GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(Mort());
        }
    }

    private void setBalles()
    {
        texteBallesRestantes.GetComponent<TMP_Text>().text = ballesRestantes.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("nbVie", vie);
        PlayerPrefs.SetInt("nbBalles", ballesRestantes);
        StartCoroutine(FonduSorti());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    IEnumerator FonduEntre()
    {
        Color colTemp = Color.black;
        while (rendu.color.a > 0.0f)
        {
            colTemp.a -= taux;
            rendu.color = colTemp;
            yield return new WaitForEndOfFrame();
        }
        colTemp.a = 0.0f;
        rendu.color = colTemp;
    }

    IEnumerator FonduSorti()
    {
        Color colTemp = rendu.color;
        while (rendu.color.a < 1.0f)
        {
            colTemp.a += taux;
            rendu.color = colTemp;
            yield return new WaitForEndOfFrame();
        }
        colTemp.a = 1.0f;
        rendu.color = colTemp;
    }
    
    IEnumerator AttendreTir()
    {
        peuxTirer = false;
        yield return new WaitForSeconds(0.5f);
        peuxTirer = true;
    }

    IEnumerator Mort()
    {
        mort = true;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        StartCoroutine(FonduSorti());
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}
