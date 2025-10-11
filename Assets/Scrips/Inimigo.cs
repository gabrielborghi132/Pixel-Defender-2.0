using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Inimigo : Personagem
{
    protected MainControl mainControl;
    [SerializeField] protected float velocidade = 2f;
    protected GameObject playerObject;
    // ===========================================================
    //                  INICIALIZAÇÃO
    // ===========================================================

    protected virtual void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        Nome = "Inimigo";
    }

    // ===========================================================
    //                  MOVIMENTO
    // ===========================================================

    protected virtual void FixedUpdate()
    {
        if (EstaVivo())
            FollowPlayer();
    }

    protected virtual void FollowPlayer()
    {
        if (playerObject == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            playerObject.transform.position,
            Base_speed * Time.deltaTime
        );
    }

    // ===========================================================
    //                  COMBATE E DANO
    // ===========================================================

    public virtual void TomarDano(float dano)
    {
        ReceberDano(dano);

        if (!EstaVivo())
            Morrer();
    }

    // ===========================================================
    //                  MORTE
    // ===========================================================

    public virtual void Morrer()
    {
        Debug.Log($"{Nome} foi derrotado!");
        // Evento de morte, som, partículas, pontuação etc.
        Destroy(gameObject);
    }

    // ===========================================================
    //                  EXTENSÕES
    // ===========================================================

    public void BaseSpeedUp(float novaVelocidade)
    {
        Base_speed = novaVelocidade;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }
}
