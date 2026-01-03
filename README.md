# Totvs – API de Gestão de Candidatos e Vagas

API REST desenvolvida em **.NET 8**, utilizando **MongoDB** e aplicando
**DDD**, **testes unitarios** e **containerização com Docker**.

# Tecnologias
- .NET 8
- MongoDB
- Docker
- Xunit
- Swagger
- Postman

# Arquitetura
A arquitetura do projeto esta separado em API, Application, Domain, 
Infrastructure e Tests seguindo o padrão do DDD.

Analisando os requisitos propostos no teste, não achei necessario
a implementação de mecanismos de segurança/autenticação utilizando o Security.
Porem tenho familiaridade com autorização utilizando token JWT.

# Funcionalidades 
## Candidato
- Criar
- Listar
- Buscar por Id
- Atualizar
- Deletar
- Atualizar curriculo

## Vagas
- Criar
- Listar
- Buscar por Id
- Atualizar
- Deletar
- Inscrição de candidato na vaga
- Listar canditados de uma vaga

## Tratamento de Exceções
Utilização de exceções personalizadas para camada de dominio, como a EntityNotFound e ValidationException.
Middleware para retorno de reposta 404 Not Found para quando a entidade nao for encontrada.

## Mapper
Classe de classes Mapper estatica para converter as entidades de dominios em DTOs de resposta para a Api.

## Testes Unitarios
Projeto de testes cobrindo todas a logica da camada de serviço e possiveis exceptions

## Testes Automatizados com Postman
Na pasta raiz do projeto estão os arquivos de exportação do postman, contendo a collection e o enviroment
com algumas requisições e testes.
Basta clicar na collection e em RUN, selecionar o enviroment e rodar.

## Docker
Tanto a aplicação do Back quanto o banco estão subindo em um container do Docker,
por padrão a API esta configurada na porta 8080 e o Mongo  a 27017

## Swagger
- http://localhost:8080/swagger

# Como executar a aplicação
## Requisitos:
- Docker
- Docker Compose

## Execução:
- Navegue até a pasta que contem o arquivo do docker-compose e execute o comando abaixo no terminal

- ```docker compose up --build``` 
