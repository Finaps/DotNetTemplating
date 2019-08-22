# ASP.NET API Templates

# Prerequisites 
.NET Core command-line (CLI) tools


# Installation of Template

To install the template localy:

```sh
$ dotnet new -i Finaps.Templates.AspNetCore
```

# Usage

```sh
$ dotnet new <short-name> -n <ProjectName> [OPTIONS]
```

## Templates included: 
  - **apiservice**: Microservice template designed for containerized services 

## Azure Developer Spaces

This template can be used with Azure Developers Spaces for Azure Kuberenetes Services (AKS). To prepare the project for this you need to first install devspaces locally:

```shell
az aks use-dev-spaces --resource-group debtco-staging --name debtco-staging --verbose
```

After ins

```shell
azds prep (--public)
```

The option public flag will enable ingresses

One of the benefits of using `azds` is that we get free helm resources. Helm is the tooling we use to deploy and manage our kubernetes services in combination with `kubectl`.
