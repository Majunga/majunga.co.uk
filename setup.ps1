$currentLocation = Get-Location
Set-Location "c:\"
minikube start --vm-driver=hyperv --hyperv-virtual-switch "Default Switch" --memory=4096
Set-Location $currentLocation

& minikube docker-env | Invoke-Expression
docker build --rm -f ".\src\Backend\Dockerfile" -t backend:2 .\src\Backend
docker build --rm -f ".\src\Frontend\Dockerfile" -t frontend:1 .\src\Frontend

kubectl create -f .\deployment.yml