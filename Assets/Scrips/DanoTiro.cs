using UnityEngine;

public class DanoTiro : MonoBehaviour
{
    [Header("Configuração do Dano")]
    [SerializeField] private float dano = 100f; // dano causado pelo projétil
    [SerializeField] private bool eDoJogador; // define quem atirou


    public float Dano
    {
        get => dano;
        set => dano = value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // se o tiro é do jogador, atinge inimigos
        if (eDoJogador && collision.CompareTag("Inimigo"))
        {
            Inimigo inimigo = collision.GetComponent<Inimigo>();
            if (inimigo != null)
            {
                inimigo.TomarDano(dano);
                Debug.Log($"Tiro do jogador causou {dano} de dano em {inimigo.name}");
            }

            Destroy(gameObject);
        }
        // se o tiro é do inimigo, atinge o jogador
        else if (!eDoJogador && collision.CompareTag("Player"))
        {
            Arvore arvore = collision.GetComponent<Arvore>();
            if (arvore != null)
            {
                arvore.TomarDano(dano);
                Debug.Log($"Tiro do inimigo causou {dano} de dano em {arvore.name}");
            }

            Destroy(gameObject);
        }

        // destrói o projétil ao colidir com paredes
        if (collision.CompareTag("Parede"))
        {
            Destroy(gameObject);
        }
    }
    // define quem é o dono do tiro
    public void DefinirOrigem(bool jogadorAtirou)
    {
        eDoJogador = jogadorAtirou;
    }
    // método opcional para definir o dano dinamicamente
    public void DefinirDano(float novoDano)
    {
        dano = novoDano;
    }
}
