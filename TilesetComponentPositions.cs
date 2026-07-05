using Godot;
using System;

public partial class TilesetComponentPositions(int size = 100) : Node
{
	public int size = size;

	public Rect2I TopLeftCorner {
		get => new Rect2I(new Vector2I(size * 0, size * 0), new Vector2I(size, size));}
    public Rect2I TopLeftHorizontal {
		get => new Rect2I(new Vector2I(size * 4, size * 0), new Vector2I(size, size));}
    public Rect2I TopLeftFill {
		get => new Rect2I(new Vector2I(size * 2, size * 2), new Vector2I(size, size));}
    public Rect2I TopLeftVertical {
		get => new Rect2I(new Vector2I(size * 0, size * 4), new Vector2I(size, size));}
    public Rect2I TopLeftJoint {
		get => new Rect2I(new Vector2I(size * 4, size * 4), new Vector2I(size, size));}
		
    public Rect2I TopRightCorner {
		get => new Rect2I(new Vector2I(size * 5, size * 0), new Vector2I(size, size));}
    public Rect2I TopRightHorizontal {
		get => new Rect2I(new Vector2I(size * 1, size * 0), new Vector2I(size, size));}
    public Rect2I TopRightFill {
		get => new Rect2I(new Vector2I(size * 3, size * 2), new Vector2I(size, size));}
    public Rect2I TopRightVertical {
		get => new Rect2I(new Vector2I(size * 5, size * 4), new Vector2I(size, size));}
    public Rect2I TopRightJoint {
		get => new Rect2I(new Vector2I(size * 1, size * 4), new Vector2I(size, size));}
		
    public Rect2I BottomLeftCorner {
		get => new Rect2I(new Vector2I(size * 0, size * 5), new Vector2I(size, size));}
    public Rect2I BottomLeftHorizontal {
		get => new Rect2I(new Vector2I(size * 4, size * 5), new Vector2I(size, size));}
    public Rect2I BottomLeftFill {
		get => new Rect2I(new Vector2I(size * 2, size * 3), new Vector2I(size, size));}
    public Rect2I BottomLeftVertical {
		get => new Rect2I(new Vector2I(size * 0, size * 1), new Vector2I(size, size));}
    public Rect2I BottomLeftJoint {
		get => new Rect2I(new Vector2I(size * 4, size * 1), new Vector2I(size, size));}
		
    public Rect2I BottomRightCorner {
		get => new Rect2I(new Vector2I(size * 5, size * 5), new Vector2I(size, size));}
    public Rect2I BottomRightHorizontal {
		get => new Rect2I(new Vector2I(size * 1, size * 5), new Vector2I(size, size));}
    public Rect2I BottomRightFill {
		get => new Rect2I(new Vector2I(size * 3, size * 3), new Vector2I(size, size));}
    public Rect2I BottomRightVertical {
		get => new Rect2I(new Vector2I(size * 5, size * 1), new Vector2I(size, size));}
    public Rect2I BottomRightJoint {
		get => new Rect2I(new Vector2I(size * 1, size * 1), new Vector2I(size, size));}
}
