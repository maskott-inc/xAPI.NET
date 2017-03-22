$ErrorActionPreference = "Stop"
$baseDir = Resolve-Path "$PSScriptRoot\.."

$buildDirPath = $PSScriptRoot
$outputDir = "$buildDirPath\output"

$solutionDirPath = "$baseDir\src"
$solutionFilePath = "$solutionDirPath\xAPI.Client.sln"

$nugetDirPath = "$baseDir\tools\NuGet"
$nugetFilePath = "$nugetDirPath\nuget.exe"
$nugetDownloadUrl = "http://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$nugetPackageId = "xAPI.Client"
$nuspecFilePath = "$buildDirPath\$nugetPackageId.nuspec"
$nuspecOutFilePath = "$outputDir\$nugetPackageId.nuspec"

$nunitDirPath = "$solutionDirPath\packages\NUnit.ConsoleRunner.3.6.1\tools"
$nunitFilePath = "$nunitDirPath\nunit3-console.exe"
$testAssemblyPath = "$solutionDirPath\xAPI.Client.Tests\bin\Release\xAPI.Client.Tests.dll"
$targetFramework = "net-4.0"

Function Main
{
    Try
    {
        Prepare-OutputDir
        Start-Transcript "$outputDir\build.log"
        Ensure-NuGetExists
        $NuGetVersion = Prompt-NuGetVersion
        Rebuild-Solution
        Run-NUnitTests
        Build-NuGetPackage -NuGetVersion $NuGetVersion
    }
    Finally
    {
        Stop-Transcript
    }
}

function Prepare-OutputDir
{
    if (!(Test-Path "$outputDir"))
    {
        New-Item -ItemType Directory -Force -Path $outputDir
    }
    else
    {
        Remove-Item -Path "$outputDir\*"
    }
}

function Ensure-NuGetExists
{
    if (!(Test-Path "$nugetFilePath"))
    {
        if (!(Test-Path "$nugetFilePath"))
        {
            New-Item -ItemType Directory -Force -Path $nugetDirPath
        }
        Write-Host "Couldn't find nuget.exe. Downloading from $nugetDownloadUrl to $nugetDirPath"
        (New-Object System.Net.WebClient).DownloadFile($nugetDownloadUrl, "$nugetFilePath")
    }
}

function Prompt-NuGetVersion
{
    do
    {
        $version = Read-Host -Prompt "NuGet package new version: "
    } while ([string]::IsNullOrEmpty($version))
    
    return $version
}

function Rebuild-Solution
{
    Write-Host
    Write-Host "Restoring solution" -ForegroundColor Green
    [Environment]::SetEnvironmentVariable("EnableNuGetPackageRestore", "true", "Process")
    exec { & $nugetFilePath update -self }
    exec { & $nugetFilePath restore "$solutionFilePath" -verbosity detailed -configfile "$solutionDirPath\nuget.config" | Out-Default } "Error restoring solution"
    Write-Host "Success!" -ForegroundColor Green

    Write-Host
    Write-Host "Building solution" -ForegroundColor Green
    exec { & MSBuild "/t:Clean;Rebuild" "/p:Configuration=Release" "/p:Platform=Any CPU" "/p:PlatformTarget=AnyCPU" "/p:TreatWarningsAsErrors=true" "$solutionFilePath" | Out-Default } "Error building solution"
    Write-Host "Success!" -ForegroundColor Green
}

function Run-NUnitTests
{
    Write-Host
    Write-Host -ForegroundColor Green "Running NUnit tests"
    exec { & $nunitFilePath "$testAssemblyPath" --framework="$targetFramework" --result="$outputDir\$nugetPackageId.xml" | Out-Default } "Error running NUnit tests"
    Write-Host "Success!" -ForegroundColor Green
}

function Build-NuGetPackage
{
    param (
        [Parameter(Mandatory=$true)] [string] $NuGetVersion
    )
    
    Write-Host
    Write-Host -ForegroundColor Green "Update nuspec package version"
    $xml = [xml](Get-Content $nuspecFilePath)
    Edit-XmlNodes -doc $xml -xpath "//*[local-name() = 'version']" -value $NuGetVersion
    $xml.save($nuspecOutFilePath)
    Write-Host "Success!" -ForegroundColor Green

    Write-Host
    Write-Host -ForegroundColor Green "Building NuGet package " $nugetPackageId
    exec { & $nugetFilePath pack "$nuspecOutFilePath" -outputdirectory "$outputDir" | Out-Default } "Error building NuGet package $nugetPackageId"
    Write-Host "Success!" -ForegroundColor Green
}

function Edit-XmlNodes
{
    param (
        [xml] $doc,
        [Parameter(Mandatory=$true)] [string] $xpath,
        [Parameter(Mandatory=$true)] [string] $value
    )
    
    $nodes = $doc.SelectNodes($xpath)
    $count = $nodes.Count

    Write-Host "Found $count nodes with path '$xpath'"
    
    foreach ($node in $nodes) {
        if ($node -ne $null) {
            if ($node.NodeType -eq "Element")
            {
                $node.InnerXml = $value
            }
            else
            {
                $node.Value = $value
            }
        }
    }
}

function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ("Invalid command: $cmd")
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

Main
