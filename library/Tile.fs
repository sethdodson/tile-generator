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
    let leftSide = boundingBoxTopLeft.X
    let rightSide = boundingBoxTopLeft.X + boundingBoxWidth
    let top = boundingBoxTopLeft.Y    
    let middleWidth = leftSide + (boundingBoxWidth / 2)
    let height = boundingBoxWidth / 2
    let bottom = top - height
    let middleHeight = bottom + (height / 2)
    {
        BoundingBoxTopLeft = boundingBoxTopLeft
        BoundingBoxTopRight = Point(rightSide, top)
        BoundingBoxBottomLeft = Point(leftSide, bottom)
        BoundingBoxBottomRight = Point(rightSide, bottom)
        Top = Point(middleWidth, top)
        Right = Point(rightSide, middleHeight)
        Bottom = Point(middleWidth, bottom)
        Left = Point(leftSide, middleHeight)
    }