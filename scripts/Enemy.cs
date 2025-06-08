using Godot;
using System;

public partial class Enemy : BaseTank
{
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        SetKeybinds(
            Keybinds.Left,
            Keybinds.Right,
            Keybinds.Up,
            Keybinds.Down,
            Keybinds.LeftClick);
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
