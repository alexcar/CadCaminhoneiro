# Cadastro de Caminhoneiros
Aplicativo que tem como objetivo gerenciar o Cadastro de Caminhoneiros.

## Manual de Instalação

#### Objetivo
Preparar o Backend e o Frontend para testar o aplicativo Cadastro de Caminhoneiros.

### Clonando o repositório
1. Abra o terminal e crie um diretório de nome "code" por exemplo.<br>
`mkdir code`

2. A partir do diretório criado, realize o clone dos projetos.<br>
`cd code`<br>
`git clone https://github.com/alexcar/CadCaminhoneiro.git`

### Preparando o projeto Backend
1. Execute o Visual Studio 2019.
2. Abra o arquivo da solução no caminho:<br> 
`code\CadCaminhoneiro\Backend\JSL.CadCaminhoneiro\JSL.CadCaminhoneiro.sln`
3. Realize a compilação da solução.
4. Abra o Package Manager Console e selecionar o projeto JSL.CadCaminhoneiro.Data
5. Execute os comandos abaixo na janela Package Manager Console para criar e popular o banco de dados do tipo localdb<br>
`add-migration Inicial`<br>
`update-database`
6. Em seguida, execute a aplicação em modo debugger pressionando F5.<br>
5. O serviço Api deve ficar disponível no endereço:<br> 
`https://localhost:44313/swagger/index.html`

### Preparando o projeto Frontend
1. Abra o prompt do DOS e acesse a pasta da aplicação no caminho:<br> 
`cd code\CadCaminhoneiro\frontend\jsl-cad-caminhoneiro-web`
2. Atualize os pacotes digitando o comando:<br> 
`npm i`
3. Execute a aplicação digitando o comando:<br> 
`ng serve -- open`
6. O navegador padrão será executado e a aplicação ficará disponível no endereço:<br> 
`http://localhost:4200`
7. Em seguida realize a manutenção das aeronaves.
