module TilesetGenerator

open System.IO
open System.Drawing


let generateFromSourceImage (sourceDirectory: DirectoryInfo) (outputDirectory: DirectoryInfo) (numberOfTiles: int) =
    let tileWidth, tileHeight = 256, 128  // Bounding box dimensions of each tile
    use bitmap = new Bitmap(tileWidth, tileHeight)
    use graphics = Graphics.FromImage(bitmap)
    let pen = new Pen(Color.Black, 2.0f)  // Pen for drawing borders

    // Correctly draw one tile within the 256x128 bounding box
    let points = [|
        Point(tileWidth / 2, 0)                         // Top
        Point(tileWidth, tileHeight / 2)                // Right
        Point(tileWidth / 2, tileHeight)                // Bottom
        Point(0, tileHeight / 2)                        // Left
        Point(tileWidth / 2, 0)                         // Close the diamond (back to Top)
    |]
    graphics.DrawPolygon(pen, points)

    let outputPath = Path.Combine(outputDirectory.FullName, "tileset.png")
    bitmap.Save(outputPath, Imaging.ImageFormat.Png)