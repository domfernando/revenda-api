# Revenda.API - Endpoint Documentation

## Visão Geral

A API `Revenda.API` gerencia clientes e seus dados associados. Os principais endpoints permitem operações de consulta, criação e atualização de clientes. Todos os endpoints seguem convenções REST e retornam respostas padronizadas.

---

## Endpoints

### 1. Obter Cliente por ID

- **Rota:** `GET /api/Cliente/{id}`
- **Descrição:** Retorna os dados de um cliente específico pelo seu ID.
- **Parâmetros de Rota:**
  - `id` (int): Identificador do cliente.
- **Resposta:**
  - `200 OK`: Objeto `ClienteResponse` se encontrado.
  - `404 Not Found`: Se o cliente não existir.

---

### 2. Listar Clientes

- **Rota:** `GET /api/Cliente`
- **Descrição:** Retorna uma lista de clientes, podendo ser filtrada por parâmetros.
- **Parâmetros de Query (opcionais):**
  - `Id` (int): Filtra por ID do cliente.
  - `Nome` (string): Filtra por nome.
- **Resposta:**
  - `200 OK`: Lista de objetos `ClienteResponse`.

---

### 3. Criar Cliente

- **Rota:** `POST /api/Cliente`
- **Descrição:** Cria um novo cliente.
- **Body:** Objeto `CreateClienteRequest`
  - `TipoPessoa` (int): Tipo de pessoa.
  - `NomeCompleto` (string): Nome completo.
  - `Nome` (string): Nome.
  - `Documento` (string): Documento.
  - `Email` (string): Email.
  - `Enderecos` (List): Lista de endereços.
- **Resposta:**
  - `200 OK`: Objeto `ClienteResponse` criado.

---

### 4. Atualizar Cliente

- **Rota:** `PUT /api/Cliente`
- **Descrição:** Atualiza os dados de um cliente existente.
- **Body:** Objeto `UpdateClienteRequest`
  - `Id` (int): Identificador do cliente.
  - Demais campos iguais ao de criação, incluindo contatos.
- **Resposta:**
  - `200 OK`: Objeto `ClienteResponse` atualizado.

---

## Regras de Negócio

- Todos os campos obrigatórios devem ser preenchidos.
- O campo `Email` deve ser válido.
- O campo `Documento` deve ser único por cliente.
- Para atualização, o cliente deve existir.
- Endereços e contatos são obrigatórios conforme o tipo de pessoa.

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

## Observações

- Todos os endpoints retornam erros padronizados em caso de falha de validação ou exceção.
- Utilize sempre o formato JSON para requisições e respostas.
