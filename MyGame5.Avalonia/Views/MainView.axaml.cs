using Avalonia.Controls;
using Avalonia.Platform;
using Stride.Engine;
using Stride.Games;
using System.Threading.Tasks;

namespace MyGame5.Avalonia.Views;

public partial class MainView : NativeControlHost
{

	private Stride.Graphics.SDL.Window _sdlWindow;
	private Game _game;

    public MainView()
    {
        InitializeComponent();
    }

	protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
	{

		_sdlWindow = new Stride.Graphics.SDL.Window("MyGame5", parent.Handle);
		var context = new GameContextSDL(_sdlWindow, _sdlWindow.Size.Width, _sdlWindow.Size.Height);

		_game = new Game();

		Task.Factory.StartNew(() =>
		{
			// Must move this off current thread or the form will hang.
			_game.Run(context);
		}, TaskCreationOptions.LongRunning);

		return base.CreateNativeControlCore(parent);
	}
}
