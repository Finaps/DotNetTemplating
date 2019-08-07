# Finaps DOTNET Microservice Template

## Requirements:

`dotnet cli`

### Installation of Template

To install the template localy:

```sh
$ cd <path/to/folder>
$ dotnet new -i .
```

### Usage

```sh
$ dotnet new finapsms -n <ProjectName> [OPTIONS]

$ dotnet new finapsms -U --force          (Performs and update)
```

If `<projectName>` is left empty it will take the folder name.

Options:

```
-M  add mongo
-H  add HTTP
-U  Update existing project with new template
```

## Extras

### Azure Developer Spaces

This template can be used with Azure Developers Spaces for Azure Kuberenetes Services (AKS). To prepare the project for this you need to first install devspaces locally:

```shell
az aks use-dev-spaces --resource-group debtco-staging --name debtco-staging --verbose
```

Then you run

```shell
azds prep (--public)
```

The option public flag will enable ingresses

One of the benefits of using `azds` is that we get free helm resources. Helm is the tooling we use to deploy and manage our kubernetes services in combination with `kubectl`.

### MongoDB

A service will need a database (maybe). To create a database for a new service run the commands lined out in the debtco teams channel wiki. Also copy the snippets from the K8sResources/charts.yaml file into the correct charts.
