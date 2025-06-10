using System.Linq;
using Godot;

public partial class HealthbarComponent : ProgressBar
{
	/// <param name="health">
	/// This value should be between 0 and 100.
	/// If the value is greater than 100, it will be set to 100.
	/// If the value is less than 0, it will be set to 0.
	/// </param>
	public void UpdateHealth(int health)
	{
		Value = health switch
		{
			> 100 => 100,
			< 0 => 0,
			_ => health
		};
	}
}
