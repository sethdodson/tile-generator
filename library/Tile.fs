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

let createTile (boundingBoxTopLeft: Point) (boundingBoxWidth: int) : Tile =
    let middleWidth = boundingBoxWidth / 2
    let height = middleWidth
    let middleHeight = height / 2
    {
        BoundingBoxTopLeft = boundingBoxTopLeft
        BoundingBoxTopRight = Point(boundingBoxWidth, boundingBoxTopLeft.Y)
        BoundingBoxBottomLeft = Point(0, 0)
        BoundingBoxBottomRight = Point(boundingBoxWidth, 0)
        Top = Point(middleWidth, height)
        Right = Point(boundingBoxWidth, middleHeight)
        Bottom = Point(middleWidth, 0)
        Left = Point(0, middleHeight)
    }