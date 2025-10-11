using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [Header("Refer�ncia � barra de HP no Canvas")]
    [SerializeField] private Slider hp_bar;

    private Personagem player;

    private void Start()
    {
        // Verifica se a barra foi atribu�da
        if (hp_bar == null)
        {
            Debug.LogError("HUD: Nenhum Slider foi atribu�do ao campo hp_bar!");
        }

        // Encontra o jogador pela tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Personagem>();
        }
        else
        {
            Debug.LogError("HUD: Nenhum objeto com a tag 'Player' foi encontrado!");
        }

        // Define o valor m�ximo inicial da barra de HP
        if (player != null)
        {
            hp_bar.maxValue = (float)player.VidaMax;
            hp_bar.value = (float)player.Vida;
        }
    }

    private void Update()
    {
        AtualizarHp();
    }

    private void AtualizarHp()
    {
        if (player != null && hp_bar != null)
        {
            hp_bar.maxValue = (float)player.VidaMax;
            hp_bar.value = (float)player.Vida;
        }
    }
}
