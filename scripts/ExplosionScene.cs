using Godot;
using System;

public partial class ExplosionScene : Node2D
{
    private Node2D _explosionSprite; // İçindeki Sprite2D veya AnimatedSprite2D
    
    public override void _Ready()
    {
        // İçindeki görsel düğümü al
        _explosionSprite = GetNode<Node2D>("Explosion"); // "Explosion" adını verdiğinizden emin olun

        // Patlamayı görünür yap
        _explosionSprite.Visible = true;

        // Animasyonu oynat ve bitmesini bekle, sonra kendini sil
        PlayAnimationAndQueueFree();
    }

    private async void PlayAnimationAndQueueFree()
    {
        // Eğer iç düğüm AnimatedSprite2D ise, animasyonu oynat
        if (_explosionSprite is AnimatedSprite2D animatedExplosion)
        {
            animatedExplosion.Play("default"); // Animasyon adını "default" olarak varsaydım
            await ToSignal(animatedExplosion, AnimatedSprite2D.SignalName.AnimationFinished);
        }
        else
        {
            // Eğer AnimatedSprite2D değilse (örneğin sadece Sprite2D), belirli bir süre bekle
            await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
        }

        QueueFree();
    }
}
