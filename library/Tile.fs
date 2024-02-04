module Tile

open System.Drawing
open System

type Tile = {
    Top: Point
    Right: Point
    Bottom: Point
    Left: Point
    BoundingBoxTopLeft: Point
    BoundingBoxTopRight: Point
    BoundingBoxBottomLeft: Point
    BoundingBoxBottomRight: Point
} with 
    member this.DrawBoundingBox(graphics: Graphics) =
        use pen = new Pen(Color.Red, 1.0f)
        graphics.DrawLine(pen, this.BoundingBoxTopLeft, this.BoundingBoxTopRight)
        graphics.DrawLine(pen, this.BoundingBoxTopRight, this.BoundingBoxBottomRight)
        graphics.DrawLine(pen, this.BoundingBoxBottomRight, this.BoundingBoxBottomLeft)
        graphics.DrawLine(pen, this.BoundingBoxBottomLeft, this.BoundingBoxTopLeft)

    member this.DrawTile(graphics: Graphics) =
        use pen = new Pen(Color.Blue, 1.0f)
        graphics.DrawLine(pen, this.Top, this.Right)
        graphics.DrawLine(pen, this.Right, this.Bottom)
        graphics.DrawLine(pen, this.Bottom, this.Left)
        graphics.DrawLine(pen, this.Left, this.Top)

let createTile (boundingBoxTopLeft: Point) (boundingBoxWidth: int) : Tile =
    if (boundingBoxWidth < 1)
        then raise (new ArgumentException("boundingBoxWidth must be greater than 0"))
    let leftSide = boundingBoxTopLeft.X
    let rightSide = boundingBoxTopLeft.X + boundingBoxWidth
    let top = boundingBoxTopLeft.Y    
    let middleWidth = leftSide + (boundingBoxWidth / 2)
    let height = boundingBoxWidth / 2
    let bottom = top + height // the reason this is addition and not subtraction is because Y increases as you go down.
    let middleHeight = top + (height / 2) // see above comment for clarity.
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