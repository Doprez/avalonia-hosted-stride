using Avalonia.Controls;
using Stride.Input;

namespace MyGame5.Avalonia.InputEvents;
public class InputSourceAvalonia : InputSourceBase
{

	private MouseAvalonia _mouse;

	private Control _control;
	private InputManager _inputManager;

	public InputSourceAvalonia(Control control)
	{
		_control = control;
	}

	public override void Initialize(InputManager inputManager)
	{
		_inputManager = inputManager;
		_mouse = new MouseAvalonia(this, _control);

		RegisterDevice(_mouse);
	}
}
