module TilesetGeneratorTests

open Faqt
open Xunit
open System.IO
open System.Drawing
open System
open Tile
open TilesetGenerator

type TilesetGeneratorTests() =
    let projectDirectoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
    let sourceDirectoryPath = Path.Combine(projectDirectoryPath, "source-images")
    let outputDirectoryPath = Path.Combine(projectDirectoryPath, "tiles")    

    interface IDisposable with
        member _.Dispose() =
            // clean up the output directory
            Console.WriteLine("Cleaning up output directory...")
            let files = Directory.GetFiles(outputDirectoryPath)
            for file in files do
                File.Delete(file)

    [<Fact>]
    member _. ``generateFromSourceImage generates a png given valid directories`` () =
        // Arrange             
        let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)        
        let outputDirectory = new DirectoryInfo(outputDirectoryPath)
        let numberOfTiles = 1

        // Act
        generateFromSourceImage sourceDirectory outputDirectory numberOfTiles

        // Assert
        outputDirectory.GetFiles().Should().HaveLength(1)

    [<Fact>]
    member _.``generated tile has correct dimensions`` () =
        // Arrange
        let expectedWidth = 256
        let expectedHeight =  128
        let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)
        let outputDirectory = new DirectoryInfo(outputDirectoryPath)
        let numberOfTiles = 1

        // Act
        generateFromSourceImage sourceDirectory outputDirectory numberOfTiles

        // Assert
        let files = outputDirectory.GetFiles()
        files.Length.Should().Be(1) |> ignore
        use tilesetImage = Image.FromFile(files.[0].FullName) :?> Bitmap
        tilesetImage.Width.Should().Be(expectedWidth) |> ignore
        tilesetImage.Height.Should().Be(expectedHeight) |> ignore
    
    [<Fact>]
    member _.``generated tileset is not of uniform color`` () =
        // Arrange
        let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)
        let outputDirectory = new DirectoryInfo(outputDirectoryPath)
        let numberOfTiles = 1

        // Act
        generateFromSourceImage sourceDirectory outputDirectory numberOfTiles

        // Assert
        let files = outputDirectory.GetFiles()
        files.Length.Should().Be(1) |> ignore
        use tilesetImage = Image.FromFile(files.[0].FullName) :?> Bitmap
        let firstPixel = tilesetImage.GetPixel(0, 0)
        let allPixels = 
            seq {
                for x in 0 .. tilesetImage.Width - 1 do
                    for y in 0 .. tilesetImage.Height - 1 do
                        yield tilesetImage.GetPixel(x, y)
            }
        let isUniformColor = allPixels |> Seq.forall (fun pixel -> pixel = firstPixel)
        isUniformColor.Should().BeFalse("if the source image isn't of uniform color, then neither should the tileset.") |> ignore

    [<Fact>]
    member _.``creating tiles generates the correct number of tiles``() =
        // Arrange
        let numberOfTiles = 10
        let tileWidth = 256
        let tilesPerRow = 1

        // Act
        let tiles = createTiles numberOfTiles tileWidth tilesPerRow

        // Assert
        tiles.Length.Should().Be(numberOfTiles) |> ignore

    [<Fact>]
    member _.``creating tiles with one row generates tiles with correct dimensions``() =
        // Arrange
        let numberOfTiles = 2
        let tileWidth = 256
        let tilesPerRow = 5

        let expectedTiles = 
            [
                { Top = Point(128, 128); Right = Point(256, 64); Bottom = Point(128, 0); Left = Point(0, 64); BoundingBoxTopLeft = Point(0, 128); BoundingBoxTopRight = Point(256, 128); BoundingBoxBottomLeft = Point(0, 0); BoundingBoxBottomRight = Point(256, 0) }
                { Top = Point(384, 128); Right = Point(512, 64); Bottom = Point(384, 0); Left = Point(256, 64); BoundingBoxTopLeft = Point(256, 128); BoundingBoxTopRight = Point(512, 128); BoundingBoxBottomLeft = Point(256, 0); BoundingBoxBottomRight = Point(512, 0) }
            ]

        // Act
        let tiles = createTiles numberOfTiles tileWidth tilesPerRow

        // Assert
        Assert.Equal<Tile list>(expectedTiles, tiles) |> ignore

    // [<Fact>]
    // member _.``creating tiles generates tiles with the correct dimensions and locations``() =
    //     // Arrange
    //     let numberOfTiles = 5
    //     let tileWidth = 256
    //     let tilesPerRow = 2

    //     let expectedTiles = 
    //         [
    //             { Top = Point(128, 384); Right = Point(256, 320); Bottom = Point(128, 256); Left = Point(0, 320); BoundingBoxTopLeft = Point(0, 384); BoundingBoxTopRight = Point(256, 384); BoundingBoxBottomLeft = Point(0, 256); BoundingBoxBottomRight = Point(256, 256) }
    //             { Top = Point(384, 384); Right = Point(512, 320); Bottom = Point(384, 256); Left = Point(256, 320); BoundingBoxTopLeft = Point(256, 384); BoundingBoxTopRight = Point(512, 384); BoundingBoxBottomLeft = Point(256, 256); BoundingBoxBottomRight = Point(512, 256) }
    //             { Top = Point(128, 256); Right = Point(256, 192); Bottom = Point(128, 128); Left = Point(0, 192); BoundingBoxTopLeft = Point(0, 256); BoundingBoxTopRight = Point(256, 256); BoundingBoxBottomLeft = Point(0, 128); BoundingBoxBottomRight = Point(256, 128) }
    //             { Top = Point(384, 256); Right = Point(512, 192); Bottom = Point(384, 128); Left = Point(256, 192); BoundingBoxTopLeft = Point(256, 256); BoundingBoxTopRight = Point(512, 256); BoundingBoxBottomLeft = Point(256, 128); BoundingBoxBottomRight = Point(512, 128) }
    //             { Top = Point(128, 128); Right = Point(256, 64); Bottom = Point(128, 0); Left = Point(0, 64); BoundingBoxTopLeft = Point(0, 128); BoundingBoxTopRight = Point(256, 128); BoundingBoxBottomLeft = Point(0, 0); BoundingBoxBottomRight = Point(256, 0) }
    //         ]

    //     // Act
    //     let tiles = createTiles numberOfTiles tileWidth tilesPerRow

    //     // Assert
    //     Assert.Equal<Tile list>(expectedTiles, tiles) |> ignore