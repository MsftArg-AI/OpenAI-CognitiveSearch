Write-Host ""
Write-Host "Checking pre-requisites"
Write-Host ""

$azdVer = (& azd version)

if ($LASTEXITCODE -ne 0 || [string]::IsNullOrEmpty($azdVer)) {
    Write-Host "Failed to load environment variables from azd environment"
    exit $LASTEXITCODE
}

##azd env list -o json

$enviroments = (& azd env list -o json | ConvertFrom-Json)

while ($enviroments.Count -eq 0) {
    Write-Host "No azd environments found, please provide a name:"
    $envName = Read-Host

    Write-Host "please provide a validad Azure Location (eastus, southcentralus, westeurope):"
    $envLocation = Read-Host

    Write-Host "please provide a validad Azure subscription ID or Name:"
    $envSubscription = Read-Host

    azd env new $envName -l $envLocation --subscription $envSubscription
    
    $enviroments = (& azd env list -o json | ConvertFrom-Json)
}

<# if($enviroments.Count -gt 1){
    #list enviroments
    Write-Host "Please select an enviroment o enter for default:"
    $enviroments | ForEach-Object {
        Write-Host "$($_.name) - $($_.location) - $($_.subscription)"
    }
    #select enviroment
    $enviroment = Read-Host
    $enviroment = $enviroments | Where-Object { $_.name -eq $enviroment }
    #set enviroment
    azd env set $enviroment.name    
} #>