using Godot;

public partial class HealthComponent : Node2D
{
    [Export]
    public int MaxHealth { get; set; } = 100;

    [Export]
    public int CurrentHealth { get; set; } = 100;

    public override void _Ready()
    {
        // Başlangıçta sağlık değerlerini ayarla
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        QueueFree();
    }
}
