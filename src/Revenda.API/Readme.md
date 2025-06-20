# Revenda.API - Endpoint Documentation

## Vis�o Geral

A API `Revenda.API` gerencia clientes e seus dados associados. Os principais endpoints permitem opera��es de consulta, cria��o e atualiza��o de clientes. Todos os endpoints seguem conven��es REST e retornam respostas padronizadas.

---

## Endpoints

### 1. Obter Cliente por ID

- **Rota:** `GET /api/Cliente/{id}`
- **Descri��o:** Retorna os dados de um cliente espec�fico pelo seu ID.
- **Par�metros de Rota:**
  - `id` (int): Identificador do cliente.
- **Resposta:**
  - `200 OK`: Objeto `ClienteResponse` se encontrado.
  - `404 Not Found`: Se o cliente n�o existir.

---

### 2. Listar Clientes

- **Rota:** `GET /api/Cliente`
- **Descri��o:** Retorna uma lista de clientes, podendo ser filtrada por par�metros.
- **Par�metros de Query (opcionais):**
  - `Id` (int): Filtra por ID do cliente.
  - `Nome` (string): Filtra por nome.
- **Resposta:**
  - `200 OK`: Lista de objetos `ClienteResponse`.

---

### 3. Criar Cliente

- **Rota:** `POST /api/Cliente`
- **Descri��o:** Cria um novo cliente.
- **Body:** Objeto `CreateClienteRequest`
  - `TipoPessoa` (int): Tipo de pessoa.
  - `NomeCompleto` (string): Nome completo.
  - `Nome` (string): Nome.
  - `Documento` (string): Documento.
  - `Email` (string): Email.
  - `Enderecos` (List): Lista de endere�os.
- **Resposta:**
  - `200 OK`: Objeto `ClienteResponse` criado.

---

### 4. Atualizar Cliente

- **Rota:** `PUT /api/Cliente`
- **Descri��o:** Atualiza os dados de um cliente existente.
- **Body:** Objeto `UpdateClienteRequest`
  - `Id` (int): Identificador do cliente.
  - Demais campos iguais ao de cria��o, incluindo contatos.
- **Resposta:**
  - `200 OK`: Objeto `ClienteResponse` atualizado.

---

## Regras de Neg�cio

- Todos os campos obrigat�rios devem ser preenchidos.
- O campo `Email` deve ser v�lido.
- O campo `Documento` deve ser �nico por cliente.
- Para atualiza��o, o cliente deve existir.
- Endere�os e contatos s�o obrigat�rios conforme o tipo de pessoa.

---

## Modelos de Dados

### ClienteResponse

- `Id` (int)
- `TipoPessoa` (int)
- `NomeCompleto` (string)
- `Nome` (string)
- `Documento` (string)
- `Email` (string)
- `Enderecos` (List)
- `Contatos` (List)

### CreateClienteRequest / UpdateClienteRequest

- Consulte os campos acima, sendo que `UpdateClienteRequest` inclui o campo `Id` e lista de contatos.

---

## Observa��es

- Todos os endpoints retornam erros padronizados em caso de falha de valida��o ou exce��o.
- Utilize sempre o formato JSON para requisi��es e respostas.
