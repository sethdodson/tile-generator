open TilesetGenerator
open System.IO

[<EntryPoint>]
let main argv =
    let sourceDirectoryPath = argv.[0]
    let outputDirectoryPath = argv.[1]
    let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)
    let outputDirectory = new DirectoryInfo(outputDirectoryPath)
    let numberOfTiles = argv.[2] |> int
    let tilesPerRow = argv.[3] |> int
    let tileWidth = 256


    generateFromSourceImage sourceDirectory outputDirectory numberOfTiles tileWidth tilesPerRow

    0 // return an integer exit code
