# Fidelidade App
Essencial de uma aplicação que visa a bonificação de clientes por pontos de vantagem

## _Funcionalidades_
- Cadastro de superusuário
- Cadastro de empresa
- Cadastro de cliente
- Cadastro de produtos
- Visualização de produtos disponíveis
- Cadastro de pontos para o cliente
- Resgate de produtos
- Atualização de status de entrega dos pedidos
- Geração de relatório contendo o histórico dos pontos do cliente
- Visualização dos pedidos realizados

## _Tecnologias Utilizadas_
- .Net Core 6
- MySql 8 (Docker)
```docker
MySql-8:
    container_name: "MySql-8"
    image: "mysql:8.0.27"
    ports: 
      - "3306:3306"
    environment:
      MYSQL_ROOT_PASSWORD: "P@ssw0rdMySql"
```
- Entity Framework
 
## _Execução do Projeto_
1. Certifique que seu banco de dados MySql está executando
2. Referencie corretamente seu banco de dados através da connection string MySql
3. Execute as migrações referentes aos dois contextos, Identity e Application
4. Execute a applicação API pelo seu IDE favorito

## _Conceitos que são encontrados no repositório_
- Unit Of Work
- Generic Repository Pattern
- Service Pattern
- Notification Pattern
- Injeção de Dependência
- Herança e Composição
- Princípios SOLID
- Mapeamento com automapper
- Versionamento de API
- Versionamento com Swagger
- Autorização de rotas
- Mapeamento do banco de dados com Fluent API
- Validação de entidades com Fluent Validation
- Validação de modelos com Data Notations
- Imutabilidade
- AsNoTrackingWithIdentityResolution
