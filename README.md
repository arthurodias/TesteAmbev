# Projeto DeveloperEvaluation

Este projeto é uma implementação de um sistema de vendas que segue as regras de negócios específicas de descontos por quantidade de itens e validação de transações. O sistema utiliza conceitos de **Domain-Driven Design (DDD)** e **CQRS** com MediatR e xUnit para testes unitários.

## Requisitos

Antes de começar, certifique-se de que você tem as seguintes ferramentas instaladas no seu ambiente de desenvolvimento:

- [.NET SDK 8.0 ou superior](https://dotnet.microsoft.com/download)
- Postgres ou outro banco de dados relacional configurado](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio ou outro editor de sua preferência (como VS Code)](https://visualstudio.microsoft.com/)
- [xUnit](https://xunit.net/) para testes unitários

## Configuração do Projeto

### Passo 1: Clonar o Repositório

Primeiro, clone o repositório para sua máquina local:

```bash
git clone https://github.com/seu-usuario/developerevaluation.git
cd developerevaluation 
```

## Configuração do Projeto

### Passo 2: Restaurar Dependências

Após clonar o repositório, restaure as dependências do projeto com o comando:

```bash
dotnet restore
```

### Passo 3: Configuração do Banco de Dados

Este projeto utiliza o **PostgreSQL** como banco de dados. Certifique-se de ter o PostgreSQL instalado e em execução localmente na porta padrão **5432**.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=DeveloperEvaluation;Username=sa;Password=Pass@word"
  }
}
```
Aplique as migrações do Entity Framework para criar o esquema no banco de dados:
```bash
dotnet ef database update
```

### Passo 4: Configuração do Projeto de Testes

O projeto de testes está localizado na pasta Ambev.DeveloperEvaluation.Unit. Para rodar os testes unitários, basta navegar até essa pasta e executar o comando:

```bash
dotnet test
```

Executando o Projeto

Para rodar a aplicação, utilize o seguinte comando na pasta do projeto principal:

```bash
dotnet run
```

## Licença

Este projeto foi desenvolvido por **Arthur Rodrigues**.



