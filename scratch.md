# Notes

- Consume manifest file
- Transform Manifest DSL into Kubernetes resources yaml

=> App
- Service
- Deployment
- PersistenceVolume + Claim

## Features
Able to generate an initial Manifest (json) file based on project type/template.
```bash
keploy init // defaults to "app-service" template?
keploy init {name} --type=app-service
keploy init {name} --type=static-website
```

Able to generate a yaml file containing K8s resources ready to be deployed:
```bash
keploy generate // defaults to .keployrc.json?
keploy generate --file={name}.json // custom file look up
``` 

Able to list all the different project types available.
```bash
keploy types
```

Able to push changes to an GitOps repo.
```bash
keploy deploy --url {someUrl} ??
```

## Manifast File

// kubectl apply -f ./dir || some-file
// -> https://my-service.some-domain.com/
  // ? => some-cluster

.service.keployrc.json
```json
{
  "name": "some-service",
  "image": "docker.io/ddubson/my-website:0.1.0",
  "port": "8080"
}
```

```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: {name}-deployment
  labels:
    app: {name}
spec:
  replicas: {replicas}
  selector:
    matchLabels:
      app: {name}
  template:
    metadata:
      labels:
        app: {name}
    spec:
      containers:
      - name: {name}
        image: {image}
        ports:
        - containerPort: {port}
```

.database.keployrc.json
```json
{
  "type": "stateful",
  "name": "some-database",
  "image": "docker.io/postgresql/postgres:latest",
  "port": "5678",
}
```


## Event Storming
