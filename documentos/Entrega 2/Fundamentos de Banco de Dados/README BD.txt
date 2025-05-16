****README*****
Banco de Dados Código


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