module TilesetGeneratorTests

open Faqt
open Xunit
open System.IO
open System.Drawing
open System
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

        // Act
        generateFromSourceImage sourceDirectory outputDirectory

        // Assert
        outputDirectory.GetFiles().Should().HaveLength(1)

    [<Fact>]
    member _.``generated tileset has correct dimensions`` () =
        // Arrange
        let tilesAcross, tilesDown = 5, 4  // 5 tiles across and 4 tiles down
        let tileWidth, tileHeight = 256, 256
        let expectedWidth = 1280 // 5 tiles across
        let expectedHeight =  640 //tileHeight + (tilesDown - 1) * (tileHeight / 2)
        let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)
        let outputDirectory = new DirectoryInfo(outputDirectoryPath)

        // Act
        generateFromSourceImage sourceDirectory outputDirectory

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

        // Act
        generateFromSourceImage sourceDirectory outputDirectory

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

    
