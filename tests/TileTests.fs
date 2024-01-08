
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

    static member Scenarios =
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
        } |> Seq.map(fun x -> [|x|])

type TileTests() =

    [<Theory>]
    [<MemberData("Scenarios", MemberType = typeof<TileTestData>)>]
    member _.``createTile creates a tile with the correct points`` (tileTestData:TileTestData) =
        // Arrange

        // Act
        let tile = createTile tileTestData.BoundingBoxTopLeft tileTestData.BoundingBoxSideLength

        // Assert
        tile.Should().Be(tileTestData.ExpectedTile) |> ignore