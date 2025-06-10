using Godot;

public partial class HitboxComponent : Area2D
{
    [Export]
    public HealthComponent HealthComponent { get; set; }

    public void TakeDamage(int damage)
    {
        if (HealthComponent != null)
        {
            HealthComponent.TakeDamage(damage);
        }
        else
        {
            GD.PrintErr("HealthComponent is not assigned!");
        }
    }
}
