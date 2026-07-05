#if TOOLS
using Godot;
using System;

[Tool]
public partial class tilesetutilityplugin : EditorPlugin
{
	private EditorDock _dock;
	public override void _EnterTree()
	{
		_dock = GD.Load<PackedScene>("res://addons/tilesetutilityplugin/tile_set_utility_dock.tscn").Instantiate<EditorDock>();
		AddDock(_dock);
	}

	public override void _ExitTree()
	{
		// Remove the dock.
		RemoveDock(_dock);
		// Erase the control from the memory.
		_dock.QueueFree();
	}
}
#endif
