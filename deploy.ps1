# Requires a link to a docker machine

function ConnectToDockerMachine()
{
    [CmdletBinding()]
    PARAM(
        [string]$machineName
    )
}

function Compose(){
    [CmdletBinding()]
    PARAM(
        [string]$composeFile,
        [string]$machineName
    )
    write-host "Starting deployment"
    & docker-machine env $machineName | Invoke-Expression
    write-host "Connected to docker machine"
    & docker-compose -f $composeFile up --build -d
    write-host "Finished build"
}