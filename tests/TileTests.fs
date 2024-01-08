
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
        BoundingBoxSideLength: int
    }

    static member Scenarios =
        // Tile is the first tile in the upper left corner.
        let firstTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(0, 256)
                BoundingBoxTopRight = Point(256, 256)
                BoundingBoxBottomLeft = Point(0, 0)
                BoundingBoxBottomRight = Point(256, 0)
                Top = Point(128, 192)
                Right = Point(256, 128)
                Bottom = Point(128, 64)
                Left = Point(0, 128)
            }
            BoundingBoxTopLeft = Point(0, 256)
            BoundingBoxSideLength = 256
        }
        // Tile with a different size.
        let biggerTile = {
            ExpectedTile = {
                BoundingBoxTopLeft = Point(0, 512)
                BoundingBoxTopRight = Point(512, 512)
                BoundingBoxBottomLeft = Point(0, 0)
                BoundingBoxBottomRight = Point(512, 0)
                Top = Point(256, 384)
                Right = Point(512, 256)
                Bottom = Point(256, 128)
                Left = Point(0, 256)
            }
            BoundingBoxTopLeft = Point(0, 512)
            BoundingBoxSideLength = 512
        }
        // We need to return IEnumerable<object[]> for MemberDataAttribute.
        [|firstTile; biggerTile|] |> Seq.map(fun x -> [|x|])

type TileTests() =

    [<Theory>]
    [<MemberData("Scenarios", MemberType = typeof<TileTestData>)>]
    member _.``createTile creates a tile with the correct points`` (tileTestData:TileTestData) =
        // Arrange

        // Act
        let tile = createTile tileTestData.BoundingBoxTopLeft tileTestData.BoundingBoxSideLength

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