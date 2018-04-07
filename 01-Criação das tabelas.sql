if not exists (select 1 from sys.tables where name = 'Cliente')
begin
  create table Cliente (
    id int not null primary key identity(1,1),
    nome varchar(200) not null,
    cpf decimal(11,0) not null,
    )
end

if not exists (select 1 from sys.tables where name = 'Endereco')
begin
  create table Endereco (
    idCliente int not null,
    rua varchar(200) not null,
    numero int not null,
    complemento varchar(200) null,
    foreign key (idCliente) references Cliente(id)
    )
end

if not exists (select 1 from sys.tables where name = 'Item')
begin
  create table Item (
    id int not null primary key identity(1,1),
    descricao varchar(200) not null,
    valor decimal(24,8) not null,
    )
end
