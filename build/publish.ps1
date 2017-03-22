$ErrorActionPreference = "Stop"
$baseDir = Resolve-Path "$PSScriptRoot\.."

$buildDirPath = $PSScriptRoot
$outputDir = "$buildDirPath\output"

$solutionDirPath = "$baseDir\src"
$solutionFilePath = "$solutionDirPath\xAPI.Client.sln"

$nugetDirPath = "$baseDir\tools\NuGet"
$nugetFilePath = "$nugetDirPath\nuget.exe"
$nugetDownloadUrl = "http://dist.nuget.org/win-x86-commandline/latest/nuget.exe"

Function Main
{
    Try
    {
        Start-Transcript "$outputDir\publish.log"
        Ensure-NuGetExists
        $nugetPackagePath = Get-NuGetPackagePath
        $nugetPublishUrl = Prompt-NuGetPublishUrl
        $nugetApiKey = Prompt-NuGetApiKey
        Publish-NuGetPackage -Package $nugetPackagePath -Source $nugetPublishUrl -ApiKey $nugetApiKey
    }
    Finally
    {
        Stop-Transcript
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

function Get-NuGetPackagePath
{
    $files = Get-ChildItem -Path "$outputDir\*.nupkg"
    if ($files.Count -ne 1)
    {
        Throw "Expected exactly 1 .nupkg file in output directory, found $($files.Count) instead"
    }
    
    return $files[0].FullName
}

function Prompt-NuGetPublishUrl
{
    do
    {
        $publishUrl = Read-Host -Prompt "NuGet publish URL: "
    } while ([string]::IsNullOrEmpty($publishUrl))
    
    return $publishUrl
}

function Prompt-NuGetApiKey
{
    do
    {
        $secure = Read-Host -Prompt "NuGet API key: " -AsSecureString
    } while ([string]::IsNullOrEmpty($secure))
    
    return [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure))
}

function Publish-NuGetPackage
{
    param (
        [Parameter(Mandatory=$true)] [string] $Package,
        [Parameter(Mandatory=$true)] [string] $Source,
        [Parameter(Mandatory=$true)] [string] $ApiKey
    )
    
    Write-Host
    Write-Host "Pushing NuGet package" -ForegroundColor Green
    exec { & $nugetFilePath push "$Package" -source "$Source" -apikey "$ApiKey" | Out-Default } "Error pushing NuGet package"
    Write-Host "Success!" -ForegroundColor Green
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
