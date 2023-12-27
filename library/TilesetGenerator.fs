module TilesetGenerator

open System.IO
open System.Drawing


let generateFromSourceImage (sourceDirectory:DirectoryInfo) (outputDirectory:DirectoryInfo) =
    let tileWidth, tileHeight = 256, 256
    let tilesAcross, tilesDown = 5, 4  // Example dimensions
    let tilesetWidth = tileWidth * tilesAcross
    let tilesetHeight = tileHeight + (tilesDown - 1) * (tileHeight / 2) // Simplified for a grid layout

    use bitmap = new Bitmap(tilesetWidth, tilesetHeight)
    use font = new Font("Arial", 20.0f)
    let brush = Brushes.Black
    use format = new StringFormat()
    format.LineAlignment <- StringAlignment.Center
    format.Alignment <- StringAlignment.Center

    use graphics = Graphics.FromImage(bitmap)
    for x in 0 .. tilesAcross - 1 do
        for y in 0 .. tilesDown - 1 do
            let tileNumber = (y * tilesAcross) + x + 1
            let centerX = (x * tileWidth) + (tileWidth / 2)
            let centerY = (y * tileHeight) + (tileHeight / 2)
            graphics.DrawString(tileNumber.ToString(), font, brush, PointF(float32 centerX, float32 centerY), format)

    let outputPath = Path.Combine(outputDirectory.FullName, "tileset.png")
    bitmap.Save(outputPath, Imaging.ImageFormat.Png)
