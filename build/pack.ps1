$ErrorActionPreference = "Stop"

$Package = "xAPI.Client"
$baseDir = Resolve-Path "$PSScriptRoot\.."
$solutionDir = "$baseDir\src"
$logDir = "$PSScriptRoot\logs"

Function Main
{
    Start-Transcript "$logDir\publish.log"
    $startingDir = pwd
    cd $solutionDir
    Try
    {
        # Rebuild package project
        dotnet clean "$solutionDir\$Package" --configuration "Release"
        dotnet build "$solutionDir\$Package" --no-incremental --configuration "Release"
        if ($LASTEXITCODE -ne 0) { throw "Could not rebuild package project" }
        
        # Run tests
        dotnet clean "$solutionDir\$Package.Tests" --configuration "Release"
        dotnet build "$solutionDir\$Package.Tests" --no-incremental --configuration "Release"
        if ($LASTEXITCODE -ne 0) { throw "Could not rebuild test project" }
        dotnet test "$solutionDir\$Package.Tests" --configuration "Release"
        if ($LASTEXITCODE -ne 0) { throw "One or more tests failed" }
        
        # Pack nuget
        dotnet pack "$solutionDir\$Package" --configuration "Release" --output "$PSScriptRoot"
        if ($LASTEXITCODE -ne 0) { throw "Could not pack nuget package" }
    }
    Finally
    {
        cd $startingDir
        Stop-Transcript
    }
}

Main
