function DODeploy()
{
    [CmdletBinding()]
    PARAM(
        [string]$DOTOKEN,
        [string]$name
    )

    docker-machine create --driver digitalocean --digitalocean-size s-1vcpu-1gb -digitalocean-region lon1 --digitalocean-access-token $DOTOKEN $name
}