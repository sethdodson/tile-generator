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
        let numberOfTiles = 1

        // Act
        generateFromSourceImage sourceDirectory outputDirectory numberOfTiles

        // Assert
        outputDirectory.GetFiles().Should().HaveLength(1)

    [<Fact>]
    member _.``generated tile has correct dimensions`` () =
        // Arrange
        let expectedWidth = 640 // 5 tiles across
        let expectedHeight =  320 //tileHeight + (tilesDown - 1) * (tileHeight / 2)
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

    
