# SystemLogin

Sistema de Login com React, .NET Core e SQL Server

Este projeto é um sistema de login completo que utiliza React para o frontend, .NET Core para o backend e SQL Server para armazenamento de dados. Ele implementa autenticação via REST API com suporte a paginação para listar usuários.

---

## Estrutura do Projeto

O projeto está dividido em três partes principais:

- **Frontend**: Aplicação React utilizando Vite para desenvolvimento rápido.
- **Backend**: API REST desenvolvida com ASP.NET Core para autenticação e gerenciamento de usuários.
- **Testes Unitários**: Projeto de testes com `xUnit` para validar o comportamento da aplicação no backend.

---

## Tecnologias Utilizadas

### Backend
- **ASP.NET Core 8.0**
- **SQL Server**
- **Entity Framework Core**
- **JWT (JSON Web Tokens)**
- **xUnit** (para testes)
- **Moq** (para mocks em testes)

### Frontend
- **React 18.x**
- **TypeScript**
- **Vite**
- **Axios**

---

## Como Rodar o Projeto

### 1. Rodando o Backend (API)

#### Pré-requisitos
- **.NET 8.0 SDK** instalado (Baixe o .NET SDK)
- **SQL Server** (ou SQL Server Express) instalado e rodando (Baixe o SQL Server)

#### Passos

1. **Clone o repositório**: Clone o repositório do projeto para o seu diretório local:

    ```bash
    git clone https://github.com/giacominimarco/SystemLogin.git
    ```

2. **Navegue até a pasta do backend**: No terminal, vá até a pasta do projeto backend:

    ```bash
    cd SystemLogin/ProjetoLogin.API
    ```

3. **Restaurar as dependências**: Restaure as dependências do projeto backend:

    ```bash
    dotnet restore
    ```

4. **Configurar o banco de dados**: No arquivo `appsettings.json`, configure a string de conexão para o banco de dados SQL Server:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=SystemLogin;Trusted_Connection=True;"
      }
    }
    ```

    **Nota**: Se você estiver usando o SQL Server Express, pode ser necessário ajustar a string de conexão, por exemplo, para:

    ```json
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SystemLogin;Trusted_Connection=True;"
    ```

5. **Criar o banco de dados e as tabelas**: Crie o banco de dados e as tabelas utilizando Entity Framework Core. Execute o comando de migração:

    ```bash
    dotnet ef database update
    ```

    Esse comando criará as tabelas no banco de dados de acordo com os modelos definidos no código.

6. **Rodar a aplicação**: Após as configurações, inicie o servidor da API:

    ```bash
    dotnet run
    ```

    O backend estará rodando por padrão em `http://localhost:5129`.

### 2. Rodando o Frontend (React)

#### Pré-requisitos
- **Node.js** (versão >= 16.8) instalado (Baixe o Node.js)
- **npm** ou **yarn** instalado (gerenciador de pacotes)

#### Passos

1. **Navegue até a pasta do frontend**: No terminal, vá até a pasta do frontend:

    ```bash
    cd SystemLogin/frontend-login
    ```

2. **Instalar as dependências do frontend**: Instale as dependências necessárias do frontend:

    ```bash
    npm install
    ```

    Ou se estiver usando o **yarn**:

    ```bash
    yarn install
    ```

3. **Rodar a aplicação frontend**: Após a instalação das dependências, inicie o servidor de desenvolvimento:

    ```bash
    npm run dev
    ```

    Ou se estiver usando o **yarn**:

    ```bash
    yarn dev
    ```

    O frontend estará rodando em `http://localhost:5173`.



### 3. Rodando os Testes Unitários

#### Estrutura de Testes

Os testes do backend estão localizados no projeto:

```
SystemLogin/ProjetoLogin.API.Tests/
```

Este projeto utiliza **xUnit** como framework de testes e **Moq** para criação de objetos simulados (mocks). Os testes abrangem, principalmente, a lógica de autenticação e geração de tokens da camada `UserService`.

#### Executando os testes

1. Navegue até a raiz do repositório (ou mantenha-se lá mesmo):
```bash
cd SystemLogin
```

2. Execute os testes com o comando:

```bash
dotnet test
```

Esse comando compilará o projeto de testes, executará os testes e exibirá um resumo com os resultados (total, sucesso ou falha).

#### Dependências utilizadas nos testes

- `xunit`
- `xunit.runner.visualstudio`
- `Moq`
- `Microsoft.NET.Test.Sdk`

Estas dependências já estão configuradas no `.csproj` do projeto de testes.


### Sobre `UnitTest1.cs`

O arquivo `UnitTest1.cs` é criado automaticamente como exemplo ao iniciar o projeto de testes com `xUnit`. Ele **pode ser removido com segurança** se você já escreveu seus próprios testes (ex: `UserServiceTests.cs`).

---

## O que deve estar instalado

Antes de rodar o projeto, certifique-se de que você tem os seguintes softwares instalados:

- **.NET SDK 8.0**: [Baixe o .NET SDK](https://dotnet.microsoft.com/download)
- **SQL Server** (ou SQL Server Express): [Baixe o SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- **Node.js** (versão >= 16.8): [Baixe o Node.js](https://nodejs.org/en/download/)
- **npm** (gerenciador de pacotes do Node.js): O npm já vem com o Node.js, então, ao instalar o Node.js, o npm também será instalado.
- **Visual Studio Code** (opcional, mas recomendado): [Baixe o Visual Studio Code](https://code.visualstudio.com/)
- **Ferramentas do Entity Framework Core**: Instale as ferramentas do EF Core para rodar as migrações:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

## Endpoints da API

A API possui os seguintes endpoints:

- **POST /api/user/login**: Realiza o login do usuário.

    **Body** (exemplo):

    ```json
    {
      "username": "admin",
      "password": "1234"
    }
    ```

- **GET /api/user/list**: Lista os usuários com paginação.

    **Query Parameters**:
    - **page**: Página (padrão 1)
    - **pageSize**: Quantidade de itens por página (padrão 10)

## Problemas Comuns

- **Erro 404 na API**: Verifique se a API está rodando corretamente e acessível na URL correta (`http://localhost:5129`).
- **Erro ao conectar ao banco de dados**: Verifique a string de conexão no arquivo `appsettings.json` e se o SQL Server está rodando corretamente.
- **Erro de dependências no frontend**: Certifique-se de que todas as dependências foram instaladas corretamente com `npm install` ou `yarn install`.

## Conclusão

Este projeto demonstra uma aplicação full stack moderna com autenticação via JWT e testes automatizados no backend.
