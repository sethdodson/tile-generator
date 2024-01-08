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
    let middle = boundingBoxSideLength / 2
    // this is the gap between the top of the bounding box and the top of the tile
    let verticalPadding = boundingBoxSideLength / 4    
    {
        BoundingBoxTopLeft = boundingBoxTopLeft
        BoundingBoxTopRight = Point(boundingBoxSideLength, boundingBoxTopLeft.Y)
        BoundingBoxBottomLeft = Point(0, 0)
        BoundingBoxBottomRight = Point(boundingBoxSideLength, 0)
        Top = Point(middle, middle + verticalPadding)
        Right = Point(boundingBoxSideLength, middle)
        Bottom = Point(middle, verticalPadding)
        Left = Point(0, middle)
    }