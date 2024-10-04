using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armazem : MonoBehaviour
{
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

        if (estoque_Carne > 250)
        {

            CriaTrabalhador("Cacador");
        }
        if (estoque_Madeira > 150)
        {
            if (casas * 5 == MeusFazendeiros.Count)
            {


                CriarCasa();
                CriaTrabalhador("Lenhador");
            }
        }
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
        if(meuTempo > 10)
        {
            estoque_Carne -= MeusFazendeiros.Count*3;
            estoque_Madeira -= MeusFazendeiros.Count *1;
            meuTempo = 0;
        }

    }
}
