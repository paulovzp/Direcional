# 🏢 Direcional - Teste Técnico

Sistema desenvolvido para o processo seletivo da vaga de Analista / Desenvolvedor Sênior no Grupo Direcional.  
A aplicação consiste em uma API RESTful desenvolvida em .NET 9 para gerenciar clientes, apartamentos, vendas e reservas.

---

## 📌 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/en-us/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/)
- [SQL Server Express (Docker)](https://hub.docker.com/_/microsoft-mssql-server)
- [JWT Authentication](https://jwt.io/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Swagger (OpenAPI)](https://swagger.io/)

---

## 🚀 Como Executar o Projeto

1. **Clone o repositório**

```bash
git clone git@github.com:paulovzp/Direcional.git
```
```bash
cd direcional
```

2. **Execute com Docker Compose para criar apenas o banco**

```bash
docker-compose up -d sqlserver
```

3. **Execute o migration**

```bash
cd src/Direcional.Persistence
```
```bash
dotnet ef database update --project Direcional.Persistence.csproj
```

4. **Execute a API**
```bash
cd src/Direcional.Api
```
```bash
dotnet run
```

## 🚀 Testar o Projeto

1. **Acesse a API via Swagger**
```bash
http://localhost:44330/swagger
```

2. **Criar um corretor**
```bash
post	/api/corretor
{
  "email": "corretor@direcional.com.br",
  "telefone": "+55 38 999997788",
  "nome": "Corretor Direcional"
}
```


## 🚀 Estrutura de Tabelas
