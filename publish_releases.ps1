$ErrorActionPreference = "Stop"

# Set location to script directory
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptDir

# Paths
$projectPath = "SpaceHaven Save Editor"
$outputPath = "Releases"

# Create output folder
if (Test-Path $outputPath) {
    Remove-Item -Path $outputPath -Recurse -Force
}
New-Item -ItemType Directory -Path $outputPath | Out-Null

Write-Host "🚀 Starting Build and Publish Process..." -ForegroundColor Cyan

# 1. Windows Self-Contained (Standalone)
Write-Host "📦 Publishing Windows x64 (Self-Contained)..." -ForegroundColor Yellow
dotnet publish "$projectPath" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -o "$outputPath/win-x64-standalone"

# 2. Windows Framework-Dependent (Lightweight / Wine)
Write-Host "📦 Publishing Windows x64 (Framework-Dependent)..." -ForegroundColor Yellow
dotnet publish "$projectPath" -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true -o "$outputPath/win-x64-portable"

# 3. Linux x64 Self-Contained
Write-Host "📦 Publishing Linux x64 (Self-Contained)..." -ForegroundColor Yellow
dotnet publish "$projectPath" -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -o "$outputPath/linux-x64"

# Packaging
Write-Host "🤐 Packaging ZIP Archives..." -ForegroundColor Cyan

# Zip Windows Standalone
Compress-Archive -Path "$outputPath/win-x64-standalone/*" -DestinationPath "$outputPath/SpaceHaven-Save-Editor-Windows-x64-Standalone.zip" -Force

# Zip Windows Portable (also for macOS via Wine)
Compress-Archive -Path "$outputPath/win-x64-portable/*" -DestinationPath "$outputPath/SpaceHaven-Save-Editor-Windows-x64-Portable.zip" -Force
Compress-Archive -Path "$outputPath/win-x64-portable/*" -DestinationPath "$outputPath/SpaceHaven-Save-Editor-macOS-Wine.zip" -Force

# Tar/Zip Linux
Compress-Archive -Path "$outputPath/linux-x64/*" -DestinationPath "$outputPath/SpaceHaven-Save-Editor-Linux-x64.zip" -Force

# Clean temporary folders
Remove-Item -Path "$outputPath/win-x64-standalone" -Recurse -Force
Remove-Item -Path "$outputPath/win-x64-portable" -Recurse -Force
Remove-Item -Path "$outputPath/linux-x64" -Recurse -Force

Write-Host "🎉 All releases published and packaged successfully in '$outputPath/' folder!" -ForegroundColor Green
