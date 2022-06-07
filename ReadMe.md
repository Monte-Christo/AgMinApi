# Hints

To update the deployed version (for instance with ArgoCD), change the image tag in

- deployment.yaml

To access the ArgoCD dashboard, map the port:

```PowerShell
kubectl port-forward svc/argocd-server -n argocd 8080:443
```
## deploy ACR

az group create --name EKContainerRegRG --location westeurope

az deployment group create --resource-group EKContainerRegRG --template-file acr.bicep --parameters acrName=ekcontainerreg

See also:
https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.app/container-app-azurevote