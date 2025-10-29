using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [Header("Referência à barra de HP no Canvas")]
    [SerializeField] private Slider hp_bar;
    [SerializeField] private Text texto;
    private Arvore player;

    private void Start()
    {
        // Verifica se a barra foi atribuída
        if (hp_bar == null)
        {
            Debug.LogError("HUD: Nenhum Slider foi atribuído ao campo hp_bar!");
        }

        // Encontra o jogador pela tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Arvore>();
        }
        else
        {
            Debug.LogError("HUD: Nenhum objeto com a tag 'Player' foi encontrado!");
        }

        // Define o valor máximo inicial da barra de HP
        if (player != null)
        {
            hp_bar.maxValue = (float)player.VidaMax;
            hp_bar.value = (float)player.Vida;
        }
        if(player != null)
        {
            texto.text = player.getNivel().ToString();
        }
    }

    private void Update()
    {
        AtualizarHp();
        atualizarnivel();
    }

    private void AtualizarHp()
    {
        if (player != null && hp_bar != null)
        {
            hp_bar.maxValue = (float)player.VidaMax;
            hp_bar.value = (float)player.Vida;
        }
    }
    private void atualizarnivel()
    {
        if(player != null)
        {
            texto.text = player.getNivel().ToString();
        }
    }
}
