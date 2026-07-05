#if TOOLS
using Godot;
using System.Diagnostics;
using System.Threading;

[Tool]
public partial class TilesetGenerator : Control
{
	[Export] private LineEdit baseTileSheetPathInput;
	[Export] private LineEdit TileSetNameInput;
	[Export] private LineEdit OutputFolderPathInput;
	
	private Texture2D baseTileSheet;
	private string tilesheetOutputPath => OutputFolderPathInput.Text + TileSetNameInput.Text + "TileSheet.png";
	private string tilesetOutputPath => OutputFolderPathInput.Text + TileSetNameInput.Text + "TileSet.tres";

	private int tileSize;
	private int componentSize => tileSize / 2;
	public void GenerateTileSet()
	{
		string baseTileSheetPath = baseTileSheetPathInput.Text;
		baseTileSheet = GD.Load<Texture2D>(baseTileSheetPath);
		
		if (baseTileSheet == null)
		{
			Debug.WriteLine($"Base TileSheet Image could not be loaded.");
			return;
		}

		Image originalImage = baseTileSheet.GetImage();
		
		var imgDimensions = originalImage.GetSize();
		bool isSquare = imgDimensions.X == imgDimensions.Y;
		if (!isSquare)
		{
			Debug.WriteLine($"Image dimensions are not square");
			return;
		}
		
		tileSize = imgDimensions.X / 3;
		Vector2I size = new Vector2I(componentSize, componentSize);

		//Image copiedImage = originalImage.GetRegion(new Rect2I(Vector2I.Zero, imgDimensions));

		TilesetComponentPositions compPos = new TilesetComponentPositions(componentSize);
		
		Image fullTilesheet = Image.CreateEmpty(componentSize * 24, componentSize * 8, false, Image.Format.Rgba8);

		#region template generation

		#region Row 1
		// Tile 1
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftCorner, new Vector2I(componentSize * 0, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightCorner, new Vector2I(componentSize * 1, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftVertical, new Vector2I(componentSize * 0, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightVertical, new Vector2I(componentSize * 1, componentSize * 1));
		// Tile 2
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftCorner, new Vector2I(componentSize * 2, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightHorizontal, new Vector2I(componentSize * 3, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftVertical, new Vector2I(componentSize * 2, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 3, componentSize * 1));
		// Tile 3
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftHorizontal, new Vector2I(componentSize * 4, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightHorizontal, new Vector2I(componentSize * 5, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 4, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 5, componentSize * 1));
		// Tile 4
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftHorizontal, new Vector2I(componentSize * 6, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightCorner, new Vector2I(componentSize * 7, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 6, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightVertical, new Vector2I(componentSize * 7, componentSize * 1));
		// Tile 5
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 8, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 9, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 8, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 9, componentSize * 1));
		// Tile 6
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftHorizontal, new Vector2I(componentSize * 10, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightHorizontal, new Vector2I(componentSize * 11, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 10, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 11, componentSize * 1));
		// Tile 7
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftHorizontal, new Vector2I(componentSize * 12, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightHorizontal, new Vector2I(componentSize * 13, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 12, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 13, componentSize * 1));
		// Tile 8
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 14, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 15, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 14, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 15, componentSize * 1));
		// Tile 9
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftCorner, new Vector2I(componentSize * 16, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightHorizontal, new Vector2I(componentSize * 17, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftVertical, new Vector2I(componentSize * 16, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 17, componentSize * 1));
		// Tile 10
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 18, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 19, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 18, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 19, componentSize * 1));
		// Tile 11
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftHorizontal, new Vector2I(componentSize * 20, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightHorizontal, new Vector2I(componentSize * 21, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 20, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 21, componentSize * 1));
		// Tile 12
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftHorizontal, new Vector2I(componentSize * 22, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightCorner, new Vector2I(componentSize * 23, componentSize * 0));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 22, componentSize * 1));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightVertical, new Vector2I(componentSize * 23, componentSize * 1));

		#endregion

		#region Row 2

		// Tile 1
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftVertical, new Vector2I(componentSize * 0, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightVertical, new Vector2I(componentSize * 1, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftVertical, new Vector2I(componentSize * 0, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightVertical, new Vector2I(componentSize * 1, componentSize * 3));
		// Tile 2
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftVertical, new Vector2I(componentSize * 2, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 3, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftVertical, new Vector2I(componentSize * 2, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 3, componentSize * 3));
		// Tile 3
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 4, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 5, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 4, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 5, componentSize * 3));
		// Tile 4
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 6, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightVertical, new Vector2I(componentSize * 7, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 6, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightVertical, new Vector2I(componentSize * 7, componentSize * 3));
		// Tile 5
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftVertical, new Vector2I(componentSize * 8, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 9, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftVertical, new Vector2I(componentSize * 8, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 9, componentSize * 3));
		// Tile 6
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 10, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 11, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 10, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 11, componentSize * 3));
		// Tile 7
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 12, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 13, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 12, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 13, componentSize * 3));
		// Tile 8
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 14, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightVertical, new Vector2I(componentSize * 15, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 14, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightVertical, new Vector2I(componentSize * 15, componentSize * 3));
		// Tile 9
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftVertical, new Vector2I(componentSize * 16, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 17, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftVertical, new Vector2I(componentSize * 16, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 17, componentSize * 3));
		// Tile 10
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 18, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 19, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 18, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 19, componentSize * 3));
		// Tile 11
		// (Empty tile)
		// Tile 12
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 22, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 23, componentSize * 2));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 22, componentSize * 3));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 23, componentSize * 3));

		#endregion
		
		#region Row 3

		// Tile 1
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftVertical, new Vector2I(componentSize * 0, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightVertical, new Vector2I(componentSize * 1, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftCorner, new Vector2I(componentSize * 0, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightCorner, new Vector2I(componentSize * 1, componentSize * 5));
		// Tile 2
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftVertical, new Vector2I(componentSize * 2, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 3, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftCorner, new Vector2I(componentSize * 2, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightHorizontal, new Vector2I(componentSize * 3, componentSize * 5));
		// Tile 3
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 4, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 5, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftHorizontal, new Vector2I(componentSize * 4, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightHorizontal, new Vector2I(componentSize * 5, componentSize * 5));
		// Tile 4
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 6, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightVertical, new Vector2I(componentSize * 7, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftHorizontal, new Vector2I(componentSize * 6, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightCorner, new Vector2I(componentSize * 7, componentSize * 5));
		// Tile 5
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftVertical, new Vector2I(componentSize * 8, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 9, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftVertical, new Vector2I(componentSize * 8, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 9, componentSize * 5));
		// Tile 6
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 10, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 11, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 10, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 11, componentSize * 5));
		// Tile 7
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 12, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 13, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 12, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 13, componentSize * 5));
		// Tile 8
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 14, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightVertical, new Vector2I(componentSize * 15, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 14, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightVertical, new Vector2I(componentSize * 15, componentSize * 5));
		// Tile 9
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 16, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 17, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 16, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 17, componentSize * 5));
		// Tile 10
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 18, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 19, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 18, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 19, componentSize * 5));
		// Tile 11
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 20, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 21, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 20, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 21, componentSize * 5));
		// Tile 12
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 22, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightVertical, new Vector2I(componentSize * 23, componentSize * 4));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 22, componentSize * 5));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightVertical, new Vector2I(componentSize * 23, componentSize * 5));

		#endregion
		
		#region Row 4

		// Tile 1
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftCorner, new Vector2I(componentSize * 0, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightCorner, new Vector2I(componentSize * 1, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftCorner, new Vector2I(componentSize * 0, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightCorner, new Vector2I(componentSize * 1, componentSize * 7));
		// Tile 2
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftCorner, new Vector2I(componentSize * 2, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightHorizontal, new Vector2I(componentSize * 3, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftCorner, new Vector2I(componentSize * 2, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightHorizontal, new Vector2I(componentSize * 3, componentSize * 7));
		// Tile 3
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftHorizontal, new Vector2I(componentSize * 4, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightHorizontal, new Vector2I(componentSize * 5, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftHorizontal, new Vector2I(componentSize * 4, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightHorizontal, new Vector2I(componentSize * 5, componentSize * 7));
		// Tile 4
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftHorizontal, new Vector2I(componentSize * 6, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightCorner, new Vector2I(componentSize * 7, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftHorizontal, new Vector2I(componentSize * 6, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightCorner, new Vector2I(componentSize * 7, componentSize * 7));
		// Tile 5
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 8, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 9, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftFill, new Vector2I(componentSize * 8, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 9, componentSize * 7));
		// Tile 6
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 10, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 11, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftHorizontal, new Vector2I(componentSize * 10, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightHorizontal, new Vector2I(componentSize * 11, componentSize * 7));
		// Tile 7
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 12, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 13, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftHorizontal, new Vector2I(componentSize * 12, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightHorizontal, new Vector2I(componentSize * 13, componentSize * 7));
		// Tile 8
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftJoint, new Vector2I(componentSize * 14, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightJoint, new Vector2I(componentSize * 15, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 14, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightFill, new Vector2I(componentSize * 15, componentSize * 7));
		// Tile 9
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftVertical, new Vector2I(componentSize * 16, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 17, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftCorner, new Vector2I(componentSize * 16, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightHorizontal, new Vector2I(componentSize * 17, componentSize * 7));
		// Tile 10
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 18, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 19, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftHorizontal, new Vector2I(componentSize * 18, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightHorizontal, new Vector2I(componentSize * 19, componentSize * 7));
		// Tile 11
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 20, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightFill, new Vector2I(componentSize * 21, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftJoint, new Vector2I(componentSize * 20, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightJoint, new Vector2I(componentSize * 21, componentSize * 7));
		// Tile 12
		fullTilesheet.BlitRect(originalImage, compPos.TopLeftFill, new Vector2I(componentSize * 22, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.TopRightVertical, new Vector2I(componentSize * 23, componentSize * 6));
		fullTilesheet.BlitRect(originalImage, compPos.BottomLeftHorizontal, new Vector2I(componentSize * 22, componentSize * 7));
		fullTilesheet.BlitRect(originalImage, compPos.BottomRightCorner, new Vector2I(componentSize * 23, componentSize * 7));

		#endregion
		#endregion
		
		var result = fullTilesheet.SavePng(tilesheetOutputPath);
		if (result == Error.Ok)
		{
			GenerateTileset(fullTilesheet);
			EditorInterface.Singleton.GetResourceFilesystem().Scan();
		}
	}

	private void GenerateTileset(Image tilesheet)
	{
		TileSetAtlasSource atlas = GenerateTileSetAtlasSource(tilesheet);
		
		TileSet tileSet = new TileSet();
		tileSet.AddSource(atlas);
		tileSet.SetTileSize(new Vector2I(tileSize, tileSize));
		
		tileSet.AddTerrainSet(0);
		tileSet.AddTerrain(0, 0);
		tileSet.SetTerrainName(0, 0,"Terrain");
		tileSet.SetTerrainSetMode(0, TileSet.TerrainMode.CornersAndSides);

		SetTerrain(atlas);
		
		ResourceSaver.Save(tileSet, tilesetOutputPath);
	}

	private TileSetAtlasSource GenerateTileSetAtlasSource(Image tilesheet)
	{
		TileSetAtlasSource atlas = new TileSetAtlasSource();
		atlas.SetTextureRegionSize(new Vector2I(tileSize, tileSize));

		ImageTexture texture = new ImageTexture();
		texture.SetImage(tilesheet);
		//Texture2D texture = GD.Load<Texture2D>(tilesheetOutputPath);
		atlas.Texture = texture;
		for (int row = 0; row < 4; row++)
		{
			for (int col = 0; col < 12; col++)
			{
				atlas.CreateTile(new Vector2I(col, row));
			}
		}

		
		return atlas;
	}

	private void SetTerrain(TileSetAtlasSource atlas)
	{
		atlas.Set("0:0/0/terrain_set", 0);
		atlas.Set("0:0/0/terrain", 0);
		atlas.Set("0:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("1:0/0/terrain_set", 0);
		atlas.Set("1:0/0/terrain", 0);
		atlas.Set("1:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("1:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("2:0/0/terrain_set", 0);
		atlas.Set("2:0/0/terrain", 0);
		atlas.Set("2:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("2:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("2:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("3:0/0/terrain_set", 0);
		atlas.Set("3:0/0/terrain", 0);
		atlas.Set("3:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("3:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("4:0/0/terrain_set", 0);
		atlas.Set("4:0/0/terrain", 0);
		atlas.Set("4:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("4:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("4:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("4:0/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("4:0/0/terrains_peering_bit/top_side", 0);
		atlas.Set("5:0/0/terrain_set", 0);
		atlas.Set("5:0/0/terrain", 0);
		atlas.Set("5:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("5:0/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("5:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("5:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("6:0/0/terrain_set", 0);
		atlas.Set("6:0/0/terrain", 0);
		atlas.Set("6:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("6:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("6:0/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("6:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("7:0/0/terrain_set", 0);
		atlas.Set("7:0/0/terrain", 0);
		atlas.Set("7:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("7:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("7:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("7:0/0/terrains_peering_bit/top_side", 0);
		atlas.Set("7:0/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("8:0/0/terrain_set", 0);
		atlas.Set("8:0/0/terrain", 0);
		atlas.Set("8:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("8:0/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("8:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("9:0/0/terrain_set", 0);
		atlas.Set("9:0/0/terrain", 0);
		atlas.Set("9:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("9:0/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("9:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("9:0/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("9:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("9:0/0/terrains_peering_bit/top_side", 0);
		atlas.Set("10:0/0/terrain_set", 0);
		atlas.Set("10:0/0/terrain", 0);
		atlas.Set("10:0/0/terrains_peering_bit/right_side", 0);
		atlas.Set("10:0/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("10:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("10:0/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("10:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("11:0/0/terrain_set", 0);
		atlas.Set("11:0/0/terrain", 0);
		atlas.Set("11:0/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("11:0/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("11:0/0/terrains_peering_bit/left_side", 0);
		atlas.Set("0:1/0/terrain_set", 0);
		atlas.Set("0:1/0/terrain", 0);
		atlas.Set("0:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("0:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("1:1/0/terrain_set", 0);
		atlas.Set("1:1/0/terrain", 0);
		atlas.Set("1:1/0/terrains_peering_bit/right_side", 0);
		atlas.Set("1:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("1:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("2:1/0/terrain_set", 0);
		atlas.Set("2:1/0/terrain", 0);
		atlas.Set("2:1/0/terrains_peering_bit/right_side", 0);
		atlas.Set("2:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("2:1/0/terrains_peering_bit/left_side", 0);
		atlas.Set("2:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("3:1/0/terrain_set", 0);
		atlas.Set("3:1/0/terrain", 0);
		atlas.Set("3:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("3:1/0/terrains_peering_bit/left_side", 0);
		atlas.Set("3:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("4:1/0/terrain_set", 0);
		atlas.Set("4:1/0/terrain", 0);
		atlas.Set("4:1/0/terrains_peering_bit/right_side", 0);
		atlas.Set("4:1/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("4:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("4:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("5:1/0/terrain_set", 0);
		atlas.Set("5:1/0/terrain", 0);
		atlas.Set("5:1/0/terrains_peering_bit/right_side", 0);
		atlas.Set("5:1/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("5:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("5:1/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("5:1/0/terrains_peering_bit/left_side", 0);
		atlas.Set("5:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("5:1/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("6:1/0/terrain_set", 0);
		atlas.Set("6:1/0/terrain", 0);
		atlas.Set("6:1/0/terrains_peering_bit/right_side", 0);
		atlas.Set("6:1/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("6:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("6:1/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("6:1/0/terrains_peering_bit/left_side", 0);
		atlas.Set("6:1/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("6:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("7:1/0/terrain_set", 0);
		atlas.Set("7:1/0/terrain", 0);
		atlas.Set("7:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("7:1/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("7:1/0/terrains_peering_bit/left_side", 0);
		atlas.Set("7:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("8:1/0/terrain_set", 0);
		atlas.Set("8:1/0/terrain", 0);
		atlas.Set("8:1/0/terrains_peering_bit/right_side", 0);
		atlas.Set("8:1/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("8:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("8:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("8:1/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("9:1/0/terrain_set", 0);
		atlas.Set("9:1/0/terrain", 0);
		atlas.Set("9:1/0/terrains_peering_bit/right_side", 0);
		atlas.Set("9:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("9:1/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("9:1/0/terrains_peering_bit/left_side", 0);
		atlas.Set("9:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("9:1/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("11:1/0/terrain_set", 0);
		atlas.Set("11:1/0/terrain", 0);
		atlas.Set("11:1/0/terrains_peering_bit/right_side", 0);
		atlas.Set("11:1/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("11:1/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("11:1/0/terrains_peering_bit/left_side", 0);
		atlas.Set("11:1/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("11:1/0/terrains_peering_bit/top_side", 0);
		atlas.Set("0:2/0/terrain_set", 0);
		atlas.Set("0:2/0/terrain", 0);
		atlas.Set("0:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("1:2/0/terrain_set", 0);
		atlas.Set("1:2/0/terrain", 0);
		atlas.Set("1:2/0/terrains_peering_bit/right_side", 0);
		atlas.Set("1:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("2:2/0/terrain_set", 0);
		atlas.Set("2:2/0/terrain", 0);
		atlas.Set("2:2/0/terrains_peering_bit/right_side", 0);
		atlas.Set("2:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("2:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("3:2/0/terrain_set", 0);
		atlas.Set("3:2/0/terrain", 0);
		atlas.Set("3:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("3:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("4:2/0/terrain_set", 0);
		atlas.Set("4:2/0/terrain", 0);
		atlas.Set("4:2/0/terrains_peering_bit/right_side", 0);
		atlas.Set("4:2/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("4:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("4:2/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("5:2/0/terrain_set", 0);
		atlas.Set("5:2/0/terrain", 0);
		atlas.Set("5:2/0/terrains_peering_bit/right_side", 0);
		atlas.Set("5:2/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("5:2/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("5:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("5:2/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("5:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("5:2/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("6:2/0/terrain_set", 0);
		atlas.Set("6:2/0/terrain", 0);
		atlas.Set("6:2/0/terrains_peering_bit/right_side", 0);
		atlas.Set("6:2/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("6:2/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("6:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("6:2/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("6:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("6:2/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("7:2/0/terrain_set", 0);
		atlas.Set("7:2/0/terrain", 0);
		atlas.Set("7:2/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("7:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("7:2/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("7:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("8:2/0/terrain_set", 0);
		atlas.Set("8:2/0/terrain", 0);
		atlas.Set("8:2/0/terrains_peering_bit/right_side", 0);
		atlas.Set("8:2/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("8:2/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("8:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("8:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("8:2/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("9:2/0/terrain_set", 0);
		atlas.Set("9:2/0/terrain", 0);
		atlas.Set("9:2/0/terrains_peering_bit/right_side", 0);
		atlas.Set("9:2/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("9:2/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("9:2/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("9:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("9:2/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("9:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("9:2/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("10:2/0/terrain_set", 0);
		atlas.Set("10:2/0/terrain", 0);
		atlas.Set("10:2/0/terrains_peering_bit/right_side", 0);
		atlas.Set("10:2/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("10:2/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("10:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("10:2/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("10:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("11:2/0/terrain_set", 0);
		atlas.Set("11:2/0/terrain", 0);
		atlas.Set("11:2/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("11:2/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("11:2/0/terrains_peering_bit/left_side", 0);
		atlas.Set("11:2/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("11:2/0/terrains_peering_bit/top_side", 0);
		atlas.Set("0:3/0/terrain_set", 0);
		atlas.Set("0:3/0/terrain", 0);
		atlas.Set("1:3/0/terrain_set", 0);
		atlas.Set("1:3/0/terrain", 0);
		atlas.Set("1:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("2:3/0/terrain_set", 0);
		atlas.Set("2:3/0/terrain", 0);
		atlas.Set("2:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("2:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("3:3/0/terrain_set", 0);
		atlas.Set("3:3/0/terrain", 0);
		atlas.Set("3:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("4:3/0/terrain_set", 0);
		atlas.Set("4:3/0/terrain", 0);
		atlas.Set("4:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("4:3/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("4:3/0/terrains_peering_bit/bottom_left_corner", 0);
		atlas.Set("4:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("4:3/0/terrains_peering_bit/top_side", 0);
		atlas.Set("5:3/0/terrain_set", 0);
		atlas.Set("5:3/0/terrain", 0);
		atlas.Set("5:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("5:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("5:3/0/terrains_peering_bit/top_side", 0);
		atlas.Set("5:3/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("6:3/0/terrain_set", 0);
		atlas.Set("6:3/0/terrain", 0);
		atlas.Set("6:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("6:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("6:3/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("6:3/0/terrains_peering_bit/top_side", 0);
		atlas.Set("7:3/0/terrain_set", 0);
		atlas.Set("7:3/0/terrain", 0);
		atlas.Set("7:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("7:3/0/terrains_peering_bit/bottom_right_corner", 0);
		atlas.Set("7:3/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("7:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("7:3/0/terrains_peering_bit/top_side", 0);
		atlas.Set("8:3/0/terrain_set", 0);
		atlas.Set("8:3/0/terrain", 0);
		atlas.Set("8:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("8:3/0/terrains_peering_bit/top_side", 0);
		atlas.Set("8:3/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("9:3/0/terrain_set", 0);
		atlas.Set("9:3/0/terrain", 0);
		atlas.Set("9:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("9:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("9:3/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("9:3/0/terrains_peering_bit/top_side", 0);
		atlas.Set("9:3/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("10:3/0/terrain_set", 0);
		atlas.Set("10:3/0/terrain", 0);
		atlas.Set("10:3/0/terrains_peering_bit/right_side", 0);
		atlas.Set("10:3/0/terrains_peering_bit/bottom_side", 0);
		atlas.Set("10:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("10:3/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("10:3/0/terrains_peering_bit/top_side", 0);
		atlas.Set("10:3/0/terrains_peering_bit/top_right_corner", 0);
		atlas.Set("11:3/0/terrain_set", 0);
		atlas.Set("11:3/0/terrain", 0);
		atlas.Set("11:3/0/terrains_peering_bit/left_side", 0);
		atlas.Set("11:3/0/terrains_peering_bit/top_left_corner", 0);
		atlas.Set("11:3/0/terrains_peering_bit/top_side", 0);
	}
	
	

}
#endif