param(
    [Parameter(Mandatory)]
    [System.String]$Version,

    [Parameter(Mandatory)]
    [ValidateSet('Debug','Release','MultiplayerDebug')]
    [System.String]$Target,
    
    [Parameter(Mandatory)]
    [System.String]$TargetPath,
    
    [Parameter(Mandatory)]
    [System.String]$TargetAssembly,
    
    [Parameter(Mandatory)]
    [System.String]$ProjectPath
)
# Make sure Get-Location is the script path
Push-Location -Path (Split-Path -Parent $MyInvocation.MyCommand.Path)

# Test some preliminaries
("$TargetPath",
 "$ProjectPath"
) | % {
    if (!(Test-Path "$_")) {Write-Error -ErrorAction Stop -Message "$_ folder is missing"}
}

# Set some directories
$PluginFolder = "C:\Users\bergm_69gfgdx\AppData\Roaming\r2modmanPlus-local\ROUNDS\profiles\allpatchedup\BepInEx\plugins"
$ReadmeFile = "$ProjectPath\README.md"
$ThunderStoreFolder = "$ProjectPath\ThunderStore"

# Go
Write-Host "Making for $Target from $TargetPath"

# Plugin name without ".dll"
$name = "$TargetAssembly" -Replace('.dll')

# Debug copies the dll to ROUNDS
if ($Target.Equals("Debug")) {
    Write-Host "Updating local installation in r2modman"
    
    # $plug = New-Item -Type Directory -Path "$RoundsPath\BepInEx\plugins\$name" -Force
    Write-Host "Copy $TargetAssembly to $PluginFolder"
    Copy-Item -Path "$TargetPath\$name.dll" -Destination $PluginFolder -Force
}

# Release package for ThunderStore
if($Target.Equals("Release") ) {
    $package = "$ProjectPath\release"

    Write-Host "Packaging for ThunderStore"
    New-Item -Type Directory -Path "$package\Thunderstore" -Force
    $thunder = New-Item -Type Directory -Path "$package\Thunderstore\package"
    $thunder.CreateSubdirectory('plugins')
    Copy-Item -Path "$TargetPath\$name.dll" -Destination "$thunder\plugins\"
    Copy-Item -Path $ReadmeFile -Destination "$thunder\README.md"
    #Copy-Item -Path $LicenseFile -Destination "$thunder\LICENSE"
    #Copy-Item -Path "$ThunderStoreFolder\manifest.json" -Destination "$thunder\manifest.json"
    #Copy-Item -Path "$ThunderStoreFolder\icon.png" -Destination "$thunder\icon.png"

    #((Get-Content -path "$thunder\manifest.json" -Raw) -replace "#VERSION#", "$Version") | Set-Content -Path "$thunder\manifest.json"


    Remove-Item -Path "$package\Thunderstore\$name.$Version.zip" -Force

    Compress-Archive -Path "$thunder\*" -DestinationPath "$package\Thunderstore\$name.$Version.zip"
    $thunder.Delete($true)

#    $arr = Get-ChildItem $ProjectPath\Cards |
#            select FullName
#    foreach($let in $arr) {
#        #$tex = Get-Content -Path $let
#        Write-Host $let
#    }
}

if($Target.Equals("Release")) {
    $package = "$ProjectPath\release"
    Copy-Item -Path "$TargetPath\$name.dll" -Destination "$package\$name.$Version.dll"
}

Pop-Location
