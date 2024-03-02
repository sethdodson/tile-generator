module Tile

open System.Drawing
open System
open UnitsOfMeasure
open Pixel


type Tile = {
    Top: PixelPoint
    Right: PixelPoint
    Bottom: PixelPoint
    Left: PixelPoint
    BoundingBoxTopLeft: PixelPoint
    BoundingBoxTopRight: PixelPoint
    BoundingBoxBottomLeft: PixelPoint
    BoundingBoxBottomRight: PixelPoint
} with 
    member this.DrawBoundingBox(graphics: Graphics) =
        use pen = new Pen(Color.Red, 1.0f)
        graphics.DrawLine(pen, this.BoundingBoxTopLeft.ToPoint(), this.BoundingBoxTopRight.ToPoint())
        graphics.DrawLine(pen, this.BoundingBoxTopRight.ToPoint(), this.BoundingBoxBottomRight.ToPoint())
        graphics.DrawLine(pen, this.BoundingBoxBottomRight.ToPoint(), this.BoundingBoxBottomLeft.ToPoint())
        graphics.DrawLine(pen, this.BoundingBoxBottomLeft.ToPoint(), this.BoundingBoxTopLeft.ToPoint())

    member this.DrawTile(graphics: Graphics) =
        use pen = new Pen(Color.Blue, 1.0f)
        graphics.DrawLine(pen, this.Top.ToPoint(), this.Right.ToPoint())
        graphics.DrawLine(pen, this.Right.ToPoint(), this.Bottom.ToPoint())
        graphics.DrawLine(pen, this.Bottom.ToPoint(), this.Left.ToPoint())
        graphics.DrawLine(pen, this.Left.ToPoint(), this.Top.ToPoint())

let createTile (boundingBoxTopLeft: PixelPoint) (boundingBoxWidth: int<pixel>) : Tile =
    if (boundingBoxWidth < 1<pixel>)
        then raise (new ArgumentException("boundingBoxWidth must be greater than 0 pixels."))

    let leftSide = boundingBoxTopLeft.X 
    let rightSide = boundingBoxTopLeft.X + boundingBoxWidth
    let top = boundingBoxTopLeft.Y    
    let middleWidth = leftSide + (boundingBoxWidth / 2)
    let height = boundingBoxWidth / 2
    let bottom = top + height // the reason this is addition and not subtraction is because Y increases as you go down.
    let middleHeight = top + (height / 2) // see above comment for clarity.
    {
        BoundingBoxTopLeft = boundingBoxTopLeft
        BoundingBoxTopRight = { X = rightSide; Y = top }
        BoundingBoxBottomLeft = { X = leftSide; Y = bottom }
        BoundingBoxBottomRight = { X = rightSide; Y = bottom }
        Top = { X = middleWidth; Y = top }
        Right = { X = rightSide; Y = middleHeight }
        Bottom = { X = middleWidth; Y = bottom }
        Left = { X = leftSide; Y = middleHeight }
    }