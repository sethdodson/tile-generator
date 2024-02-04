
module TileTests

open Faqt
open Xunit
open System.Drawing
open Tile
open System

// TileTestData is a type used to represent different scenarios for the Theory below.
// It contains the expected Tile result and the parameters to create a Tile.
// This allows us to easily add new scenarios to the Theory test by creating new instances of TileTestData.
type TileTestData = 
    {
        ExpectedTile: Tile
        BoundingBoxTopLeft: Point
        BoundingBoxWidth: int
    }

    static member Scenarios =
        // Tile is the first tile in the upper left corner.
        // In coordinates used by graphics libraries, 0,0 is the upper left corner.
        // y values increase as you go down, x values increase as you go right.
        // This is not what you expect from a cartesian plane.
        let firstTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(0, 0)
                BoundingBoxTopRight = Point(256, 0)
                BoundingBoxBottomLeft = Point(0, 128)
                BoundingBoxBottomRight = Point(256, 128)
                Top = Point(128, 0)
                Right = Point(256, 64)
                Bottom = Point(128, 128)
                Left = Point(0, 64)
            }
            BoundingBoxTopLeft = Point(0, 0)
            BoundingBoxWidth = 256
        }
        // Tile with a different size.
        let biggerTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(0, 0)
                BoundingBoxTopRight = Point(512, 0)
                BoundingBoxBottomLeft = Point(0, 256)
                BoundingBoxBottomRight = Point(512, 256)
                Top = Point(256, 0)
                Right = Point(512, 128)
                Bottom = Point(256, 256)
                Left = Point(0, 128)
            }
            BoundingBoxTopLeft = Point(0, 0)
            BoundingBoxWidth = 512
        }
        // Tile is close to the middle of a 1024x1024 tileset. 4 tiles across, 8 tiles down.
        let middleTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(256, 512)
                BoundingBoxTopRight = Point(512, 512)
                BoundingBoxBottomLeft = Point(256, 640)
                BoundingBoxBottomRight = Point(512, 640)
                Top = Point(384, 512)
                Right = Point(512, 576)
                Bottom = Point(384, 640)
                Left = Point(256, 576)
            }
            BoundingBoxTopLeft = Point(256, 512)
            BoundingBoxWidth = 256
        }

        // Tile is the last tile in the bottom right corner of a 1024x1024 tileset.
        let bottomRightTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(768, 896)
                BoundingBoxTopRight = Point(1024, 896)
                BoundingBoxBottomLeft = Point(768, 1024)
                BoundingBoxBottomRight = Point(1024, 1024)
                Top = Point(896, 896)
                Right = Point(1024, 960)
                Bottom = Point(896, 1024)
                Left = Point(768, 960)
            }
            BoundingBoxTopLeft = Point(768, 896)
            BoundingBoxWidth = 256
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
        let boundingBoxTopLeft = Point(0, 0)
        let boundingBoxWidth = -1

        // Act
        let createTileWithNegativeWidth = fun () -> createTile boundingBoxTopLeft boundingBoxWidth

        // Assert
        createTileWithNegativeWidth.Should().Throw<ArgumentException, _>()
