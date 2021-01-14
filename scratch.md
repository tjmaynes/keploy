# Notes

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

```json
{
  "type": "some-available-type",
  "name": "some-service",
  "port": "3000"
}
```

## Event Storming 
