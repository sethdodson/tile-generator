module TilesetGenerator

open System.IO
open System.Drawing
open Tile


let createTiles (numberOfTiles: int) (tileWidth: int) (tilesPerRow: int) =
    [for i in 0 .. numberOfTiles - 1 ->
        let x = i * tileWidth
        let y = tileWidth / 2
        let boundingBoxTopLeft = Point(x, y)
        createTile boundingBoxTopLeft tileWidth]

let generateFromSourceImage (sourceDirectory: DirectoryInfo) (outputDirectory: DirectoryInfo) (numberOfTiles: int) =
    let tileWidth, tileHeight = 256, 128  // Bounding box dimensions of each tile
    use bitmap = new Bitmap(tileWidth, tileHeight)
    use graphics = Graphics.FromImage(bitmap)
    let tile = Tile.createTile (new Point(0, 128)) 256
    tile.DrawBoundingBox graphics
    tile.DrawTile graphics
    let outputPath = Path.Combine(outputDirectory.FullName, "tileset.png")
    bitmap.Save(outputPath, Imaging.ImageFormat.Png)