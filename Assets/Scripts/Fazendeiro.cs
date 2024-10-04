using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fazendeiro : MonoBehaviour
{

    public int bolsa_carne = 0;
    public int bolsa_madeira = 0;


    public GameObject Destino_Carne;
    public GameObject Destino_Madeira;
    public GameObject Destino_Armazem;
    private NavMeshAgent Agente;
    private float temporizador;
    private Armazem MeuArmazem;

    public enum MeuEstados{Cacador, Lenhador }
    public MeuEstados EstadoAtual;
    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();
        MeuArmazem = Destino_Armazem.GetComponent<Armazem>();

      
        
    }


    public void DefineTrabalho(string nomeTrabalho)
    {
        
        if (nomeTrabalho == "Lenhador")
        {
            EstadoAtual = MeuEstados.Lenhador;
        }
        if(nomeTrabalho == "Cacador")
        {
            EstadoAtual = MeuEstados.Cacador;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(EstadoAtual == MeuEstados.Lenhador)
        {
            Lenhador();
        }
        if(EstadoAtual == MeuEstados.Cacador)
        {
            Cacador();
        }
    }

    void Cacador()
    {
        if (bolsa_carne < 10)
        {
            Agente.SetDestination(Destino_Carne.transform.position);
            float distancia = Vector3.Distance(transform.position,
                Destino_Carne.transform.position);
            if (distancia < 3)
            {
                Agente.speed = 0;
                temporizador += Time.deltaTime;
                if (temporizador > 0.5f)
                {
                    bolsa_carne++;
                    temporizador = 0;
                }

            }
            else{
                Agente.speed = 5;
            }
        }
        else
        {
            Agente.speed = 5;
            Agente.SetDestination(Destino_Armazem.transform.position);
            float distancia = Vector3.Distance(transform.position,
                Destino_Armazem.transform.position);
            if (distancia < 3)
            {
                MeuArmazem.ReceberCarne(bolsa_carne);
                bolsa_carne = 0;

            }
        }
    }

    void Lenhador()
    {
        if (bolsa_madeira < 10)
        {
            Agente.SetDestination(Destino_Madeira.transform.position);
            float distancia = Vector3.Distance(transform.position,
                Destino_Madeira.transform.position);
            if (distancia < 4)
            {
                Agente.speed = 1;
                temporizador += Time.deltaTime;
                if (temporizador > 0.5f)
                {
                    bolsa_madeira++;
                    temporizador = 0;
                }

            }
            else
            {
                Agente.speed = 5;
            }
        }
        else
        {
            Agente.speed = 5;
            Agente.SetDestination(Destino_Armazem.transform.position);
            float distancia = Vector3.Distance(transform.position,
                Destino_Armazem.transform.position);
            if (distancia < 3)
            {
                MeuArmazem.ReceberMadeira(bolsa_madeira);
                bolsa_madeira= 0;

            }
        }
    }


    public void InformaCarne(GameObject DCarne)
    {
        Destino_Carne = DCarne;
    }
    public void InformaMadeira(GameObject DMadeira)
    {
        Destino_Madeira = DMadeira;
    }
    public void InformaArmazem(GameObject DArmazem)
    {
        Destino_Armazem = DArmazem;
    }

}
