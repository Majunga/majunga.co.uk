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

    docker-machine env $machineName | Invoke-Expression

    docker-compose -f $composeFile up --build -d
}