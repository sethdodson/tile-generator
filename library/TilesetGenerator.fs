module TilesetGenerator

open System.IO
open System.Drawing
open Tile


let createTiles numberOfTiles tileWidth tileHeight tilesPerRow =
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

    let tiles = [for i in 0 .. numberOfTiles - 1 -> createTileForTileIndex i]
    List.chunkBySize tilesPerRow tiles

let generateFromSourceImage (sourceDirectory: DirectoryInfo) (outputDirectory: DirectoryInfo) numberOfTiles tileWidth tilesPerRow =
    let tileHeight = tileWidth / 2

    let rowsOfTiles = createTiles numberOfTiles tileWidth tileHeight tilesPerRow

    let numberOfRows = rowsOfTiles.Length
    use bitmap = new Bitmap(tileWidth * tilesPerRow, tileHeight * numberOfRows)
    use graphics = Graphics.FromImage(bitmap)

    for row in rowsOfTiles do
        for tile in row do
            tile.DrawBoundingBox graphics
            tile.DrawTile graphics

    let outputPath = Path.Combine(outputDirectory.FullName, "tileset.png")
    bitmap.Save(outputPath, Imaging.ImageFormat.Png)