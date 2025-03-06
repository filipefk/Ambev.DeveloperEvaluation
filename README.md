# Developer Evaluation Project


## Instruções para baixar e executar a API
**Os passos descritos abaixo são para executar a API usando o Docker. Certifique-se de ter o Docker instalado para que os comandos funcionem corretamente**

- Baixe os fontes da main que está tageada com a versão 4.0.0
- Após os fontes baixados, abra um console na pasta \template\backend e crie a imagem do Docker com o comando abaixo
  ```bash
  docker-compose up -d --build
  ```
- Acessar o Swagger da API na URL http://localhost:8080/swagger/index.html

## Primeiro acesso
**A API já está configurada para conectar no banco de dados PostgreSQL com as credenciais que vieram definidas no arquivo docker-compose.yml**

**Ao executar pela primeira vez, a API vai criar as tabelas necessárias, alguns usuários, produtos e filiais. O usuário abaixo pode ser usado para autenticar o acesso**
```
Username: Admin da Taking
Email: Admin@taking.com.br
Password: 1234.Abc
Phone: +551141026121
Status: Active
Role: Admin
```

**Os outros usuários, produtos e filiais podem ser consultados pelas rotas de Get de cada entidade**

## Utilizando a API
**A API está documentada pelo Swagger e o primeiro endpoint a ser executado é o de autenticação (Auth) para receber um token**

**O token recebido deve ser adicionado pelo botão "Authorize" que está a direita no topo da página do Swagger**

**Lembre de incluir a palavra "Bearer " e um espaço antes do token conforme está orientado na própria tela que aparece**

`Examplo: 'Bearer 123abcde'`

**A partir deste ponto, é possível executar o CRUD de todas as entidades da API.**

**Passo a passo para criar uma venda:**
- Autentique o usuário e adicione o token para as próximas chamadas
- Escolha os produtos e cadastre um carrinho
- Se quiser pode cadastrar novos usuários e produtos também
- Para criar uma venda, selecione o ID do carrinho e o ID de uma filial
- Quando a venda é criada, o carrinho é apagado
- Um usuário com a role "Customer" não pode fazer nada nos endpoints de usuários e só pode fazer consultas (Get) nos endpoints de produto
- Quando a venda é criada, são aplicadas as regras de desconto definidas
- Existe um endpoint específico para cancelar uma venda

**Nesta versão, não temos endpoints para trazer indicadores de venda ainda, mas é possível visualizar a listagem completa de vendas com os dados pertinentes**

**A solução tem 154 testes de unidade e 23 testes de integração**

**Foram criados eventos para criação de venda, alteração de venda, cancelamento de venda e está sendo enviado para o LOG**

**Qualquer dúvida, estou a disposição**
