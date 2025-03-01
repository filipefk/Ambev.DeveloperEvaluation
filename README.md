# Developer Evaluation Project


## Instruções para baixar e executar a API
**Os passos descritos abaixo são para executar a API usando o Docker. Certifique-se de ter o Docker instalado para que os comandos funcionem corretamente**

- Baixe os fontes da main que está tageada com a versão 3.0.0
- Após os fontes baixados, abra um console na pasta \template\backend e crie a imagem do Docker com o comando abaixo
  ```bash
  docker build -t ambevdeveloperevaluationwebapi:3.0 .
  ```
- Com a imagem pronta, basta subir o `Compose` executando também no terminal o comando abaixo
  ```bash
  docker compose up -d
  ```
- Acessar o Swagger da API na URL https://localhost/swagger/index.html

## Primeiro acesso
**A API já está configurada para conectar no banco de dados PostgreSQL com as credenciais definidas no arquivo docker-compose.yml**
**Ao executar pela primeira vez, a API vai criar as tabelas necessárias e um usuário**
`Username: Admin da Taking`
`Email: Admin@taking.com.br`
`Password: 1234.Abc`
`Phone: +551141026121`
`Status: Active`
`Role: Admin`

**A API também vai criar uma filial (Branch), que será necessária para realizar uma venda (Sale). Infelizmente ainda não temos a opção de cadastrar filiais pela API, mas esta funcionalidade em breve estará disponível**
`Id: 490dfaf7-0c1b-4855-a79f-3b0cd3bd1ee2`
`Name: Main store`

## Utilizando a API
**A API está documentada pelo Swagger e o primeiro endpoint a ser executado é o de autenticação (Auth) para receber um token**
**O token recebido deve ser adicionado pelo botão "Authorize" que está a direita no topo da página do Swagger**
**Lembre de incluir a palavra "Bearer " e um espaço antes do token conforme está orientado na própria tela que aparece**
`Examplo: 'Bearer 123abcde'`
**A partir deste ponto, é possível executar o CRUD de todas as entidades da API.**
**O banco de dados inicia vazio, por isso será necessário cadastrar os Produtos para depois poder criar um carrinho e uma venda. Em uma próxima versão, teremos o recurso de carga de dados automática para facilitar os testes**
**Também não temos ainda endpoints para trazer indicadores de venda, mas é possível visualizar a listagem completa de vendas com os dados pertinentes**
`Enjoi`