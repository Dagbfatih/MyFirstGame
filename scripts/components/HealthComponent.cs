using Godot;

public partial class HealthComponent : Node2D
{
    [Export]
    public int MaxHealth { get; set; } = 100;

    [Export]
    public int CurrentHealth { get; set; } = 100;

    [Export]
    public HealthbarComponent HealthbarComponent { get; set; }

    [Signal]
    public delegate void CharacterDiedEventHandler();

    public override void _Ready()
    {
        // Başlangıçta sağlık değerlerini ayarla
        CurrentHealth = MaxHealth;
        UpdateHealthbar();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }

        UpdateHealthbar();
    }

    private void Die()
    {
        EmitSignal(SignalName.CharacterDied);
        QueueFree();
    }

    private void UpdateHealthbar()
    {
        HealthbarComponent?.UpdateHealth(CurrentHealth);
    }
}
