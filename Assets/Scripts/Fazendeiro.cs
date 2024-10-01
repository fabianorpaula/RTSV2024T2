using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fazendeiro : MonoBehaviour
{

    public int bolsa_carne = 0;
    public GameObject Destino_Carne;
    public GameObject Destino_Armazem;
    private NavMeshAgent Agente;
    private float temporizador;
    private Armazem MeuArmazem;

    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();
        MeuArmazem = Destino_Armazem.GetComponent<Armazem>();
    }

    // Update is called once per frame
    void Update()
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
}
