
module TileTests

open Faqt
open Xunit
open System.Drawing
open Tile

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
        let firstTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(0, 128)
                BoundingBoxTopRight = Point(256, 128)
                BoundingBoxBottomLeft = Point(0, 0)
                BoundingBoxBottomRight = Point(256, 0)
                Top = Point(128, 128)
                Right = Point(256, 64)
                Bottom = Point(128, 0)
                Left = Point(0, 64)
            }
            BoundingBoxTopLeft = Point(0, 128)
            BoundingBoxWidth = 256
        }
        // Tile with a different size.
        let biggerTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(0, 256)
                BoundingBoxTopRight = Point(512, 256)
                BoundingBoxBottomLeft = Point(0, 0)
                BoundingBoxBottomRight = Point(512, 0)
                Top = Point(256, 256)
                Right = Point(512, 128)
                Bottom = Point(256, 0)
                Left = Point(0, 128)
            }
            BoundingBoxTopLeft = Point(0, 256)
            BoundingBoxWidth = 512
        }
        // Tile is close to the middle of a 1024x1024 map. 4 tiles across, 8 tiles down.
        let middleTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(512, 640)
                BoundingBoxTopRight = Point(768, 640)
                BoundingBoxBottomLeft = Point(512, 512)
                BoundingBoxBottomRight = Point(768, 512)
                Top = Point(640, 640)
                Right = Point(768, 576)
                Bottom = Point(640, 512)
                Left = Point(512, 576)
            }
            BoundingBoxTopLeft = Point(512, 640)
            BoundingBoxWidth = 256
        }
        // We need to return IEnumerable<object[]> for MemberDataAttribute.
        [|firstTile; biggerTile; middleTile|] |> Seq.map(fun x -> [|x|])

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
