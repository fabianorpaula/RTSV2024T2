using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armazem : MonoBehaviour
{
    public string NomeJogador;
    
    public int estoque_Carne = 0;
    public int estoque_Madeira = 0;
    //Cada Casa Habita 5 Pessoas
    public int casas = 1;
    private float meuTempo;
    public List<GameObject> MeusFazendeiros;
    public GameObject Trabalhador;


    //Informacoes de Locais
    public GameObject DestinoMadeira;
    public GameObject DestinoCarne;

    public int qtdCacadores;
    public int qtdLenhadores;




    private void Start()
    {
        Time.timeScale = 5;
        CriaTrabalhador("Cacador");
        CriaTrabalhador("Cacador");
        CriaTrabalhador("Cacador");
        CriaTrabalhador("Lenhador");
        CriaTrabalhador("Lenhador");
    }


    private void CriaTrabalhador(string nomeTrabalho)
    {
        if (casas * 5 > MeusFazendeiros.Count)
        {



            if (estoque_Carne > 50)
            {

                estoque_Carne -= 50;
                GameObject meuTrabalhador = Instantiate(Trabalhador,
                    transform.position,
                    Quaternion.identity);
                meuTrabalhador.GetComponent<Fazendeiro>().InformaCarne(DestinoCarne);
                meuTrabalhador.GetComponent<Fazendeiro>().InformaMadeira(DestinoMadeira);
                meuTrabalhador.GetComponent<Fazendeiro>().InformaArmazem(this.gameObject);
                meuTrabalhador.GetComponent<Fazendeiro>().DefineTrabalho(nomeTrabalho);
                MeusFazendeiros.Add(meuTrabalhador);
                if(nomeTrabalho == "Cacador")
                {
                    qtdCacadores++;
                }
                if (nomeTrabalho == "Lenhador")
                {
                    qtdLenhadores++;
                }
            }
        }
    }


    private void CriarCasa()
    {
        if (estoque_Madeira > 100)
        {


            casas++;
            estoque_Madeira -= 100;
        }
    }

   
    private void Update()
    {
        Temporizador();
        if(estoque_Carne < 0 || estoque_Madeira < 0)
        {
            Time.timeScale = 0;
        }

        if (estoque_Carne > 270)
        {

            CriaTrabalhador("Cacador");
        }
        if (estoque_Madeira > 170)
        {
            if (casas * 5 == MeusFazendeiros.Count)
            {


                CriarCasa();
                CriaTrabalhador("Lenhador");
            }
        }


        //Para Mudar Trabalhador de Funcao
        //DevoMudarTrabalho("Lenhador");
        //DevoMudarTrabalho("Cacador");
        /*
        if(estoque_Madeira < 50)
        {
            DevoMudarTrabalho("Lenhador");
        }*/

    }

    public void ReceberCarne(int carne)
    {
        estoque_Carne += carne;
    }

    public void ReceberMadeira(int madeira)
    {
        estoque_Madeira+= madeira;
    }


    void Temporizador()
    {
        meuTempo += Time.deltaTime;
        if(meuTempo > 15)
        {
            estoque_Carne -= MeusFazendeiros.Count*3;
            estoque_Madeira -= MeusFazendeiros.Count *1;
            meuTempo = 0;
        }

    }

    public void DevoMudarTrabalho(string necessitoTrbalhador)
    {
        int numTrabalhador = Random.Range(0, MeusFazendeiros.Count);
        if(necessitoTrbalhador == "Lenhador")
        {
            if (MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().InformaTrabalho() == "Cacador")
            {
                MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().DefineTrabalho("Lenhador");
                qtdLenhadores++;
                qtdCacadores--;
            }
        }

        if (necessitoTrbalhador == "Cacador")
        {
            if (MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().InformaTrabalho() == "Lenhador")
            {
                MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().DefineTrabalho("Cacador");
                qtdLenhadores--;
                qtdCacadores++;
            }
        }

      
    }

}
