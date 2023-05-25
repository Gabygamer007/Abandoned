using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SpriteRenderer rendu;
    public Transform fondu;
    public Transform title;
    public Transform subTitle;
    [SerializeField]
    private float taux;
    [SerializeField]
    private float fallingSpeed;

    void Start()
    {
        rendu = fondu.GetComponent<SpriteRenderer>();

        PlayerPrefs.SetInt("nbVie", 3);
        PlayerPrefs.SetInt("nbBalles", 16);
        PlayerPrefs.SetString("choix", "");

        StartCoroutine(TomberTitre());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(FonduSorti());
    }

    public void Quit()
    {
        Application.Quit();
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
        SceneManager.LoadScene("Level_1");
    }

    IEnumerator TomberTitre()
    {
        RectTransform titleRectTransform = title.GetComponent<RectTransform>();
        Vector2 targetPosition = new Vector2(titleRectTransform.anchoredPosition.x, 400f);

        while (titleRectTransform.anchoredPosition.y > targetPosition.y)
        {
            Vector2 tempPos = titleRectTransform.anchoredPosition;
            tempPos.y -= fallingSpeed * Time.fixedDeltaTime;
            titleRectTransform.anchoredPosition = tempPos;
            yield return null;
        }
        titleRectTransform.anchoredPosition = targetPosition;

        yield return new WaitForSeconds(1f);

        RectTransform subtitleRectTransform = subTitle.GetComponent<RectTransform>();
        targetPosition = new Vector2(subtitleRectTransform.anchoredPosition.x, 300f);

        while (subtitleRectTransform.anchoredPosition.y > targetPosition.y)
        {
            Vector2 tempPos = subtitleRectTransform.anchoredPosition;
            tempPos.y -= fallingSpeed * Time.fixedDeltaTime;
            subtitleRectTransform.anchoredPosition = tempPos;
            yield return null;
        }
        subtitleRectTransform.anchoredPosition = targetPosition;



    }
}
