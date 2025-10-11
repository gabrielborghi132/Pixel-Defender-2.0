using UnityEngine;

public abstract class Personagem : MonoBehaviour
{
    [SerializeField] private string nome;
    [SerializeField] private int id_Personagem;
    [SerializeField] private float vida;
    [SerializeField] private float vidaMax;
    [SerializeField] private float ataque;
    [SerializeField] private float velocidadeAtaque;
    [SerializeField] private float base_speed;

    // Getters e Setters
    public float Base_speed
    {
        get => base_speed;
        set => base_speed = value;
    }
    public string Nome
    {
        get => nome;
        set => nome = value;
    }

    public int Id_Personagem
    {
        get => id_Personagem;
        set => id_Personagem = value;
    }

    public float Vida
    {
        get => vida;
        set => vida = value;
    }

    public float VidaMax
    {
        get => vidaMax;
        set => vidaMax = value;
    }

    public float Ataque
    {
        get => ataque;
        set => ataque = value;
    }

    public float VelocidadeAtaque
    {
        get => velocidadeAtaque;
        set => velocidadeAtaque = value;
    }

    public void ReceberDano(double dano)
    {
        vida = Mathf.Max(0, (float)(vida-dano));
    }

    public void Atacar(Personagem alvo)
    {
        alvo.ReceberDano(ataque);
    }
    public bool EstaVivo()
    {
        return vida > 0;
    }
}
