using Godot;

public abstract partial class BaseTank : CharacterBody2D
{
    [Export]
    public PackedScene BulletScene { get; set; } // Mermi sahnesini Godot editöründen sürükleyip bırakacağız.

    [Export]
    public PackedScene ExplosionEffectScene; // Patlama efekti sahnesi

    [Export]
    public float FireRate = 0.5f; // Saniyede kaç kez ateş edebileceği (0.5f = 2 atış/saniye)

    [Export]
    public int Speed { get; set; } = 5000;

    [Export]
    public float RotationSpeed { get; set; } = 3f;

    protected float _rotationDirection;
    protected float _canFireTimer = 0.0f; // Ateş edene kadar beklenen süre
    protected Marker2D _muzzle; // Namlu ucunu temsil eden Marker2D Node'u
    protected Node2D _explosionEffect; // Patlama efekti için Node2D

    private string _leftKeybind = Keybinds.Left;
    private string _rightKeybind = Keybinds.Right;
    private string _upKeybind = Keybinds.Up;
    private string _downKeybind = Keybinds.Down;
    private string _fireKeybind = Keybinds.LeftClick;

    public override void _Ready()
    {
        // Namlu ucundaki Marker2D Node'unu bul
        _muzzle = GetNode<Marker2D>("Muzzle"); // Marker2D'nin adını Muzzle olarak varsayıyorum
        if (_muzzle == null)
        {
            GD.PrintErr("Muzzle Marker2D bulunamadı! Lütfen PlayerTank altına bir Marker2D ekleyin ve adını Muzzle yapın.");
        }
    }

    public override void _Process(double delta)
    {
        // Ateş etme bekleme süresini azalt
        _canFireTimer -= (float)delta;
        if (Input.IsActionPressed(_fireKeybind) && _canFireTimer <= 0)
        {
            Fire();
            _canFireTimer = FireRate; // Bir sonraki atış için bekleme süresini ayarla
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        Rotation += _rotationDirection * RotationSpeed * (float)delta;
        MoveAndSlide();
    }

    public void TakeDamage(int damageAmount)
    {
        var healthComponent = GetNode<HealthComponent>("HealthComponent");
        healthComponent.TakeDamage(damageAmount);
    }

    protected void SetKeybinds(string left, string right, string up, string down, string fire)
    {
        _leftKeybind = left;
        _rightKeybind = right;
        _upKeybind = up;
        _downKeybind = down;
        _fireKeybind = fire;
    }

    protected virtual void Fire()
    {
        if (BulletScene == null || _muzzle == null)
        {
            GD.PrintErr("Mermi sahnesi veya namlu noktası tanımlı değil!");
            return;
        }

        // Spawn bullet scene
        Node2D bulletSceneRoot = (Node2D)BulletScene.Instantiate();
        GetParent().AddChild(bulletSceneRoot); // Bu, tankın parent'ına (World Node'una) ekler.

        // Spawn explosion effect scene
        _explosionEffect = (Node2D)ExplosionEffectScene.Instantiate();
        _muzzle.AddChild(_explosionEffect);

        Bullet2d bulletInstance = bulletSceneRoot.GetNode<Bullet2d>("Bullet");

        bulletInstance.GlobalPosition = _muzzle.GlobalPosition;
        bulletInstance.GlobalRotation = _muzzle.GlobalRotation;

        bulletInstance.Fire(Vector2.Right.Rotated(GlobalRotation));
    }

    private void GetInput()
    {
        _rotationDirection = Input.GetAxis(_leftKeybind, _rightKeybind);
        Velocity = Transform.X * Input.GetAxis(_downKeybind, _upKeybind) * Speed;
    }
}
