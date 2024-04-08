using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    public AudioClip deathScreenSound;
    AudioSource source;
    Image death;
    Text deathText;
    GameObject buttons;
    bool isFaded = false;

    void OnEnable()
    {
        death = transform.Find("Image").GetComponent<Image>();
        deathText = transform.Find("Text").GetComponent<Text>();
        buttons = transform.Find("Buttons").gameObject;
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        deathText.canvasRenderer.SetAlpha(0.0f);
        foreach (Transform button in buttons.transform)
        {
            button.Find("Text").GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        }
        source.PlayOneShot(deathScreenSound);
    }

    void Update()
    {
        if (isFaded == false)
        {
            isFaded = true;
            deathText.CrossFadeAlpha(1.0f, 1.0f, false);
            foreach (Transform button in buttons.transform)
            {
                button.Find("Text").GetComponent<Text>().CrossFadeAlpha(1.0f, 1.0f, false);
            }
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
