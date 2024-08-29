create database financeiroDB;
use financeiroDB;

-- tabelas
CREATE TABLE funcionarios(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    cpf VARCHAR(11) NOT NULL
); 

CREATE TABLE servicos(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
    valor DECIMAL NOT NULL,
    descricao VARCHAR(100) NOT NULL,
    tempo TIME NOT NULL
);

CREATE TABLE dispositivos(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
	aliquota DOUBLE NOT NULL,
    descricao VARCHAR (100) NOT NULL,
    status_dispo BOOLEAN NOT NULL
);

CREATE TABLE clientes(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
	nome VARCHAR(100) NOT NULL,
	cpf VARCHAR(11) NOT NULL,
	email VARCHAR(100) NOT NULL,
	telefone VARCHAR(50) NOT NULL,
	data_nasc DATE NOT NULL
);

CREATE TABLE vendas (
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
    data_venda DATE NOT NULL,
    hora TIME NOT NULL,
    valor_total DECIMAL NOT NULL,
    desconto DECIMAL NOT NULL,
    valor_final DECIMAL NOT NULL,
	tipo VARCHAR(100) NOT NULL,
    id_cliente_fk INTEGER NOT NULL,
	FOREIGN KEY (id_cliente_fk) REFERENCES clientes (id)
);

CREATE TABLE vendasServicos (
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
	venda_fk  INTEGER NOT NULL,
	servico_fk  INTEGER NOT NULL,
	FOREIGN KEY (venda_fk) REFERENCES vendas (id) ON DELETE CASCADE,
	FOREIGN KEY (servico_fk) REFERENCES servicos (id) 
);

CREATE TABLE caixas(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
    saldo_inicial DECIMAL NOT NULL, 
    total_entradas INTEGER NOT NULL, 
    total_saidas INTEGER NOT NULL, 
    status_caixa VARCHAR(100) NOT NULL,
    func_fk INTEGER NOT null,
	FOREIGN KEY (func_fk) REFERENCES funcionarios (id)
);

CREATE TABLE despesas(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
	valor DECIMAL NOT NULL, 
    data_vencimento DATE NOT NULL,
    data_pagamento DATE,
    status_despesa VARCHAR(100) NOT NULL,
	caixa_fk  INTEGER NOT NULL,
	FOREIGN KEY (caixa_fk) REFERENCES caixas (id)
);

CREATE TABLE recebimentos(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
	valor DECIMAL NOT NULL, 
    data_vencimento DATE NOT NULL,
    data_pagamento DATE NOT NULL,
    status_recebimento VARCHAR(100) NOT NULL,
	caixa_fk  INTEGER NOT NULL,
	FOREIGN KEY (caixa_fk) REFERENCES caixas (id),
	venda_fk  INTEGER NOT NULL,
	FOREIGN KEY (venda_fk) REFERENCES vendas (id)
);

CREATE TABLE encargos(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
    valor DECIMAL NOT NULL,
    descricao VARCHAR(100) NOT NULL,
    id_dispositivo_fk INTEGER NOT NULL,
    FOREIGN KEY (id_dispositivo_fk) REFERENCES dispositivos (id),
	recebimento_fk  INTEGER,
	FOREIGN KEY (recebimento_fk) REFERENCES recebimentos (id) ON DELETE CASCADE
);

CREATE TABLE fornecedores(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
    razao_social  VARCHAR(150) NOT NULL,
	nome_fantasia  VARCHAR(150) NOT NULL,
    ativo BOOLEAN NOT NULL,
    atividade_economica VARCHAR(250) NOT NULL,
    telefone VARCHAR (20) NOT NULL,
    email VARCHAR (150) NOT NULL
    );
   

-- Alters e Drops

ALTER TABLE caixas ADD saldo_total decimal;

ALTER TABLE vendasServicos ADD (valor_unitario DECIMAL, Quantidade INTEGER);

ALTER TABLE vendas CHANGE id_cliente_fk  id_cliente_fk  INTEGER ;
ALTER TABLE recebimentos CHANGE venda_fk  venda_fk  INTEGER;
ALTER TABLE despesas CHANGE caixa_fk  caixa_fk  INTEGER;
ALTER TABLE despesas CHANGE data_pagamento  data_pagamento DATE;
ALTER TABLE recebimentos CHANGE data_pagamento  data_pagamento DATE;

ALTER TABLE despesas ADD fornecedor_fk INTEGER;
ALTER TABLE despesas ADD foreign key (fornecedor_fk) REFERENCES fornecedores(id);
 


