using Godot;

public partial class Hero2D : BaseTank
{
    public override void _Ready()
    {
        base._Ready();
        SetKeybinds(
            Keybinds.ArrowLeft,
            Keybinds.ArrowRight,
            Keybinds.ArrowUp,
            Keybinds.ArrowDown,
            Keybinds.Space);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    protected new void Fire()
    {
        base.Fire();
    }
}
