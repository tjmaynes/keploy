# Keploy
> An opinionated tool for generating Istio + Kubernetes resource files from a simple json file.

## Requirements

- [.NET 5](https://dotnet.microsoft.com/download/dotnet)

## Usage

First, create a `.keployrc.json` file:
```json
{
  "name": "your-app-name",
  "image_name": "ngnix:alpine",
  "port": 8080
}
```

To generate your app's Kubernetes resources, run the following command:
```bash
keploy generate -f .keployrc.json 
```

## Development

To install project dependencies, run the following commmand:
```bash
make install_dependencies
```

To run project tests, run the following command:
```bash
make test
```
