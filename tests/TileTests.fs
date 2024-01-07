
module TileTests

open Faqt
open Xunit
open System.Drawing
open Tile

type TileTestData = 
    {
        ExpectedTile: Tile
        BoundingBoxTopLeft: Point
        BoundingBoxSideLength: int
    }

    static member Data =
        seq {
            yield { 
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
        }

type TileTests() =



    [<Theory>]
    [<MemberData("Data", MemberType = typeof<TileTestData>)>]
    member _.``createTile creates a tile with the correct points`` (tileTestData:TileTestData) =
        // Arrange
        // let boundingBoxTopLeft = Point(0, 256)
        // let boundingBoxSideLength = 256
        // let expectedTop = new Point(128, 192)
        // let expectedRight = new Point(256, 128)
        // let expectedBottom = new Point(128, 64)
        // let expectedLeft = new Point(0, 128)
        // let expectedBoundingBoxTopLeft = Point(0, 256)
        // let expectedBoundingBoxTopRight = Point(256, 256)
        // let expectedBoundingBoxBottomLeft = Point(0, 0)
        // let expectedBoundingBoxBottomRight = Point(256, 0)

        // Act
        let tile = createTile tileTestData.BoundingBoxTopLeft tileTestData.BoundingBoxSideLength

        // Assert
        tile.Should().Be(tileTestData.ExpectedTile) |> ignore
        // tile.Top.Should().Be(tileTestData.ExpectedTile.Top) |> ignore
        // tile.Right.Should().Be(tileTestData.ExpectedTile.Right) |> ignore
        // tile.Bottom.Should().Be(tileTestData.ExpectedTile.Bottom) |> ignore
        // tile.Left.Should().Be(expectedLeft) |> ignore
        // tile.BoundingBoxTopLeft.Should().Be(expectedBoundingBoxTopLeft) |> ignore
        // tile.BoundingBoxTopRight.Should().Be(expectedBoundingBoxTopRight) |> ignore
        // tile.BoundingBoxBottomLeft.Should().Be(expectedBoundingBoxBottomLeft) |> ignore
        // tile.BoundingBoxBottomRight.Should().Be(expectedBoundingBoxBottomRight) |> ignore
