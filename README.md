# PDV Desktop — Sistema de Ponto de Venda

Aplicação desktop em **C# / WinForms** para um pequeno comércio: login com senha
criptografada, cadastro de usuários, registro de vendas com foto do produto, backup e
restore do banco pela própria interface, e visualizador de logs.

O foco do projeto é **arquitetura em camadas**: a interface não conversa com o banco, e a
regra de negócio não sabe SQL.

## Arquitetura

```
┌──────────────────────────────────────────────┐
│  UI — WinForms                               │
│  FrmLogin, FrmCadastroUsuario, FrmPDV,       │
│  FrmBKeRestore, FrmLogs                      │
└───────────────────┬──────────────────────────┘
                    │  só chama a BLL
┌───────────────────▼──────────────────────────┐
│  BLL — Business Logic Layer                  │
│  UsuarioBLL   validações e autenticação      │
│  VendaBLL     regras de venda                │
│  SegurancaBLL hash e força de senha          │
└───────────────────┬──────────────────────────┘
                    │  só chama a DAL
┌───────────────────▼──────────────────────────┐
│  DAL — Data Access Layer                     │
│  UsuarioDAL, VendasDAL, BackupRestoreDAL     │
│  ADO.NET com queries parametrizadas          │
└───────────────────┬──────────────────────────┘
                    │
              SQL Server (Lojinha)
```

## Funcionalidades

- **Login** com senha protegida por PBKDF2 e salt individual
- **Cadastro de usuários** com validação de nome e força de senha
- **PDV** — registro de vendas com descrição, quantidade, preço, forma de pagamento e foto
- **CRUD completo** de vendas (inserir, listar, editar, excluir)
- **Backup e restore** do banco pela interface, com validação do arquivo `.bak`
- **Tela de logs** para consultar o que o sistema registrou

## Tecnologias

- **C#** / **.NET Framework 4.7.2**
- **Windows Forms** com **Guna.UI2** para os componentes visuais
- **SQL Server** com **ADO.NET** (`System.Data.SqlClient`)
- **NLog** para registro de eventos e erros

## Segurança

**Senhas com PBKDF2-SHA256.** Cada usuário recebe um salt aleatório de 128 bits sorteado
com `RNGCryptoServiceProvider` no momento do cadastro. A derivação usa 100.000 iterações.
Salt e hash são guardados juntos, num campo só:

```
PBKDF2$100000$<salt em base64>$<hash em base64>
```

Como o salt é diferente por usuário, dois usuários com a mesma senha ficam com hashes
diferentes, e tabelas pré-calculadas (rainbow tables) não ajudam o atacante.

**Comparação em tempo constante.** `ComparacaoSegura` percorre todos os bytes sempre, em
vez de parar no primeiro que difere. Uma comparação comum vaza, pelo tempo de resposta,
quantos bytes foram acertados.

**A verificação não acontece em SQL.** Com salt por usuário o banco não tem como
recalcular o hash. A DAL devolve o hash guardado e quem compara é a BLL. Isso também evita
mandar qualquer forma da senha na query.

**Queries parametrizadas** em todo acesso a dados, contra SQL injection.

**Sem credenciais no código.** As strings de conexão ficam no `App.config` e usam
autenticação integrada do Windows — não há usuário nem senha de banco versionados.

## Como rodar

Pré-requisitos: **Visual Studio 2019+** (com workload .NET desktop) e **SQL Server**
(o Express serve).

```bash
git clone https://github.com/juliocsg1/pdv-desktop-csharp.git
```

1. Crie o banco rodando `database/schema.sql` no SQL Server
2. Abra `teladelogin.sln` no Visual Studio
3. Restaure os pacotes NuGet (o Visual Studio faz sozinho, ou rode `nuget restore`)
4. Se o seu SQL Server não for `localhost`, ajuste as duas strings de conexão em
   `teladelogin/App.config`
5. Compile e execute (F5)
6. Na tela de login, use **"Novo cadastro"** para criar o primeiro usuário

> O `schema.sql` não insere usuário nenhum de propósito. O hash precisa ser gerado pela
> aplicação, que sorteia o salt — um `INSERT` com senha fixa não funcionaria.

## Estrutura

```
teladelogin/
├── BLL/
│   ├── SegurancaBLL.cs      PBKDF2, comparação segura e validação de força
│   ├── UsuarioBLL.cs        Login, cadastro e alteração de senha
│   └── VendaBLL.cs          Regras de venda
├── DAL/
│   ├── UsuarioDAL.cs        Busca do hash, cadastro e alteração
│   ├── VendasDAL.cs         CRUD de vendas
│   └── BackupRestoreDAL.cs  BACKUP/RESTORE DATABASE via banco master
├── UI/                      Os cinco formulários
├── App.config               Strings de conexão
└── NLog.config              Configuração de log

database/schema.sql          Criação do banco e das tabelas
```

## Detalhes de implementação

**Backup e restore usam duas conexões.** Não dá para restaurar um banco enquanto se está
conectado a ele, então `BackupRestoreDAL` mantém uma conexão separada ao banco `master`.
Antes de restaurar, coloca o banco em `SINGLE_USER` para derrubar conexões pendentes.

**Foto do produto em `VARBINARY(MAX)`.** A imagem da venda é gravada como binário no banco,
não como caminho de arquivo.

**Log estruturado.** Cada camada tem seu próprio `Logger` do NLog, e as mensagens registram
o usuário afetado sem nunca gravar a senha.

## Limitações conhecidas

- Não há controle de perfis: todo usuário autenticado tem acesso a todas as telas.
- A sessão não expira — o sistema fica aberto enquanto o formulário estiver aberto.
- O caminho de backup é digitado pelo usuário e interpolado no comando SQL. Como o campo é
  preenchido por um operador local e não por entrada externa, o risco é baixo, mas o certo
  seria parametrizar.
- Sem testes automatizados.

## Contexto

Projeto desenvolvido na disciplina de Desenvolvimento de Sistemas do curso Técnico em
Desenvolvimento de Sistemas — ETEC João Belarmino.

A versão entregue em aula usava SHA256 com um salt fixo, igual para todos os usuários.
Ao publicar aqui, a camada de segurança foi reescrita para PBKDF2 com salt individual,
o que exigiu também mudar a verificação de login: antes a comparação era feita dentro da
query SQL, e agora acontece na BLL.
