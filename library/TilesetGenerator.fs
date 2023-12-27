module TilesetGenerator

open System.IO
open System.Drawing


let generateFromSourceImage (sourceDirectory:DirectoryInfo) (outputDirectory:DirectoryInfo) =
    use bitmap = new Bitmap(1280, 640)
    let outputPath = Path.Combine(outputDirectory.FullName, "generated.png")
    bitmap.Save(outputPath, Imaging.ImageFormat.Png)