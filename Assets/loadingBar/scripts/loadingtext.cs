using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingtext : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;

    public float speed = 1f;
    public Text text;
    public Text textNormal;


    // Use this for initialization
    void Awake () {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }

    private void Start()
    {
        StartCoroutine(StartLoading());
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator StartLoading()
    {
        int a = 0;
        while (a < 100)
        {
            imageComp.fillAmount = imageComp.fillAmount + Time.deltaTime * speed;
            a = (int)(imageComp.fillAmount * 100);
            if (a > 0 && a <= 33)
            {
                textNormal.text = "Loading...";
            }
            else if (a > 33 && a <= 67)
            {
                textNormal.text = "Downloading...";
            }
            else if (a > 67 && a <= 100)
            {
                textNormal.text = "Please wait...";
            }
            else
            {

            }
            text.text = a + "%";

            yield return null;
        }

        LoadNextScene();

        yield return new WaitForSeconds(1.0f);

        imageComp.fillAmount = 0.0f;
        text.text = "0%";
    }
}
