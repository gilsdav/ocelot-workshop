![GraphQl](https://fullstackmark.com/img/posts/17/rest-vs-graphql.png)

## Create DB

```sh
docker run -d -e SA_PASSWORD=PizzaGraphQL123+ -e SQLSERVER_DATABASE=PizzaGraphQL -e SQLSERVER_USER=PizzaGraphQL -e SQLSERVER_PASSWORD=PizzaGraphQL123+ -p 1433:1433 --name PizzaGraphSQL exoplatform/sqlserver:2017-CU8
```
ou
```
docker-compose up -d
```


```
dotnet tool install --global dotnet-ef
```

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

```
dotnet watch run
```
