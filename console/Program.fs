open TilesetGenerator
open System.IO

[<EntryPoint>]
let main argv =
    let sourceDirectoryPath = argv.[0]
    let outputDirectoryPath = argv.[1]
    let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)
    let outputDirectory = new DirectoryInfo(outputDirectoryPath)

    (generateFromSourceImage sourceDirectory outputDirectory)

    0 // return an integer exit code
