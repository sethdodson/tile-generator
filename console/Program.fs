open TilesetGenerator
open System.IO

[<EntryPoint>]
let main argv =
    let sourceDirectoryPath = argv.[0]
    let outputDirectoryPath = argv.[1]
    let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)
    let outputDirectory = new DirectoryInfo(outputDirectoryPath)
    let numberOfTiles = argv.[2] |> int

    generateFromSourceImage sourceDirectory outputDirectory numberOfTiles

    0 // return an integer exit code
