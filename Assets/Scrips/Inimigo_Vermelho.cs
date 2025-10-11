using UnityEngine;

public class Inimigo_Vermelho : Inimigo
{
    [SerializeField] private float move_speed;
    // Inicialização
    protected override void Start()
    {
        // Chama o Start da classe base (Inimigo)
        base.Start();

        // Define os atributos específicos do inimigo vermelho
        Nome = "Inimigo Vermelho";
        Debug.Log($"{Nome} criado com {VidaMax} de vida e velocidade {Base_speed}.");
    }

    // Colisão com o jogador
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerStats = collision.gameObject.GetComponent<Personagem>();

            if (playerStats != null)
            {
                Atacar(playerStats);
                Debug.Log($"{Nome} atacou o jogador causando {Ataque} de dano!");
            }

            Morrer();
        }
    }


}
