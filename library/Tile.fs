module Tile

open System.Drawing

type Tile = {
    Top: Point
    Right: Point
    Bottom: Point
    Left: Point
    BoundingBoxTopLeft: Point
    BoundingBoxTopRight: Point
    BoundingBoxBottomLeft: Point
    BoundingBoxBottomRight: Point
}

let createTile (boundingBoxTopLeft: Point) (boundingBoxSideLength: int) : Tile =
    {
        BoundingBoxTopLeft = boundingBoxTopLeft
        BoundingBoxTopRight = Point(256, 256)
        BoundingBoxBottomLeft = Point(0, 0)
        BoundingBoxBottomRight = Point(256, 0)
        Top = Point(128, 192)
        Right = Point(256, 128)
        Bottom = Point(128, 64)
        Left = Point(0, 128)
    }
    // {
    //     BoundingBoxTopLeft = boundingBoxTopLeft
    //     BoundingBoxTopRight = Point(boundingBoxTopLeft.X + boundingBoxSideLength, boundingBoxTopLeft.Y)
    //     BoundingBoxBottomLeft = Point(boundingBoxTopLeft.X, boundingBoxTopLeft.Y + boundingBoxSideLength)
    //     BoundingBoxBottomRight = Point(boundingBoxTopLeft.X + boundingBoxSideLength, boundingBoxTopLeft.Y + boundingBoxSideLength)
    //     Top = Point(boundingBoxSideLength / 2, boundingBoxTopLeft.Y)
    //     Right = Point(boundingBoxSideLength, boundingBoxTopLeft.Y + boundingBoxSideLength / 2)
    //     Bottom = Point(boundingBoxTopLeft.X + boundingBoxSideLength / 2, boundingBoxTopLeft.Y + boundingBoxSideLength)
    //     Left = Point(boundingBoxTopLeft.X, boundingBoxTopLeft.Y + boundingBoxSideLength / 2)
    // }