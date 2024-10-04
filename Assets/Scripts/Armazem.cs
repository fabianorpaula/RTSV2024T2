using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armazem : MonoBehaviour
{
    public int estoque_Carne = 0;
    public int estoque_Madeira = 0;
    private float meuTempo;
    public List<GameObject> MeusFazendeiros;
    public GameObject Trabalhador;


    //Informacoes de Locais
    public GameObject DestinoMadeira;
    public GameObject DestinoCarne;




    private void Start()
    {
        CriaTrabalhador("Cacador");
        CriaTrabalhador("Cacador");
        CriaTrabalhador("Cacador");
        CriaTrabalhador("Cacador");
        CriaTrabalhador("Lenhador");
    }


    private void CriaTrabalhador(string nomeTrabalho)
    {
        GameObject meuTrabalhador = Instantiate(Trabalhador,
            transform.position,
            Quaternion.identity);
        meuTrabalhador.GetComponent<Fazendeiro>().InformaCarne(DestinoCarne);
        meuTrabalhador.GetComponent<Fazendeiro>().InformaMadeira(DestinoMadeira);
        meuTrabalhador.GetComponent<Fazendeiro>().InformaArmazem(this.gameObject);
        meuTrabalhador.GetComponent<Fazendeiro>().DefineTrabalho(nomeTrabalho);
        MeusFazendeiros.Add(meuTrabalhador);
    }

    private void Update()
    {
        Temporizador();
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
