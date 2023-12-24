module TilesetGenerator

open System.IO
open System.Drawing


let generateFromSourceImage (sourceDirectory:DirectoryInfo) (outputDirectory:DirectoryInfo) =
    let bitmap = new Bitmap(1, 1)  // Create a tiny bitmap
    let outputPath = Path.Combine(outputDirectory.FullName, "generated.png")
    bitmap.Save(outputPath, Imaging.ImageFormat.Png)