module TilesetGeneratorTests

open Faqt
open Xunit
open System.IO
open System
open TilesetGenerator

type TilesetGeneratorTests() =
    let projectDirectoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
    let sourceDirectoryPath = Path.Combine(projectDirectoryPath, "source-images")
    let outputDirectoryPath = Path.Combine(projectDirectoryPath, "tiles")

    interface IDisposable with
        member _.Dispose() = ()


    [<Fact>]
    member _. ``generateFromSourceImage generates a png given valid directories`` () =
        // Arrange
        // get three levels up from the current directory                
        let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)        
        let outputDirectory = new DirectoryInfo(outputDirectoryPath)

        // Act
        generateFromSourceImage sourceDirectory outputDirectory

        // Assert
        outputDirectory.GetFiles().Length.Should().Be(1)