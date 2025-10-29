using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerarInimigos : MonoBehaviour
{
    [Header("Configurações de Spawn")]
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float tempoCooldown = 10f;
    [SerializeField] private int fase = 1;

    [Header("Atributos Base dos Inimigos")]
    [SerializeField] private float vidaMax = 30f;
    [SerializeField] private float ataque = 10f;
    [SerializeField] private float velocidadeAtaque = 5.0f;
    [SerializeField] private float baseSpeed = 2f;

    [Header("Atributos personagem")]
    [SerializeField] private float vidaMaxPersonagem = 100f;
    [SerializeField] private float ataquePersonagem = 5f;
    [SerializeField] private float velocidadeAtaquePersonagem = 1.0f;
    [SerializeField] private float baseSpeedPersonagem = 1f;
    private Arvore player;
    private float cooldownAtual = 0f;


    // ============================================================
    //                    CICLO DE VIDA
    // ============================================================
    void Start()
    {
        vidaMax = 30f;
        ataque = 5f;
        velocidadeAtaque = 5.0f;
        baseSpeed = 2f;
        cooldownAtual = 0f;

        player = FindAnyObjectByType<Arvore>();
        player.VidaMax = vidaMaxPersonagem;
        player.Vida = player.VidaMax;
        player.Ataque = ataquePersonagem;
        player.VelocidadeAtaque = velocidadeAtaquePersonagem;
        player.Base_speed = baseSpeedPersonagem;
        SpawEnemies(); // Gera primeira leva logo no início
    }

    void Update()
    {
        if (!player.EstaVivo())
        {
            ReiniciarJogo();
            RecarregarCena();
        }
        cooldownAtual += Time.deltaTime;

        if (cooldownAtual >= tempoCooldown|| TodosInimigosMortos())
        {
            player.Evoluir();
            SpawEnemies();
            cooldownAtual = 0f;
            fase++;
        }
    }

    // ============================================================
    //                    GERAÇÃO DE INIMIGOS
    // ============================================================
    public void SpawEnemies()
    {
        int nivel = fase;
        
        if (spawnPoints.Count == 0 || enemies.Count == 0)
        {
            Debug.LogWarning("Nenhum ponto de spawn ou inimigo configurado!");
            return;
        }
        foreach (GameObject sp in spawnPoints)
        {
            int random_enemy = Random.Range(0, enemies.Count);
            GameObject novoInimigo = Instantiate(enemies[random_enemy], sp.transform.position, Quaternion.identity);

            Inimigo inimigo = novoInimigo.GetComponent<Inimigo>();
            inimigo.VidaMax = vidaMax;
            inimigo.Vida = inimigo.VidaMax;
            inimigo.Ataque = ataque;
            inimigo.VelocidadeAtaque = velocidadeAtaque;
            inimigo.Base_speed = baseSpeed;
        }

        vidaMax *= 1.3f;
        ataque *= 1.3f;
        velocidadeAtaque /= 1.05f;  // Disparam mais rápido
        baseSpeed *= 1.03f;         // Movem-se um pouco mais rápido


    }
    private bool TodosInimigosMortos()
    {
        return GameObject.FindGameObjectsWithTag("Inimigo").Length <= 0;
    }
    public void ReiniciarJogo()
    {
        // Destroi todos os inimigos existentes
        foreach (GameObject inimigo in GameObject.FindGameObjectsWithTag("Inimigo"))
        {
            Destroy(inimigo);
        }

        // Reseta variáveis
        fase = 1;
        vidaMax = 30f;
        ataque = 5f;
        velocidadeAtaque = 5.0f;
        baseSpeed = 2f;
        cooldownAtual = 0f;

        vidaMaxPersonagem = 100f;
        ataquePersonagem = 10f;
        velocidadeAtaquePersonagem = 1.0f;
        baseSpeedPersonagem = 1f;

        // Recomeça o spawn inicial
        //SpawEnemies();

        Debug.Log("Jogo reiniciado: atributos e fase resetados.");
    }

    // ===========================================================
    //                      REINÍCIO COMPLETO (CENA)
    // ===========================================================
    public void RecarregarCena()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
