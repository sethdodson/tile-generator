module Pixel

open UnitsOfMeasure
open System.Drawing

// Wrapper for System.Drawing.Point that uses the pixel unit of measure
type PixelPoint = {
    X: int<pixel>
    Y: int<pixel>
} with
    member this.ToPoint() = Point(int this.X, int this.Y)