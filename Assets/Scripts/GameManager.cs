using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance; // outros scripts possam acessar variaveis ou metodos publicos desta classe


    //variaveis da arvore
    public GameObject[] troncos;
    public List<GameObject> listaTroncos;

    private float alturaTronco = 2.43f;
    private float posicaoInicialY = -2.38f;
    private int maxTroncos = 6;
    private bool troncoSemGalho = false;

    // var pontuacao
    public Text pontuacao;
    private int pontos = 0;

    // variaveis tempo
    public Image barraTempo;
    private float larguraBarra = 188f;

    private float tempoJogo = 10;
    private float tempoExtra = 0.115f;
    private float tempoAtual;

    public bool gameOver = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        tempoAtual = tempoJogo;
        InicializaTroncos();
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameOver)
        {
            DiminuiBarra();
            /*if (Input.GetButtonDown("Left") || Input.GetButtonDown("Right"))
            {
                CortaTronco();
                ReposicionaTronco();
                SomaPontos();
                SomaTempo();
            }*/
        }
	}

    void CriaTroncos(int posicao)
    {
        GameObject tronco = Instantiate(troncoSemGalho ? troncos[Random.Range(0, 3)] : troncos[0]);
        tronco.transform.localPosition = new Vector3(0f, posicaoInicialY + posicao * alturaTronco, 0f);
        listaTroncos.Add(tronco);
        troncoSemGalho = !troncoSemGalho;
    }

    void InicializaTroncos()
    {
        for (int posicao = 0; posicao <= maxTroncos; posicao++)
        {
            CriaTroncos(posicao);
        }
    }

    void CortaTronco()
    {
        Destroy(listaTroncos[0]);
        listaTroncos.RemoveAt(0);
        SoundManager.instance.PlayFx(SoundManager.instance.fxCorte);
    }

    void ReposicionaTronco()
    {
        for (int posicao = 0; posicao < listaTroncos.Count; posicao++)
        {
            listaTroncos[posicao].transform.localPosition = new Vector3(0f, posicaoInicialY + posicao * alturaTronco, 0f);
        }
        CriaTroncos(maxTroncos);
    }

    void SomaPontos()
    {
        pontos++;
        pontuacao.text = pontos.ToString();
    }

    void SomaTempo()
    {
        if(tempoAtual+tempoExtra < tempoJogo)
        {
            tempoAtual += tempoExtra;
        }
    }

    void DiminuiBarra()
    {
        tempoAtual -= Time.deltaTime;

        float tempo = tempoAtual / tempoJogo;
        float pos = larguraBarra - (tempo * larguraBarra);

        barraTempo.transform.localPosition = new Vector2(-pos, barraTempo.transform.localPosition.y);

        if(tempoAtual <= 0f)
        {
            gameOver = true;
            SalvaPontuacao();
        }
    }

    public void SalvaPontuacao()
    {
        if (PlayerPrefs.GetInt("best") < pontos)
        {
            PlayerPrefs.SetInt("best", pontos);
        }
        PlayerPrefs.SetInt("score", pontos);
        SoundManager.instance.PlayFx(SoundManager.instance.fxMorte);
        Invoke("ChamaCenaGameOver", 2f);
    }

    public void ChamaCenaGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void Toque()
    {
        if (!gameOver)
        {
            CortaTronco();
            ReposicionaTronco();
            SomaPontos();
            SomaTempo();
        }
    }
}
