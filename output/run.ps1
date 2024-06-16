# PowerShell script to generate PlantUML script based on XML files in a directory

$directoryPath = ".\xml"
$outputScript = "generated_script.puml"

# Get all XML files in the specified directory
$xmlFiles = Get-ChildItem -Path $directoryPath -Filter *.xml

# Generate PlantUML script
$scriptContent = @"
@startuml
!define CLASS diagram
!define MEMBERS diagram
!define FIELDS diagram
!define METHODS diagram

"@

# Add include statements for each XML file
foreach ($xmlFile in $xmlFiles) {
    $scriptContent += "`r`nfile `"$($xmlFile.FullName)`""
}

# Add closing statement to the PlantUML script
$scriptContent += "`r`n`r`n@enduml"

# Write content to the output script
$scriptContent | Out-File -FilePath $outputScript -Force -Encoding utf8
