-- Schema do banco usado pelo PDV.
-- Rode no SQL Server antes de abrir o sistema pela primeira vez.

IF DB_ID('Lojinha') IS NULL
    CREATE DATABASE Lojinha;
GO

USE Lojinha;
GO

-- ---------------------------------------------------------------
-- Usuários do sistema
-- ---------------------------------------------------------------
-- A coluna Senha guarda o hash completo no formato
--   PBKDF2$<iteracoes>$<salt em base64>$<hash em base64>
-- O salt vai junto do hash, por isso não existe coluna separada.
-- 200 caracteres cobrem o formato com folga.
IF OBJECT_ID('Usuarios', 'U') IS NULL
CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    Usuario   VARCHAR(20)  NOT NULL UNIQUE,
    Senha     VARCHAR(200) NOT NULL
);
GO

-- ---------------------------------------------------------------
-- Vendas registradas no PDV
-- ---------------------------------------------------------------
IF OBJECT_ID('Vendas', 'U') IS NULL
CREATE TABLE Vendas (
    VendaID          INT IDENTITY(1,1) PRIMARY KEY,
    ProdutoDescricao VARCHAR(200)   NOT NULL,
    Quantidade       INT            NOT NULL,
    PrecoUnitario    DECIMAL(10,2)  NOT NULL,
    PrecoTotal       DECIMAL(10,2)  NOT NULL,
    FormaPagamento   VARCHAR(50)    NOT NULL,
    Foto             VARBINARY(MAX) NULL
);
GO

-- ---------------------------------------------------------------
-- Primeiro usuário
-- ---------------------------------------------------------------
-- Não há INSERT de usuário aqui de propósito: o hash precisa ser gerado
-- pela aplicação, que sorteia um salt novo. Cadastre o primeiro usuário
-- pela tela "Novo cadastro" na tela de login.
