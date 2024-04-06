using Avalonia.Controls;
using Stride.Engine;

namespace MyGame5.Avalonia;
public class AvaloniaGame : Game
{

	private Control _control;

	public AvaloniaGame(Control control)
	{
		_control = control;
	}

	protected override void BeginRun()
	{
		var inputManager = Services.GetService<Stride.Input.InputManager>();

		//Input.Sources.Clear();

		inputManager.Sources.Add(new InputEvents.InputSourceAvalonia(_control));
	}
}
