using Godot;
using System.Threading.Tasks;

public partial class GameOverComponent : Label
{
    [Export]
    public string VictoryText { get; set; } = "You Win!";

    [Export]
    public string DefeatText { get; set; } = "You Lose!";

    public override void _Ready()
    {
        Visible = false;
    }

    public void ShowGameOver(string type)
    {
        Text = type == "enemy" ? VictoryText : DefeatText;
        Visible = true;
    }
}
