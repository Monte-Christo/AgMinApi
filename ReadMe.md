To update the deployed version (for instance with ArgoCD, change

- docker.yml
- deployment.yaml


To access the ArgoCD dashboard, map the port:

```
kubectl port-forward svc/argocd-server -n argocd 8080:443
```