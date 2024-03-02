module TilesetGeneratorTests

open Faqt
open Xunit
open System.IO
open System.Drawing
open System
open Tile
open TilesetGenerator
open UnitsOfMeasure
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
        let parameters = 
            { 
                SourceDirectory = new DirectoryInfo(sourceDirectoryPath); 
                OutputDirectory = new DirectoryInfo(outputDirectoryPath); 
                NumberOfTiles = 1<tile>; 
                TileWidth = 256<pixel>; 
                TilesPerRow = 1<tile>
            }

        // Act
        generateFromSourceImage parameters

        // Assert
        parameters.OutputDirectory.GetFiles().Should().HaveLength(1)

    [<Fact>]
    member _.``generated tile has correct dimensions`` () =
        // Arrange
        let parameters = 
            { 
                SourceDirectory = new DirectoryInfo(sourceDirectoryPath); 
                OutputDirectory = new DirectoryInfo(outputDirectoryPath); 
                NumberOfTiles = 1<tile>; 
                TileWidth = 256<pixel>; 
                TilesPerRow = 1<tile>
            }
        let expectedWidth = 256
        let expectedHeight =  128

        // Act
        generateFromSourceImage parameters

        // Assert
        let files = parameters.OutputDirectory.GetFiles()
        files.Length.Should().Be(1) |> ignore
        use tilesetImage = Image.FromFile(files.[0].FullName) :?> Bitmap
        tilesetImage.Width.Should().Be(expectedWidth) |> ignore
        tilesetImage.Height.Should().Be(expectedHeight) |> ignore
    
    [<Fact>]
    member _.``generated tileset is not of uniform color`` () =
        // Arrange
        let parameters = 
            { 
                SourceDirectory = new DirectoryInfo(sourceDirectoryPath); 
                OutputDirectory = new DirectoryInfo(outputDirectoryPath); 
                NumberOfTiles = 1<tile>; 
                TileWidth = 256<pixel>; 
                TilesPerRow = 1<tile>
            }

        // Act
        generateFromSourceImage parameters

        // Assert
        let files = parameters.OutputDirectory.GetFiles()
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
        let parameters = 
            { 
                SourceDirectory = new DirectoryInfo(sourceDirectoryPath); 
                OutputDirectory = new DirectoryInfo(outputDirectoryPath); 
                NumberOfTiles = 10<tile>; 
                TileWidth = 256<pixel>; 
                TilesPerRow = 1<tile>
            }
        let tileHeight = 128<pixel>

        // Act
        let tiles = createTiles parameters tileHeight

        // Assert
        tiles.Length.Should().Be(int parameters.NumberOfTiles) |> ignore

    [<Fact>]
    member _.``creating tiles with one row generates tiles with correct dimensions``() =
        // Arrange
        let parameters = 
            { 
                SourceDirectory = new DirectoryInfo(sourceDirectoryPath); 
                OutputDirectory = new DirectoryInfo(outputDirectoryPath); 
                NumberOfTiles = 2<tile>; 
                TileWidth = 256<pixel>; 
                TilesPerRow = 5<tile>
            }
        let tileHeight = 128<pixel>

        let expectedTiles = 
            [[
                { Top = { X = 128<pixel>; Y = 0<pixel> }; Right = { X = 256<pixel>; Y = 64<pixel> }; Bottom = { X = 128<pixel>; Y = 128<pixel> }; Left = { X = 0<pixel>; Y = 64<pixel> }; BoundingBoxTopLeft = { X = 0<pixel>; Y = 0<pixel> }; BoundingBoxTopRight = { X = 256<pixel>; Y = 0<pixel> }; BoundingBoxBottomLeft = { X = 0<pixel>; Y = 128<pixel> }; BoundingBoxBottomRight = { X = 256<pixel>; Y = 128<pixel> } }
                { Top = { X = 384<pixel>; Y = 0<pixel> }; Right = { X = 512<pixel>; Y = 64<pixel> }; Bottom = { X = 384<pixel>; Y = 128<pixel> }; Left = { X = 256<pixel>; Y = 64<pixel> }; BoundingBoxTopLeft = { X = 256<pixel>; Y = 0<pixel> }; BoundingBoxTopRight = { X = 512<pixel>; Y = 0<pixel> }; BoundingBoxBottomLeft = { X = 256<pixel>; Y = 128<pixel> }; BoundingBoxBottomRight = { X = 512<pixel>; Y = 128<pixel> } }
            ]]

        // Act
        let tiles = createTiles parameters tileHeight
        // Assert
        Assert.Equal<Tile list>(expectedTiles, tiles) |> ignore

    [<Fact>]
    member _.``creating tiles with multiple rows generates tiles with the correct dimensions and locations``() =
        // Arrange
        let parameters = 
            { 
                SourceDirectory = new DirectoryInfo(sourceDirectoryPath); 
                OutputDirectory = new DirectoryInfo(outputDirectoryPath); 
                NumberOfTiles = 5<tile>; 
                TileWidth = 256<pixel>; 
                TilesPerRow = 2<tile>
            }
        let tileHeight = 128<pixel>        

        let expectedTiles = 
            [
                [   
                    { Top = { X = 128<pixel>; Y = 0<pixel> }; Right = { X = 256<pixel>; Y = 64<pixel> }; Bottom = { X = 128<pixel>; Y = 128<pixel> }; Left = { X = 0<pixel>; Y = 64<pixel> }; BoundingBoxTopLeft = { X = 0<pixel>; Y = 0<pixel> }; BoundingBoxTopRight = { X = 256<pixel>; Y = 0<pixel> }; BoundingBoxBottomLeft = { X = 0<pixel>; Y = 128<pixel> }; BoundingBoxBottomRight = { X = 256<pixel>; Y = 128<pixel> } }
                    { Top = { X = 384<pixel>; Y = 0<pixel> }; Right = { X = 512<pixel>; Y = 64<pixel> }; Bottom = { X = 384<pixel>; Y = 128<pixel> }; Left = { X = 256<pixel>; Y = 64<pixel> }; BoundingBoxTopLeft = { X = 256<pixel>; Y = 0<pixel> }; BoundingBoxTopRight = { X = 512<pixel>; Y = 0<pixel> }; BoundingBoxBottomLeft = { X = 256<pixel>; Y = 128<pixel> }; BoundingBoxBottomRight = { X = 512<pixel>; Y = 128<pixel> } }
                ]
                [
                    { Top = { X = 128<pixel>; Y = 128<pixel> }; Right = { X = 256<pixel>; Y = 192<pixel> }; Bottom = { X = 128<pixel>; Y = 256<pixel> }; Left = { X = 0<pixel>; Y = 192<pixel> }; BoundingBoxTopLeft = { X = 0<pixel>; Y = 128<pixel> }; BoundingBoxTopRight = { X = 256<pixel>; Y = 128<pixel> }; BoundingBoxBottomLeft = { X = 0<pixel>; Y = 256<pixel> }; BoundingBoxBottomRight = { X = 256<pixel>; Y = 256<pixel> } }
                    { Top = { X = 384<pixel>; Y = 128<pixel> }; Right = { X = 512<pixel>; Y = 192<pixel> }; Bottom = { X = 384<pixel>; Y = 256<pixel> }; Left = { X = 256<pixel>; Y = 192<pixel> }; BoundingBoxTopLeft = { X = 256<pixel>; Y = 128<pixel> }; BoundingBoxTopRight = { X = 512<pixel>; Y = 128<pixel> }; BoundingBoxBottomLeft = { X = 256<pixel>; Y = 256<pixel> }; BoundingBoxBottomRight = { X = 512<pixel>; Y = 256<pixel> } }
                ]
                [
                    { Top = { X = 128<pixel>; Y = 256<pixel> }; Right = { X = 256<pixel>; Y = 320<pixel> }; Bottom = { X = 128<pixel>; Y = 384<pixel> }; Left = { X = 0<pixel>; Y = 320<pixel> }; BoundingBoxTopLeft = { X = 0<pixel>; Y = 256<pixel> }; BoundingBoxTopRight = { X = 256<pixel>; Y = 256<pixel> }; BoundingBoxBottomLeft = { X = 0<pixel>; Y = 384<pixel> }; BoundingBoxBottomRight = { X = 256<pixel>; Y = 384<pixel> } }
                ]
            ]

        // Act
        let tiles = createTiles parameters tileHeight

        // Assert
        Assert.Equal<Tile list>(expectedTiles, tiles) |> ignore