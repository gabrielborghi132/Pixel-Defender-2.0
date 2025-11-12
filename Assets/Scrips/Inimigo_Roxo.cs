using UnityEngine;

public class Inimigo_Roxo : Inimigo
{
    [Header("Configuração do Inimigo Laranja")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float forcaTiro = 10f;
    private float cooldown;
    private bool canAttack = true;
    private GameObject playerObj;

    // ===========================================================
    //                  INICIALIZAÇÃO
    // ===========================================================
    protected override void Start()
    {
        base.Start(); // inicializa atributos de Inimigo
        Nome = "Inimigo Laranja";
        playerObj = GameObject.FindGameObjectWithTag("Player");

    }

    // ===========================================================
    //                  LOOP PRINCIPAL
    // ===========================================================
    void Update()
    {
        if (!EstaVivo()) return;

        if (canAttack)
        {
            Atirar();
        }

        CoolDown();
    }

    // ===========================================================
    //                  ATAQUE COM PROJÉTIL
    // ===========================================================
    private void Atirar()
    {
        if (playerObj == null) return;

        // instancia o projétil
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // configura o dano
        DanoTiro tiro = proj.GetComponent<DanoTiro>();
        if (tiro != null)
        {
            tiro.DefinirOrigem(false); // false = inimigo
            tiro.DefinirDano(Ataque);

        }

        // aplica força na direção do player
        Vector2 direcao = (playerObj.transform.position - transform.position).normalized;
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direcao * forcaTiro, ForceMode2D.Impulse);
        }

        // controla cooldown
        canAttack = false;
        cooldown = 0f;
    }

    // ===========================================================
    //                  COOLDOWN
    // ===========================================================
    private void CoolDown()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= VelocidadeAtaque)
        {
            canAttack = true;
            cooldown = 0f;
        }
    }
    protected override void FollowPlayer()
    {

    }
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
