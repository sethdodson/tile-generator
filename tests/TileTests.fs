
module TileTests

open Faqt
open Xunit
open Tile
open System
open UnitsOfMeasure
open Pixel

// TileTestData is a type used to represent different scenarios for the Theory below.
// It contains the expected Tile result and the parameters to create a Tile.
// This allows us to easily add new scenarios to the Theory test by creating new instances of TileTestData.
type TileTestData = 
    {
        ExpectedTile: Tile
        BoundingBoxTopLeft: PixelPoint
        BoundingBoxWidth: int<pixel>
    }

    static member Scenarios =
        // Tile is the first tile in the upper left corner.
        // In coordinates used by graphics libraries, 0,0 is the upper left corner.
        // y values increase as you go down, x values increase as you go right.
        // This is not what you expect from a cartesian plane.
        let firstTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = { X = 0<pixel>; Y = 0<pixel> }
                BoundingBoxTopRight = { X = 256<pixel>; Y = 0<pixel> }
                BoundingBoxBottomLeft = { X = 0<pixel>; Y = 128<pixel> }
                BoundingBoxBottomRight = { X = 256<pixel>; Y = 128<pixel> }
                Top = { X = 128<pixel>; Y = 0<pixel> }
                Right = { X = 256<pixel>; Y = 64<pixel> }
                Bottom = { X = 128<pixel>; Y = 128<pixel> }
                Left = { X = 0<pixel>; Y = 64<pixel> }
            }
            BoundingBoxTopLeft = { X = 0<pixel>; Y = 0<pixel> }
            BoundingBoxWidth = 256<pixel>
        }
        // Tile with a different size.
        let biggerTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = { X = 0<pixel>; Y = 0<pixel> }
                BoundingBoxTopRight = { X = 512<pixel>; Y = 0<pixel> }
                BoundingBoxBottomLeft = { X = 0<pixel>; Y = 256<pixel> }
                BoundingBoxBottomRight = { X = 512<pixel>; Y = 256<pixel> }
                Top = { X = 256<pixel>; Y = 0<pixel> }
                Right = { X = 512<pixel>; Y = 128<pixel> }
                Bottom = { X = 256<pixel>; Y = 256<pixel> }
                Left = { X = 0<pixel>; Y = 128<pixel> }
            }
            BoundingBoxTopLeft = { X = 0<pixel>; Y = 0<pixel> }
            BoundingBoxWidth = 512<pixel>
            }
        // Tile is close to the middle of a 1024x1024 tileset. 4 tiles across, 8 tiles down.
        let middleTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = { X = 256<pixel>; Y = 512<pixel> }
                BoundingBoxTopRight = { X = 512<pixel>; Y = 512<pixel> }
                BoundingBoxBottomLeft = { X = 256<pixel>; Y = 640<pixel> }
                BoundingBoxBottomRight = { X = 512<pixel>; Y = 640<pixel> }
                Top = { X = 384<pixel>; Y = 512<pixel> }
                Right = { X = 512<pixel>; Y = 576<pixel> }
                Bottom = { X = 384<pixel>; Y = 640<pixel> }
                Left = { X = 256<pixel>; Y = 576<pixel> }
            }
            BoundingBoxTopLeft = { X = 256<pixel>; Y = 512<pixel> }
            BoundingBoxWidth = 256<pixel>
        }

        // Tile is the last tile in the bottom right corner of a 1024x1024 tileset.
        let bottomRightTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = { X = 768<pixel>; Y = 896<pixel> }
                BoundingBoxTopRight = { X = 1024<pixel>; Y = 896<pixel> }
                BoundingBoxBottomLeft = { X = 768<pixel>; Y = 1024<pixel> }
                BoundingBoxBottomRight = { X = 1024<pixel>; Y = 1024<pixel> }
                Top = { X = 896<pixel>; Y = 896<pixel> }
                Right = { X = 1024<pixel>; Y = 960<pixel> }
                Bottom = { X = 896<pixel>; Y = 1024<pixel> }
                Left = { X = 768<pixel>; Y = 960<pixel> }
            }
            BoundingBoxTopLeft = { X = 768<pixel>; Y = 896<pixel> }
            BoundingBoxWidth = 256<pixel>
        }
        // We need to return IEnumerable<object[]> for MemberDataAttribute.
        [|firstTile; biggerTile; middleTile; bottomRightTile|] |> Seq.map(fun x -> [|x|])

type TileTests() =

    [<Theory>]
    [<MemberData("Scenarios", MemberType = typeof<TileTestData>)>]
    member _.``createTile creates a tile with the correct points`` (tileTestData:TileTestData) =
        // Arrange

        // Act
        let tile = createTile tileTestData.BoundingBoxTopLeft tileTestData.BoundingBoxWidth

        // Assert
        tile.BoundingBoxTopLeft.Should().Be(tileTestData.ExpectedTile.BoundingBoxTopLeft) |> ignore
        tile.BoundingBoxTopRight.Should().Be(tileTestData.ExpectedTile.BoundingBoxTopRight) |> ignore
        tile.BoundingBoxBottomLeft.Should().Be(tileTestData.ExpectedTile.BoundingBoxBottomLeft) |> ignore
        tile.BoundingBoxBottomRight.Should().Be(tileTestData.ExpectedTile.BoundingBoxBottomRight) |> ignore
        tile.Top.Should().Be(tileTestData.ExpectedTile.Top) |> ignore
        tile.Right.Should().Be(tileTestData.ExpectedTile.Right) |> ignore
        tile.Bottom.Should().Be(tileTestData.ExpectedTile.Bottom) |> ignore
        tile.Left.Should().Be(tileTestData.ExpectedTile.Left) |> ignore        
        tile.Should().Be(tileTestData.ExpectedTile) |> ignore

    [<Fact>]
    member _.``Creating a tile with negative width throws an exception`` () =
        // Arrange
        let boundingBoxTopLeft = { X = 0<pixel>; Y = 0<pixel> }
        let boundingBoxWidth = -1<pixel>

        // Act
        let createTileWithNegativeWidth = fun () -> createTile boundingBoxTopLeft boundingBoxWidth

        // Assert
        createTileWithNegativeWidth.Should().Throw<ArgumentException, _>()
