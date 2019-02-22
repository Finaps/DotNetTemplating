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
```

If `<projectName>` is left empty it will take the folder name.

Options:

```
-M  add mongo
-H  add HTTP
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

A service will need a database (maybe). To facilitate this we recommend using Helm with a stable repo channel to manage database and their setup. When you prepare your repo with `azds` you will get a charts folder. We recommend to add a second folder to the charts folder which will hold the `values.yaml` file for setup. To setup a mongodb instance we run the following command:

```shell
helm install -n <servicename>-database(-dev) stable/mongodb -f charts/<servicename>-database/values.yaml (--namespace=<development-namepsace>)
```
