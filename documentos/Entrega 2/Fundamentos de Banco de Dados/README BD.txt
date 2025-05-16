****README*****

# 📦 Banco de Dados - Sistema de Monitoramento de Consumo Energético

Este banco de dados foi projetado para armazenar informações relacionadas ao monitoramento de consumo de energia em uma residência inteligente. Ele inclui usuários, sensores, dados dos sensores, registros de consumo, metas de economia, desempenho, recompensas e histórico de resgates.

---

## 🧑‍💼 Tabela: `USUARIO`

Armazena os dados dos usuários do sistema.

| Coluna     | Tipo           | Descrição                           |
|------------|----------------|-------------------------------------|
| id_usuario | INT (PK)       | Identificador único do usuário      |
| nome       | VARCHAR(50)    | Nome completo                       |
| renda      | DECIMAL(10,2)  | Renda mensal                        |
| email      | VARCHAR(50)    | Endereço de e-mail                  |

---

## 🔧 Tabela: `SENSOR`

Registra os sensores instalados nas residências dos usuários.

| Coluna     | Tipo     | Descrição                            |
|------------|----------|----------------------------------------|
| id_sensor  | INT (PK) | Identificador único do sensor          |
| id_usuario | INT (FK) | Chave estrangeira para `USUARIO`       |

---

## 📊 Tabela: `DADOS_SENSOR`

Armazena as leituras captadas pelos sensores.

| Coluna          | Tipo         | Descrição                                      |
|------------------|--------------|-----------------------------------------------|
| id_dado          | INT (PK)     | Identificador único do dado                   |
| id_sensor        | INT (FK)     | Chave estrangeira para `SENSOR`               |
| tipo             | VARCHAR(50)  | Tipo do dado (temperatura, umidade, etc.)     |
| data_TimeStamp   | DATE         | Data da leitura                               |
| hora_TimeStamp   | TIME         | Hora da leitura                               |
| movimento        | BOOLEAN      | Indica detecção de movimento (true/false)     |

---

## ⚡ Tabela: `CONSUMO`

Registra o consumo de energia por sensor e por usuário.

| Coluna      | Tipo           | Descrição                                |
|-------------|----------------|------------------------------------------|
| id_consumo  | INT (PK)       | Identificador único do registro          |
| data        | DATE           | Data do consumo                          |
| valor_kWh   | DECIMAL(10,2)  | Quantidade de energia consumida (kWh)    |
| id_sensor   | INT (FK)       | Sensor responsável pelo consumo          |
| id_usuario  | INT (FK)       | Usuário associado ao consumo             |

---

## 🎯 Tabela: `METAS`

Define metas de economia de energia.

| Coluna          | Tipo           | Descrição                                 |
|------------------|----------------|-------------------------------------------|
| id_meta          | INT (PK)       | Identificador único da meta               |
| descricao        | VARCHAR(100)   | Descrição da meta                         |
| valor_meta_kWh   | DECIMAL(10,2)  | Valor de consumo a ser atingido (kWh)     |
| data_limite      | DATE           | Data limite para atingir a meta           |

---

## 📈 Tabela: `ECONOMIZA`

Relaciona usuários com metas e registra o desempenho em relação às metas.

| Coluna      | Tipo        | Descrição                                     |
|-------------|-------------|-----------------------------------------------|
| id_usuario  | INT (PK, FK)| Usuário que participa da meta                 |
| id_meta     | INT (PK, FK)| Meta relacionada                              |
| id_consumo  | INT (FK)    | Registro de consumo usado na avaliação        |
| desempenho  | FLOAT       | Percentual de desempenho alcançado            |

---

## 🏆 Tabela: `RECOMPENSA`

Define recompensas disponíveis para os usuários.

| Coluna          | Tipo           | Descrição                          |
|------------------|----------------|------------------------------------|
| id_recompensa    | INT (PK)       | Identificador único da recompensa |
| nome_recompensa  | VARCHAR(100)   | Nome da recompensa                |
| custo_pontos     | INT            | Custo da recompensa em pontos     |

---

## 🎁 Tabela: `USUARIO_RECOMPENSA`

Histórico de recompensas recebidas pelos usuários.

| Coluna           | Tipo           | Descrição                                     |
|------------------|----------------|-----------------------------------------------|
| id_usuario       | INT (PK, FK)   | Usuário que recebeu a recompensa              |
| id_recompensa    | INT (PK, FK)   | Recompensa recebida                           |
| data_recebimento | DATE (PK)      | Data em que a recompensa foi recebida         |

---

## 🔗 Relacionamentos

- `USUARIO` 1️⃣➝🔁 `SENSOR`
- `SENSOR` 1️⃣➝🔁 `DADOS_SENSOR`
- `USUARIO` 1️⃣➝🔁 `CONSUMO`
- `SENSOR` 1️⃣➝🔁 `CONSUMO`
- `USUARIO` 🔁⛓️🔁 `METAS` via `ECONOMIZA`
- `USUARIO` 🔁⛓️🔁 `RECOMPENSA` via `USUARIO_RECOMPENSA`

---

> Documentação gerada automaticamente ✨

# **Banco de Dados - Código**


CREATE TABLE USUARIO (
id_usuario INT AUTO_INCREMENT,
nome VARCHAR(50) NOT NULL,
renda DECIMAL(10,2),
email VARCHAR(50),
PRIMARY KEY (id_usuario)
);



CREATE TABLE SENSOR (
id_sensor INT AUTO_INCREMENT,
id_usuario INT,
PRIMARY KEY (id_sensor),
FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario)
);



CREATE TABLE DADOS_SENSOR (
id_dado INT AUTO_INCREMENT,
id_sensor INT,
tipo VARCHAR(50) NOT NULL,
PRIMARY KEY (id_dado),
FOREIGN KEY (id_sensor) REFERENCES SENSOR(id_sensor)

ALTER TABLE DADOS_SENSOR
ADD COLUMN data_TimeStamp DATE NOT NULL;

ALTER TABLE DADOS_SENSOR
ADD COLUMN hora_TimeStamp TIME NOT NULL;

);




ALTER TABLE DADOS_SENSOR
ADD COLUMN movimento BOOLEAN NOT NULL DEFAULT FALSE;

CREATE TABLE CONSUMO (
id_consumo INT AUTO_INCREMENT,
data DATE NOT NULL,
valor_kWh DECIMAL(10,2) NOT NULL,
id_sensor INT,
id_usuario INT,
PRIMARY KEY (id_consumo),
FOREIGN KEY (id_sensor) REFERENCES SENSOR(id_sensor),
FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario)
);




CREATE TABLE METAS (
id_meta INT AUTO_INCREMENT,
descricao VARCHAR(100),
valor_meta_kWh DECIMAL(10,2),
data_limite DATE,
PRIMARY KEY (id_meta)
);




CREATE TABLE ECONOMIZA (
id_usuario INT,
id_meta INT,
id_consumo INT,
desempenho FLOAT,
PRIMARY KEY (id_usuario, id_meta),
FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario),
FOREIGN KEY (id_meta) REFERENCES METAS(id_meta),
FOREIGN KEY (id_consumo) REFERENCES CONSUMO(id_consumo)
);



CREATE TABLE RECOMPENSA (
id_recompensa INT AUTO_INCREMENT,
nome_recompensa VARCHAR(100),
custo_pontos INT,
PRIMARY KEY (id_recompensa)
);


CREATE TABLE USUARIO_RECOMPENSA (
id_usuario INT,
id_recompensa INT,
data_recebimento DATE,
PRIMARY KEY (id_usuario, id_recompensa, data_recebimento),
FOREIGN KEY (id_usuario) REFERENCES USUARIO(id_usuario),
FOREIGN KEY (id_recompensa) REFERENCES RECOMPENSA(id_recompensa)
);
