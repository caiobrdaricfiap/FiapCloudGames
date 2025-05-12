# FIAP Cloud Games API

**FIAP Cloud Games** é a plataforma do tech challange da FIAP que nasceu com o propósito de ser o próximo grande sucesso no universo dos jogos! 🎮🚀 Estamos na nossa primeira fase de desenvolvimento, com a missão de criar um serviço robusto para gerenciar usuários e seus jogos adquiridos.

## O que é isso aqui?

Esta API foi desenvolvida para gerenciar tudo relacionado a usuários e jogos em nossa plataforma. Com ela, você poderá realizar o **cadastro de usuários**, autenticação via **JWT**, **cadastrar jogos** e muito mais! O objetivo é garantir que a plataforma possa escalar e evoluir com o tempo, sempre mantendo a base sólida.

E sim, estamos começando com um **MVP** (Produto Mínimo Viável), mas já com todos os **requisitos técnicos** que você vai precisar para gerenciar seus dados de maneira eficiente.

## Funcionalidades Principais

### 1. Cadastro de Usuários
- Nome, **e-mail** e **senha** são os dados essenciais.
- E sim, garantimos uma validação para e-mails e senhas seguras (mínimo de 8 caracteres, números, letras e caracteres especiais).

### 2. Autenticação e Autorização
- **Autenticação via JWT**: A chave para você interagir com nossa API de forma segura.
- Dois níveis de acesso:
  - **Usuário**: Acesso à plataforma e biblioteca de jogos.
  - **Administrador**: Controle total! Cadastrar jogos, gerenciar usuários, criar promoções.

### 3. Cadastro e Listagem de Jogos
- A partir de agora, os jogos são parte da sua vida. E você poderá:
  - Cadastrar jogos (somente **administradores**).
  - Consultar a biblioteca de jogos adquiridos (para **usuários**).

### 4. Arquitetura
- **Monolito**: Sim, estamos começando de forma simples e ágil. Vamos evoluir, mas por enquanto, é só o que precisamos para o MVP.

---

## Requisitos Técnicos

### 1. Persistência de Dados
- **Entity Framework Core** para persistir dados de usuários e jogos.
- **MongoDB** (opcional) para maior flexibilidade e performance na persistência.
- **Dapper** (opcional) se for necessário otimizar consultas.

### 2. API com .NET 8
- **Minimal API** ou **Controllers MVC**: Escolha do melhor estilo para a sua API.
- **Middleware** para tratamento de erros e logs estruturados.
- **Swagger** para documentação dos endpoints da API (sim, é super importante).

### 3. Qualidade de Software
- **Testes unitários** para validar a lógica de negócios.
- **TDD** ou **BDD** em pelo menos um módulo do sistema.
  
### 4. Domain-Driven Design (DDD)
- Vamos mapear o domínio com **Event Storming** e seguir o fluxo de interações para garantir a melhor modelagem possível.
  
---

## Como clonar o projeto

1. Clone o repositório e contribuir:

```bash
git clone https://github.com/caiobrdaric/FiapCloudGames.git
cd FiapCloudGames
```

2. Criar uma Nova Branch

Antes de começar a fazer modificações, é importante criar uma nova branch para trabalhar. Isso garante que as mudanças feitas sejam isoladas e facilitam a revisão do código.
```bash
git checkout -b nome-da-branch
```

## Comandos mais utilizados no EntityFramework Core.

1. Criar Migração:
  Add-Migration NomeDaMigracao

2. Atualizar a base de dados conforme a ultima migration.
  Update-Database