using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armazem : MonoBehaviour
{
    public string NomeJogador;
    
    public int estoque_Carne = 0;
    public int estoque_Madeira = 0;
    public int estoque_Ouro = 0;
    public int pontos_Riqueza = 0;
    //Cada Casa Habita 5 Pessoas
    public int casas = 1;
    private float meuTempo;
    public List<GameObject> MeusFazendeiros;
    public GameObject Trabalhador;


    //Informacoes de Locais
    public GameObject DestinoMadeira;
    public GameObject DestinoCarne;
    public GameObject DestinoRiqueza;
    public GameObject DestinoOuro;

    public int qtdCacadores;
    public int qtdLenhadores;

    public int incrementoBolsa = 0;

    public bool estadoVila = true;

    public bool primeiraCriacao = false;

    private void Start()
    {
        Time.timeScale = 5;
        
        
        
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
                meuTrabalhador.GetComponent<Fazendeiro>().InformaRiqueza(DestinoRiqueza);
                meuTrabalhador.GetComponent<Fazendeiro>().InformaOuro(DestinoOuro);
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

   private void EvoluaTrabalhador()
    {
        if(estoque_Ouro >= 100)
        {
            estoque_Ouro -= 100;
            incrementoBolsa++;
        }

    }

    void TempoDeJogo()
    {
        if (estoque_Ouro > 100)
        {
            EvoluaTrabalhador();
        }



        Temporizador();
        if (estoque_Carne < 0 || estoque_Madeira < 0)
        {
            //Time.timeScale = 0;
            Morreu();
        }
    }

    

    void Morreu()
    {
        estadoVila = false;
    }

    private void Update()
    {

        TempoDeJogo();

        if (estadoVila)
        {
            CodigoGame();
            
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

    public void ReceberOuro(int ouro)
    {
        estoque_Ouro += ouro;
    }

    public void InformaRiqueza()
    {
        pontos_Riqueza++;
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

        //Eu não posso tirar o rico da função dele
        if (MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().InformaTrabalho() != "Rico")
        {
            if (necessitoTrbalhador == "Lenhador")
            {
                if (MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().InformaTrabalho() != "Lenhador")
                {
                    MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().DefineTrabalho("Lenhador");
                    qtdLenhadores++;
                    qtdCacadores--;
                }
            }

            if (necessitoTrbalhador == "Cacador")
            {
                if (MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().InformaTrabalho() != "Cacador")
                {
                    MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().DefineTrabalho("Cacador");
                    qtdLenhadores--;
                    qtdCacadores++;
                }
            }

            if (necessitoTrbalhador == "Mineiro")
            {
                if (MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().InformaTrabalho() != "Mineiro")
                {
                    MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().DefineTrabalho("Mineiro");
                    qtdLenhadores--;
                    qtdCacadores++;
                }
            }

            if (necessitoTrbalhador == "Rico")
            {
                if (MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().InformaTrabalho() != "Rico")
                {
                    MeusFazendeiros[numTrabalhador].GetComponent<Fazendeiro>().DefineTrabalho("Rico");
                    qtdLenhadores--;
                    qtdCacadores++;
                }
            }
        }

    


    }


    //////
    ///Codigos Jogadores
    ///
    void CodigoGame()
    {

        switch (NomeJogador)
        {
            case "João":

                if(primeiraCriacao == false)
                {
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Lenhador");
                    primeiraCriacao = true;
                }



                if (estoque_Carne > 360)
                {

                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                }
                if (estoque_Madeira > 150)
                {
                    if (casas * 5 == MeusFazendeiros.Count)
                    {


                        CriarCasa();
                        
                    }
                }

                

                break;
            case "Paulo":

                if (primeiraCriacao == false)
                {
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Cacador");
                    //CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Mineiro");
                    primeiraCriacao = true;
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

                

                break;
            case "Maria":

                if (primeiraCriacao == false)
                {
                    
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                    primeiraCriacao = true;
                }
                if (estoque_Carne > 600)
                {

                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Rico");
                }
                if (estoque_Madeira > 200)
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

                break;
            case "Renan":

                if (primeiraCriacao == false)
                {
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    primeiraCriacao = true;
                }
                if (estoque_Carne > 230)
                {

                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Rico");
                }
                if (estoque_Madeira > 140)
                {
                    if (casas * 5 == MeusFazendeiros.Count)
                    {


                        CriarCasa();
                        CriaTrabalhador("Lenhador");
                        CriaTrabalhador("Mineiro");
                    }
                }

                

                break;
            case "Pablo":

                if (primeiraCriacao == false)
                {

                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Rico");
                    primeiraCriacao = true;
                }
                if (estoque_Carne > 320)
                {

                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Lenhador");
                }
                if (estoque_Madeira > 110)
                {
                    if (casas * 5 == MeusFazendeiros.Count)
                    {


                        CriarCasa();
                        
                    }
                }

                

                break;
            case "Kayro":

                if (primeiraCriacao == false)
                {
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Cacador");

                    primeiraCriacao = true;
                }
                if (estoque_Carne > 300)
                {

                    CriaTrabalhador("Cacador");
                }
                if (estoque_Madeira > 170)
                {
                    if (casas * 5 == MeusFazendeiros.Count)
                    {


                        CriarCasa();
                        
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

                break;
            case "EricA":
                if (primeiraCriacao == false)
                {
                    CriaTrabalhador("Mineiro");
                    primeiraCriacao = true;
                }
                if (estoque_Carne > 240)
                {

                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Rico");
                }
                if (estoque_Madeira > 150)
                {
                    if (casas * 5 == MeusFazendeiros.Count)
                    {


                        CriarCasa();
                        CriaTrabalhador("Lenhador");

                    }
                }

                

                break;
            case "ErickY":
                if (primeiraCriacao == false)
                {
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                    //CriaTrabalhador("Cacador");

                    primeiraCriacao = true;
                }
                if (estoque_Carne > 300)
                {

                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Rico");
                }
                if (estoque_Madeira > 300)
                {
                    if (casas * 5 == MeusFazendeiros.Count)
                    {


                        CriarCasa();
                        CriaTrabalhador("Lenhador");
                    }
                }

                

                break;
            case "Leticia":
                if (primeiraCriacao == false)
                {
                    
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Mineiro");
                    CriaTrabalhador("Rico");
                    primeiraCriacao = true;
                }
                if (estoque_Carne > 900)
                {

                    CriaTrabalhador("Rico");
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

                break;
            case "Matheus":

                if (primeiraCriacao == false)
                {
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Lenhador");

                    primeiraCriacao = true;
                }



                if (estoque_Madeira > (MeusFazendeiros.Count * 2f) + 150)
                {
                    CriarCasa();
                }

                if (estoque_Carne > (MeusFazendeiros.Count * 5.5f) + 300)
                {
                    if (qtdCacadores < MeusFazendeiros.Count * 0.3f)
                    {
                        CriaTrabalhador("Cacador");
                    }
                    else if (qtdLenhadores < (MeusFazendeiros.Count / 10) + 1)
                    {
                        CriaTrabalhador("Lenhador");
                    }
                    else
                    {
                        CriaTrabalhador("Rico");
                    }
                }

                

                

                break;
            case "Hugo":

                if (primeiraCriacao == false)
                {
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Mineiro");
                    primeiraCriacao = true;
                }



                if (estoque_Carne > 1000)
                {

                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Rico");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Cacador");
                    CriaTrabalhador("Lenhador");
                    CriaTrabalhador("Mineiro");
                }
                if (estoque_Madeira > 200)
                {
                    if (casas * 5 == MeusFazendeiros.Count)
                    {


                        CriarCasa();
                       
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

                break;

        }


    }



}
