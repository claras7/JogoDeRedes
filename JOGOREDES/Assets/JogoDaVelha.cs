using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JogoDaVelha : MonoBehaviour
{
    public Button[] casas;                  // Botões do tabuleiro
    public TMP_Text[] textosDasCasas;       // Textos (TextMeshPro) de cada casa
    public TMP_Text textoStatus;           // Texto que mostra a vez ou o resultado
    public Button botaoReiniciar;          // Botão para reiniciar o jogo

    private string[] estado = new string[9]; // Estado atual do tabuleiro
    private string jogadorAtual = "X";
    private bool jogoAtivo = true;

    void Start()
    {
        ReiniciarJogo();
    }

    public void ClicarNaCasa(int index)
    {
        if (!jogoAtivo || estado[index] != "") return;

        estado[index] = jogadorAtual;
        textosDasCasas[index].text = jogadorAtual;
        casas[index].interactable = false;

        if (VerificarVitoria())
        {
            textoStatus.text = $"Jogador {jogadorAtual} venceu!";
            jogoAtivo = false;
            botaoReiniciar.gameObject.SetActive(true);
        }
        else if (VerificarEmpate())
        {
            textoStatus.text = "Empate!";
            jogoAtivo = false;
            botaoReiniciar.gameObject.SetActive(true);
        }
        else
        {
            jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
            textoStatus.text = $"Vez do Jogador {jogadorAtual}";
        }
    }

    bool VerificarVitoria()
    {
        int[,] combinacoes = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < combinacoes.GetLength(0); i++)
        {
            string a = estado[combinacoes[i, 0]];
            string b = estado[combinacoes[i, 1]];
            string c = estado[combinacoes[i, 2]];

            if (a == jogadorAtual && b == jogadorAtual && c == jogadorAtual)
                return true;
        }
        return false;
    }

    bool VerificarEmpate()
    {
        foreach (string s in estado)
            if (s == "") return false;
        return true;
    }

    public void ReiniciarJogo()
    {
        for (int i = 0; i < casas.Length; i++)
        {
            estado[i] = "";
            textosDasCasas[i].text = "";
            casas[i].interactable = true;
        }

        jogadorAtual = "X";
        jogoAtivo = true;
        textoStatus.text = "Vez do Jogador X";
        botaoReiniciar.gameObject.SetActive(false);
    }
}
