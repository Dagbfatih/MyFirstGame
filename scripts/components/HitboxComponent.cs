using Godot;
using System;

public partial class HitboxComponent : Area2D
{
    [Export]
    public HealthComponent HealthComponent { get; set; }

    public void Damage(int damage)
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
