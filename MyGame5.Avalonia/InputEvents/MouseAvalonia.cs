using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Stride.Core.Mathematics;
using Stride.Input;
using System;
using AMouseButton = Avalonia.Input.MouseButton;
using SMouseButton = Stride.Input.MouseButton;

namespace MyGame5.Avalonia.InputEvents;
public class MouseAvalonia : MouseDeviceBase
{
	public override bool IsPositionLocked { get; }
	public override string Name { get; } = "Avalonia Mouse";
	public override Guid Id { get; }
	public override IInputSource Source { get; }

	public MouseAvalonia(InputSourceAvalonia source, Control uiControl)
	{
		Source = source;

		uiControl.PointerMoved += UIControl_PointerMoved;
		uiControl.PointerPressed += UIControl_PointerPressed;
		uiControl.PointerReleased += GetUIControl_PointerReleased;
		uiControl.PointerWheelChanged += GetUIControl_PointerWheelChanged;
		uiControl.SizeChanged += GetUIControl_SizeChanged;
	}

	private void GetUIControl_SizeChanged(object? sender, SizeChangedEventArgs e)
	{
		SetSurfaceSize(new Vector2((float)e.NewSize.Width, (float)e.NewSize.Height));
	}

	private void GetUIControl_PointerWheelChanged(object? sender, PointerWheelEventArgs e)
	{
		MouseState.HandleMouseWheel((float)e.Delta.Y);
	}

	private void GetUIControl_PointerReleased(object? sender, PointerReleasedEventArgs e)
	{
		var point = e.GetCurrentPoint(sender as Control);

		switch(point.Properties.PointerUpdateKind)
		{
			// MouseButton Up events
			case PointerUpdateKind.LeftButtonReleased:
				MouseState.HandleButtonUp(SMouseButton.Left);
				break;
			case PointerUpdateKind.RightButtonReleased:
				MouseState.HandleButtonUp(SMouseButton.Right);
				break;
			case PointerUpdateKind.MiddleButtonReleased:
				MouseState.HandleButtonUp(SMouseButton.Middle);
				break;
			case PointerUpdateKind.XButton1Released:
				MouseState.HandleButtonUp(SMouseButton.Extended1);
				break;
			case PointerUpdateKind.XButton2Released:
				MouseState.HandleButtonUp(SMouseButton.Extended2);
				break;
		}
	}

	private void UIControl_PointerPressed(object? sender, PointerPressedEventArgs e)
	{
		var point = e.GetCurrentPoint(sender as Control);
		
		switch(point.Properties.PointerUpdateKind)
		{
			// MouseButton Down events
			case PointerUpdateKind.LeftButtonPressed:
				MouseState.HandleButtonDown(SMouseButton.Left);
				break;
			case PointerUpdateKind.RightButtonPressed:
				MouseState.HandleButtonDown(SMouseButton.Right);
				break;
			case PointerUpdateKind.MiddleButtonPressed:
				MouseState.HandleButtonDown(SMouseButton.Middle);
				break;
			case PointerUpdateKind.XButton1Pressed:
				MouseState.HandleButtonDown(SMouseButton.Extended1);
				break;
			case PointerUpdateKind.XButton2Pressed:
				MouseState.HandleButtonDown(SMouseButton.Extended2);
				break;
		}
	}

	private void UIControl_PointerMoved(object? sender, PointerEventArgs e)
	{
		var pointer = e.GetCurrentPoint(sender as Control);

		if (IsPositionLocked)
		{
			MouseState.HandleMouseDelta(new Vector2((int)pointer.Position.X, (int)pointer.Position.Y));
		}
		else
		{
			MouseState.HandleMove(new Vector2((int)pointer.Position.X, (int)pointer.Position.Y));
		}
	}

	public override void LockPosition(bool forceCenter = false)
	{

	}

	public override void SetPosition(Vector2 normalizedPosition)
	{

	}

	public override void UnlockPosition()
	{

	}

	private static SMouseButton ConvertMouseButton(uint mouseButton)
	{
		switch ((AMouseButton)mouseButton)
		{
			case AMouseButton.Left:
				return SMouseButton.Left;
			case AMouseButton.Right:
				return SMouseButton.Right;
			case AMouseButton.Middle:
				return SMouseButton.Middle;
		}

		return (SMouseButton)(-1);
	}
}
