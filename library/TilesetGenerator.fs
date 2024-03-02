module TilesetGenerator

open System.IO
open System.Drawing
open Tile
open UnitsOfMeasure
open Pixel


type GenerateTilesParameters = {
    SourceDirectory: DirectoryInfo
    OutputDirectory: DirectoryInfo
    NumberOfTiles: int<tile>
    TileWidth: int<pixel>
    TilesPerRow: int<tile>
}

let createTiles generateTilesParameters (tileHeight: int<pixel>) =
    let calculateLeftSideX (tileIndex:int) = 
        let columnIndex = tileIndex % (int generateTilesParameters.TilesPerRow)
        columnIndex * generateTilesParameters.TileWidth

    let calculateTopSideY (tileIndex: int) =
        let rowIndex = tileIndex / (int generateTilesParameters.TilesPerRow)
        rowIndex * tileHeight

    let calculateBoundingBoxTopLeft tileIndex = { X = (calculateLeftSideX tileIndex); Y = (calculateTopSideY tileIndex) } 

    let createTileAtTileIndex tileIndex = 
        let boundingBoxTopLeft = calculateBoundingBoxTopLeft tileIndex
        createTile boundingBoxTopLeft generateTilesParameters.TileWidth

    let tiles = List.init (int generateTilesParameters.NumberOfTiles) createTileAtTileIndex
    List.chunkBySize (int generateTilesParameters.TilesPerRow) tiles

let generateFromSourceImage generateTilesParameters =
    let tileHeight = generateTilesParameters.TileWidth / 2

    let rowsOfTiles = createTiles generateTilesParameters tileHeight

    let numberOfRows = rowsOfTiles.Length

    let totalWidth = (int generateTilesParameters.TileWidth) * (int generateTilesParameters.TilesPerRow)

    let totalHeight = (int tileHeight) * (int numberOfRows)

    use bitmap = new Bitmap(totalWidth, totalHeight)
    use graphics = Graphics.FromImage(bitmap)

    for row in rowsOfTiles do
        for tile in row do
            tile.DrawBoundingBox graphics
            tile.DrawTile graphics

    let outputPath = Path.Combine(generateTilesParameters.OutputDirectory.FullName, "tileset.png")
    bitmap.Save(outputPath, Imaging.ImageFormat.Png)