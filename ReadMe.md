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
