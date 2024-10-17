using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Visor : MonoBehaviour
{
    public TMP_Text Nome;
    public TMP_Text Carne;
    public TMP_Text Madeira;
    public TMP_Text Ouro;
    public TMP_Text QTDFazenderios;

    public Armazem MeuArmazem;
    public TMP_Text Ricos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Nome.text = MeuArmazem.NomeJogador;
        Carne.text = "Carne: "+MeuArmazem.estoque_Carne.ToString();
        Madeira.text = "Madeira: " + MeuArmazem.estoque_Madeira;ToString();
        int CasaM = MeuArmazem.casas * 5;
        QTDFazenderios.text = "Fazenderios: " + MeuArmazem.MeusFazendeiros.Count.ToString() + " / " + CasaM.ToString();
        Ricos.text = "Ricos: "+MeuArmazem.pontos_Riqueza.ToString();
       
    }
}
