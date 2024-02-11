module TilesetGenerator

open System.IO
open System.Drawing
open Tile


let createTiles numberOfTiles tileWidth tilesPerRow =
    let tileHeight = tileWidth / 2

    let calculateLeftSideX tileIndex = 
        let columnIndex = tileIndex % tilesPerRow
        columnIndex * tileWidth

    let calculateTopSideY tileIndex =
        let rowIndex = tileIndex / tilesPerRow
        rowIndex * tileHeight

    let calculateBoundingBoxTopLeft tileIndex = Point(calculateLeftSideX tileIndex, calculateTopSideY tileIndex)

    let createTileForTileIndex tileIndex = 
        let boundingBoxTopLeft = calculateBoundingBoxTopLeft tileIndex
        createTile boundingBoxTopLeft tileWidth

    [for i in 0 .. numberOfTiles - 1 -> createTileForTileIndex i]

let generateFromSourceImage (sourceDirectory: DirectoryInfo) (outputDirectory: DirectoryInfo) (numberOfTiles: int) =

    let tileWidth, tileHeight = 256, 128  // Bounding box dimensions of each tile
    use bitmap = new Bitmap(tileWidth, tileHeight)
    use graphics = Graphics.FromImage(bitmap)
    let tile = Tile.createTile (new Point(0, 128)) 256
    tile.DrawBoundingBox graphics
    tile.DrawTile graphics
    let outputPath = Path.Combine(outputDirectory.FullName, "tileset.png")
    bitmap.Save(outputPath, Imaging.ImageFormat.Png)