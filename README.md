# 🛍️ WakeCommerce

WakeCommerce é um projeto **modular** desenvolvido em **.NET**, utilizando princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**.  
O objetivo é fornecer uma base escalável para serviços de catálogo de produtos, com APIs bem definidas, testes automatizados e suporte a containerização.

---

## ✨ Funcionalidades

- [x] **Cadastrar produtos:** Permite a insersão de produtos parâmetros.
- [x] **Consultar produto por id:** Permite consultar produto pelo seu identificador.
- [X] **Consultar produtos por filtro:** Permite consultar pelo nome ou parte dele, ordenando por qualquer campo e com paginação do resultado.
- [X] **Alterar produto por id:** Permite alterar os dados de um produto.
- [X] **Deletar produto por id:** Permite eliminar um registro de produto.

---

## ➡️ Como Testar
Executando a apliacação no visual studio 2022 com Docker Desktop selecionar a compilação com docker-compose, ele cria um container com a API e o banco de dados.
E a UI de teste do Swagger pode ser acessado pela URL https://localhost:5001/swagger/index.html

O bando de dados possui 5 itens pré cadastrados para testes rápidos.
```  
id										name	   	quantity	price	created_at				updated_at
00000000-0000-0000-0000-000000000001	Produto 1	10.000		1.50	2021-11-08 00:00:00+00	N
00000000-0000-0000-0000-000000000002	Produto 2	3.333		1.50	2021-11-08 00:00:00+00	N
00000000-0000-0000-0000-000000000003	Produto 3	4.100		1.99	2021-11-08 00:00:00+00	N
00000000-0000-0000-0000-000000000004	Produto 4	7.100		1.50	2021-11-08 00:00:00+00	N
00000000-0000-0000-0000-000000000005	Produto 5	3.330		1.50	2021-11-08 00:00:00+00	N
```
## 🔤 Resumo dos Endpoints
- **GET BY ID**
```
curl -X 'GET' \
  'https://localhost:5001/api/v1/Products/00000000-0000-0000-0000-000000000001/get-by-id' \
  -H 'accept: application/json'
```
- **GET BY FILTER**
```
curl -X 'GET' \
  'https://localhost:5001/api/v1/Products/get-by-filter?SearchTerm=Produto&SortColumn=Price&SortOrder=desc&Page=0&PageSize=2' \
  -H 'accept: application/json'
```
- **CREATE NEW PRODUCT**
```
curl -X 'POST' \
  'https://localhost:5001/api/v1/Products/create-new-product' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "Produto 6",
  "quantity": 9.99,
  "price": 3.77
}'
```
- **UPDATE BY ID**
```
curl -X 'PUT' \
  'https://localhost:5001/api/v1/Products/00000000-0000-0000-0000-000000000002/update-by-id' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "Produto 7",
  "quantity": 9.99,
  "price": 3.77
}'
```
-**DELETE BY ID**
```
curl -X 'DELETE' \
  'https://localhost:5001/api/v1/Products/00000000-0000-0000-0000-000000000005/delete-by-id' \
  -H 'accept: application/json'
```
---

## 🚀 Tecnologias Utilizadas
- **.NET 9 / C#**
- **Entity Framework Core** (Migrations, acesso a dados)
- **ASP.NET Core Web API**
- **xUnit** (Testes unitários e Integração)
- **Docker & Docker Compose** (containerização)
- **GitHub Actions** (CI/CD configurado)
---

## Padrões de projeto
- **DDD
- **CQRS
- **DECORATOR
- **SOLID

## 📂 Estrutura do Projeto
```
src/
└── WakeCommerce/
		├── Catalog.Application # Casos de uso (Application Layer)
		├── Catalog.Domain # Entidades e regras de negócio (Domain Layer)
		├── Catalog.Infrastructure # Infraestrutura e persistência (Infra Layer)
		├── Catalog.Web.Api # API pública (Presentation Layer)
		├── Shared.Defaults # Classes e contratos compartilhados
		├── Unit.Tests # Testes unitários
		└── Integration.Tests # Testes de integração
```  
Esse design segue os princípios de **CQRS + DDD + Clean Architecture**.

---
## 🗳️ Banco de Dados e ORM
Desenvolvido Code First, o bando de dados segue as instruções configuradas no EF na camada de Infraestrutura.
Em um cenário simples foi adotado um modelo de domínio simples (Anemic Domain) para dar o passo inicial, mas é possível migrar facilmente para um modelo de Domínio Rico ou (Rich Domain)
- **Por exemplo:**
```
		 Produto							Produto
 			├── Id								├── Id
			├── Name			===>>			├── Name
   			├── Price							├── Price
	  		└── Quantity						|	  ├── Currency
	 											|	  └── Amount
			 									└── Quantity
			 										  ├── Value
													  ├── Type
													  └── Pack
```
## 🛠️ Configuração do Banco de Dados

### Criar uma nova migration
No Package Manager Console:
```Package Manager CLI
add-migration <MIGRATION_NAME> -o Database/Migrations
```
No Powershell
```powershell
dotnet ef migrations add CreateDatabase --project .\Catalog.Infrastructure\ --startup-project .\Catalog.Web.Api\ -o Database/Migrations
```
