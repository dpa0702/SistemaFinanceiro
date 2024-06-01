## Execução da Solução

A solução foi criada utilizando o Visual Studio com o runtime do dotnet 8.0, banco de dados SQL Server, por tanto para executar a solução é necessário garantir que todas as tecnologias necessárias estejam em execução (para o banco de dados, 
pode ser uma instancia local ou em nuvem).


### Banco de dados

A utilização do banco SQL Server foi devido aos relacionamentos entre as tabelas para que as consultas sejam mais eficientes. 

Será necessária a criação do banco de dados dentro de um servidor SQL Server:

A connectionString deverá ser colocada dentro do appsettings do projeto

Após criado o banco de dados e atualizada a connectionString, será necessário executar as migrations do projeto, executando um terminal dentro do diretório do projeto e rodando o comando do entity framework:

PM> cd [diretório do projeto 'Infra']
PM> Update-Database -StartupProject [projeto.'Infra'] -connection "adicioneSuaConectionStringAqui"
