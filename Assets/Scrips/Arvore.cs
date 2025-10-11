using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Arvore : Personagem
{
    [Header("Configurações da Árvore")]
    [SerializeField] private int nivel = 1;
    [SerializeField] private float multiplicadorEvolucao = 1.30f;
    [SerializeField] private float curaPorEvolucao = 0.10f;
    [SerializeField] private float move_speed;
    [SerializeField] private GameObject tiro;
    [SerializeField] private float forcaTiro = 10f;
    private Rigidbody2D rb;
    private Vector2 movimento;
    private float cooldownTiro = 0.3f;
    private float proximoTiro = 0f;
    private void Awake()
    {
        Nome = "Árvore";
    }
    private void Start()
    {
        multiplicadorEvolucao = 1.20f;
        curaPorEvolucao = 0.20f;
        Ataque = 10;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        Debug.Log("Árvore inicializada com o novo sistema de Input.");
    }

    private void Update()
    {
        wasd();
        Atirar();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimento * move_speed * Time.fixedDeltaTime);
    }

    private void wasd()
    {
        if (Keyboard.current == null)
        {
            Debug.LogWarning("Nenhum teclado detectado!");
            return;
        }

        float horizontal = Keyboard.current.aKey.isPressed ? -1 :
                           Keyboard.current.dKey.isPressed ? 1 : 0;

        float vertical = Keyboard.current.sKey.isPressed ? -1 :
                         Keyboard.current.wKey.isPressed ? 1 : 0;

        movimento = new Vector2(horizontal, vertical).normalized;
    }

    private void Atirar()
    {
        if (Mouse.current == null || Mouse.current.leftButton.isPressed == false) return;
        if (Time.time < proximoTiro) return;

        proximoTiro = Time.time + cooldownTiro;

        GameObject tiroInstancia = Instantiate(tiro, transform.position, Quaternion.identity);

        // Configura o dano e origem
        DanoTiro danoTiro = tiroInstancia.GetComponent<DanoTiro>();
        if (danoTiro != null)
        {
            danoTiro.DefinirOrigem(true);  // tiro do jogador
            danoTiro.DefinirDano(Ataque);  // dano da árvore
        }

        // Direção e força
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direcaoTiro = (mousePos - (Vector2)transform.position).normalized;

        Rigidbody2D rbTiro = tiroInstancia.GetComponent<Rigidbody2D>();
        rbTiro.AddForce(direcaoTiro * forcaTiro, ForceMode2D.Impulse);

        Destroy(tiroInstancia, 3f);
    }


    public void Atacar(Inimigo alvo)
    {
        if (alvo == null || !alvo.EstaVivo()) return;
        alvo.ReceberDano(Ataque);
        Debug.Log($"{Nome} atacou {alvo.name}, causando {Ataque} de dano!");
    }

    public void TomarDano(float dano)
    {
        ReceberDano(dano);
        Debug.Log($"{Nome} recebeu {dano} de dano. Vida atual: {Vida}/{VidaMax}");

        if (!EstaVivo())
            Morrer();
    }

    public void Evoluir()
    {
        nivel++;
        VidaMax *= multiplicadorEvolucao;
        Ataque *= multiplicadorEvolucao;
        VelocidadeAtaque /= multiplicadorEvolucao;
        Vida += VidaMax * curaPorEvolucao;

        if (Vida > VidaMax)
            Vida = VidaMax;

        Debug.Log($"{Nome} evoluiu para o nível {nivel}!");
    }


    private void Morrer()
    {
        Debug.Log($"{Nome} foi derrotada...");
        gameObject.SetActive(false);
    }
}
