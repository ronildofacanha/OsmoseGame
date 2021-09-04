using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    public Text textGamePonto;
    public Text textGameErro;
    public Text textGameTempo;
    public Text textGameAcerto;
    public InputField nome;
    float timeLeft;

    public AudioClip[] clip;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetString("nome", "") != "")
        {
            nome.text = PlayerPrefs.GetString("nome");
            textGamePonto.text = (PlayerPrefs.GetString("ponto",""));
            textGameAcerto.text = (PlayerPrefs.GetString("acerto",""));
            textGameErro.text = (PlayerPrefs.GetString("erro",""));
            textGameTempo.text = (PlayerPrefs.GetString("tempo",""));
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
    }


    public void Login()
    {
        AudioSource.loop = false;
        AudioSource.clip = clip[0];
        AudioSource.Play();
            if (nome.text != "")
            {
                if (PlayerPrefs.GetString("nome", "") == "")
                {
                    PlayerPrefs.SetString("nome", nome.text);
                }
                 StartCoroutine(CallFunction(0.3f));
        }
    }
       

    IEnumerator CallFunction(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(CallFunction(time));
    }
}
