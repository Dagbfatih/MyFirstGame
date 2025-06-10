using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; } // Global erişim için Singleton deseni

    public List<Node> Tanks { get; set; }

    [Signal]
    public delegate void GameOverEventHandler(string type); // Sinyal tanımlama

    private readonly PackedScene _gameOverComponentScene = GD.Load<PackedScene>("res://scenes/components/GameOverComponent.tscn");


    public override void _Ready()
    {
        if (Instance != null)
        {
            GD.PrintErr("Multiple GameManager instances detected! This should be a singleton.");
            return;
        }
        Instance = this;

        Tanks = [.. GetTree().GetNodesInGroup("tanks")];

        ConnectOnTankDestroyedEvent(new Callable(this, nameof(OnTankDestroyed)));
    }

    private void ConnectOnTankDestroyedEvent(Callable callable)
    {
        foreach (var tank in Tanks)
        {
            if (tank == null) continue;

            tank.Connect(BaseTank.SignalName.TankDestroyed, callable);
        }
    }

    protected virtual async void OnTankDestroyed(BaseTank baseTank)
    {
        await GameOverLabel(baseTank.Type);

        foreach (var tank in Tanks)
        {
            if (IsInstanceValid(tank) && tank.IsInsideTree())
            {
                tank.QueueFree();
            }
        }
        Tanks.Clear();

        GetTree().ChangeSceneToFile("res://scenes/scene2.tscn"); // Not written yet, to be continued...
    }

    private async Task GameOverLabel(string destroyedTankType)
    {
        var gameOverComponent = _gameOverComponentScene.Instantiate<GameOverComponent>();
        // Add GameOverComponent under the UI (CanvasLayer) node
        var uiNode = GetTree().CurrentScene.GetNode("UI");

        if (uiNode == null)
        {
            GD.PrintErr("UI node not found in the current scene. Cannot add GameOverComponent.");
            return;
        }

        uiNode.AddChild(gameOverComponent);
        gameOverComponent.ShowGameOver(destroyedTankType);
        GD.Print("GameOverComponent added to UI.");
        
        await ToSignal(GetTree().CreateTimer(3.0f), "timeout");

        gameOverComponent.QueueFree();

    }
}