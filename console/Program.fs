open TilesetGenerator
open System.IO
open UnitsOfMeasure

[<EntryPoint>]
let main argv =
    let sourceDirectoryPath = argv.[0]
    let outputDirectoryPath = argv.[1]
    let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)
    let outputDirectory = new DirectoryInfo(outputDirectoryPath)
    let numberOfTiles = argv.[2] |> int
    let tilesPerRow = argv.[3] |> int
    let tileWidth = 256

    let parameters = 
        { 
            SourceDirectory = sourceDirectory; 
            OutputDirectory = outputDirectory; 
            NumberOfTiles = numberOfTiles * 1<tile>; 
            TileWidth = tileWidth * 1<pixel>; 
            TilesPerRow = tilesPerRow * 1<tile>;
        }


    generateFromSourceImage parameters

    0 // return an integer exit code
