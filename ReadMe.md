# Hints

To push a new image to DockerHub,
change the env var IMAGE_TAG in

- docker.yml

To update the deployed version (for instance with ArgoCD), change the image tag in

- deployment.yaml

To access the ArgoCD dashboard, map the port:

```PowerShell
kubectl port-forward svc/argocd-server -n argocd 8080:443
```
## deploy ACR

az group create --name myContainerRegRG --location centralus

az deployment group create --resource-group myContainerRegRG --template-file aca.bicep --parameters acrName={your-unique-name}