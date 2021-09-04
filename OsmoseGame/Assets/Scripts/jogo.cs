using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class jogo : MonoBehaviour
{
    public GameObject clickBlock;
    public GameObject GameOverTela;
    public GameObject Grid;
    public GameObject[] telaQuest;
    public Text textGamePonto;
    public Text textGameErro;
    public Text textGameTempo;
    public Text textGameAcerto;
    public Button[] Buttons;
    //sond
    public AudioClip[] clip;
    public AudioSource AudioSource;
    float timeLeft = 2;
    int gamePonto = 0;
    int gameErro = 0;
    int gameAcertos = 0;
    int nQuest = 0;
    float tempo = 0;
    bool ponto = false;

    public void limpar()
    {
        PlayerPrefs.SetString("chave1", "");
        PlayerPrefs.SetString("chave2", "");
        PlayerPrefs.SetInt("key1", 0);
        PlayerPrefs.SetInt("key2", 0);
        gamePonto = 0;
        gameErro = 0;
        gameAcertos = 0;
        textGamePonto.text = "PONTOS:\n0";
        textGameErro.text = "ERROS:\n0";
        textGameTempo.text = "TEMPO:\n0";
        textGameAcerto.text = "ACERTOS:\n0";
    }

    private void Start()
    {
       // PlayerPrefs.DeleteAll();
        limpar();
        randomOrder();
        /*print(PlayerPrefs.GetString("ponto"));
        print(PlayerPrefs.GetString("acerto"));
        print(PlayerPrefs.GetString("erro"));
        print(PlayerPrefs.GetString("tempo"));*/
    }

    private void Update()
    {
        gameTempo();

        if (PlayerPrefs.GetInt("key1") == 0 && PlayerPrefs.GetInt("key2") == 0)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
            {

                if (ponto == true)
                {
                    btn_Wins();
                    ponto = false;
                    telaQuest[nQuest-1].SetActive(true);
                }

                foreach (Button btn in Buttons)
                {
                    btn.interactable = true;

                    if (btn.enabled.Equals(false))
                    {
                        btn.interactable = false;
                    }
                }

                clickBlock.SetActive(false);

                if (game_Over())
                {
                    GameOverTela.SetActive(true);
                }
                timeLeft = 2;
            }
        }
    }

    public void btn_Chave(string item)
    {
        AudioSource.clip = clip[0];
        AudioSource.Play();

        if (!game_Over())
        {
            if (PlayerPrefs.GetString("chave1").Equals("") && PlayerPrefs.GetInt("key1") == 0)
            {
                PlayerPrefs.SetString("chave1", item);
                PlayerPrefs.SetInt("key1", 1);
              //  print(PlayerPrefs.GetString("chave1"));
                return;
            }
            else if (PlayerPrefs.GetString("chave2").Equals("") && PlayerPrefs.GetInt("key2") == 0)
            {
                PlayerPrefs.SetString("chave2", item);
                PlayerPrefs.SetInt("key2", 1);
              //  print(PlayerPrefs.GetString("chave2"));

                // If Chave == Chave
                if (PlayerPrefs.GetString("chave1").Equals(PlayerPrefs.GetString("chave2")))
                {
                    print("#VENCEU# " + PlayerPrefs.GetString("chave1") + " == " + PlayerPrefs.GetString("chave2"));
                    PlayerPrefs.SetString("chave1", "");
                    PlayerPrefs.SetString("chave2", "");
                    PlayerPrefs.SetInt("key1", 0);
                    PlayerPrefs.SetInt("key2", 0);
                    ponto = true;
                    gamePonto += 1;
                    textGamePonto.text = "PONTOS:\n"+gamePonto;
                    clickBlock.SetActive(true);
                    nQuest += 1;
                    AudioSource.clip = clip[1];
                    AudioSource.Play();
                }
                else
                {
                    print("#PERDEU# " + PlayerPrefs.GetString("chave1") + " == " + PlayerPrefs.GetString("chave2"));
                    PlayerPrefs.SetString("chave1", "");
                    PlayerPrefs.SetString("chave2", "");
                    PlayerPrefs.SetInt("key1", 0);
                    PlayerPrefs.SetInt("key2", 0);
                    clickBlock.SetActive(true);
                    gameErro += 1;
                    textGameErro.text = "ERROS:\n" + gameErro;
                    AudioSource.clip = clip[2];
                    AudioSource.Play();
                }
            }
        }
            
    }

    public void btn_InteracFalse(Button button)
    {
        button.interactable = false;
    }

    public void btn_Wins()
    {
        foreach (Button btn in Buttons)
        {
            if (btn.interactable.Equals(false))
            {
                btn.enabled = false;
             //   print("FALSE = " + btn);
            }
        }
    }

    public bool game_Over()
    {
            if ((Buttons.Length/2) == gamePonto)
            {
            // print("ACABOU");
            /*dados
             textGamePonto.text = "PONTOS:\n0";
             textGameErro.text = "ERROS:\n0";
             textGameTempo.text = "TEMPO:\n0";
             textGameAcerto.text = "ACERTOS:\n0";
             */
                return true;
            }
        return false;
    }

    public void Voltar()
    {
        if (PlayerPrefs.GetString("ponto", "") == "")
        {
            /*   int gamePonto = 0;
    int gameErro = 0;
    int gameAcertos = 0;
    int nQuest = 0;*/
            PlayerPrefs.SetString("ponto", gamePonto.ToString());
            PlayerPrefs.SetString("erro", gameErro.ToString());
            PlayerPrefs.SetString("tempo", textGameTempo.text);
            PlayerPrefs.SetString("acerto", gameAcertos.ToString());
        }

        SceneManager.LoadSceneAsync(0);
    }

    public void gameTempo()
    {
        if (!game_Over())
        {
            tempo += Time.deltaTime;
            float minutes = Mathf.FloorToInt(tempo / 60);
            float seconds = Mathf.FloorToInt(tempo % 60);

            textGameTempo.text = "TEMPO:\n" + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void gameAcerto(int resposta)
    {
        if(resposta == 0)
        {
            gameErro += 1;
            textGameErro.text = "ERROS:\n" + gameErro;
            AudioSource.clip = clip[2];
            AudioSource.Play();
        }
        else if (resposta ==1)
        {
            gameAcertos += 1;
            textGameAcerto.text = "ACERTOS:\n" + gameAcertos;
            AudioSource.clip = clip[1];
            AudioSource.Play();
        }
    }

    public void randomOrder()
    {
        int[] v = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19};

        int temp;

        for (int i = 0; i < 20; i++)
        {

            int rnd = Random.Range(0, v.Length);
            temp = v[rnd];
            v[rnd] = v[i];
            v[i] = temp;
            //
            Buttons[v[i]].transform.parent = Grid.transform;
            print(v[i]);
        }
        
        foreach(Button item in Buttons)
        {
            item.transform.parent = Grid.transform;
        }

        //textGamePonto.transform.parent = Grid.transform;
       // textGameErro.transform.parent = Grid.transform;
       // textGameTempo.transform.parent = Grid.transform;
       // textGameAcerto.transform.parent = Grid.transform;

    }

    public void suffle(int[] v)
    {
        int temp;
        for (int i = 0; i < v.Length; i++)
        {
            int rnd = Random.Range(0, v.Length);
            temp = v[rnd];
            v[rnd] = v[i];
            v[i] = temp;
        }
    }
}
  
