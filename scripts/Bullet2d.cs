using System;
using Godot;

public partial class Bullet2d : Area2D
{
	[Export] // Godot editöründen değeri ayarlamamızı sağlar
	public float Speed = 10000.0f; // Mermi hızı (piksel/saniye)
	public int Damage = 10;
	private Vector2 _direction = Vector2.Zero;

	public override void _Ready()
	{
		// Merminin ilk oluşturulduğunda otomatik olarak yok olmasını sağlayabiliriz
		// Örneğin 2 saniye sonra yok olsun.
		// Bu, sahnede çok fazla mermi birikmesini engeller.
		GetTree().CreateTimer(2.0f).Connect("timeout", Callable.From(QueueFree));
		BodyEntered += OnBodyEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += _direction * Speed * (float)delta;
	}


	public void Fire(Vector2 direction)
	{
		_direction = direction.Normalized();
		Rotation = _direction.Angle() - Mathf.Pi / 2f;
	}

	public void OnBodyEntered(Node body)
	{
		GD.Print($"Mermi çarptı: {body.Name}");
		if (body is BaseTank tank)
		{
			tank.TakeDamage(Damage);
		}

		GD.Print($"Mermi çarptı: {body.Name}");
		QueueFree();
	}
}
