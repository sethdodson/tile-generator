module TilesetGeneratorTests

open Faqt
open Xunit
open System.IO
open System
open TilesetGenerator

type TilesetGeneratorTests() =

    [<Fact>]
    member _. ``generateFromSourceImage generates a png given valid directories`` () =
        // Arrange
        // get three levels up from the current directory
        let projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
        let sourceDirectoryPath = Path.Combine(projectDirectory, "source-images")
        let sourceDirectory = new DirectoryInfo(sourceDirectoryPath)
        let outputDirectoryPath = Path.Combine(projectDirectory, "tiles")  
        let outputDirectory = new DirectoryInfo(outputDirectoryPath)

        // Act
        generateFromSourceImage sourceDirectory outputDirectory

        // Assert
        outputDirectory.GetFiles().Length.Should().Be(1)