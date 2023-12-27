module TilesetGenerator

open System.IO
open System.Drawing


let generateFromSourceImage (sourceDirectory:DirectoryInfo) (outputDirectory:DirectoryInfo) =
    use bitmap = new Bitmap(1280, 640)
    let outputPath = Path.Combine(outputDirectory.FullName, "generated.png")

    // Set the first pixel to a different color
    bitmap.SetPixel(0, 0, Color.FromArgb(255, 0, 0, 0))  // Set to black, for example

    bitmap.Save(outputPath, Imaging.ImageFormat.Png)
