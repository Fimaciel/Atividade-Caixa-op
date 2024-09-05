INSERT INTO funcionarios(nome, cpf) VALUES ("Lucas","9032213022");
INSERT INTO funcionarios(nome, cpf) VALUES ("João","12345678912");
select * from funcionarios;

INSERT INTO servicos(valor, descricao, tempo) VALUES (100.20, 'arrumar pc', '01:40:00');
INSERT INTO servicos(valor, descricao, tempo) VALUES (100, 'arrumar placa mãe', '01:20:00');
INSERT INTO servicos(valor, descricao, tempo) VALUES (120, 'Limpar Computador', '01:00:00');
INSERT INTO servicos(valor, descricao, tempo) VALUES (121, 'Formatar', '01:10:00');
INSERT INTO servicos(valor, descricao, tempo) VALUES (150, 'Formatar Celular', '01:10:00');
INSERT INTO servicos(valor, descricao, tempo) VALUES (150, 'Trocar tela', '01:10:00');
INSERT INTO servicos(valor, descricao, tempo) VALUES (200, 'Arrumar Televisão', '01:10:00');

UPDATE servicos SET descricao = "Arrumar Computador e Laptos" WHERE id = 1;


INSERT INTO dispositivos(aliquota, descricao, status_dispo) VALUES (2, 'MERCADO LIVRE', true);
INSERT INTO dispositivos(aliquota, descricao, status_dispo) VALUES (1, 'STONE', true);
INSERT INTO dispositivos(aliquota, descricao, status_dispo) VALUES (10, 'MASTECARD PICPAY', false);

INSERT INTO clientes (nome, cpf, email, telefone, data_nasc) VALUES ('João Silva', '12345678901', 'joao.silva@example.com', '11287654321', '1980-05-15');
INSERT INTO clientes (nome, cpf, email, telefone, data_nasc) VALUES ('chorão Lopes', '12342678901', 'joao.lopes@example.com', '11387654321', '1981-05-15');
INSERT INTO clientes (nome, cpf, email, telefone, data_nasc) VALUES ('Gavi Math', '12345378901', 'joao.math@example.com', '11487654321', '1990-05-15');

SELECT * FROM clientes;



Select * FROM Vendas as v inner join clientes as c ON v.id_cliente_fk = c.id;
Select * FROM Vendas as v left join clientes as c ON v.id_cliente_fk = c.id;
Select * FROM Vendas as v right join clientes as c ON v.id_cliente_fk = c.id;


Select valor_total, nome FROM Vendas as v inner join clientes as c ON v.id_cliente_fk = c.id;
Select desconto, valor_total, nome FROM Vendas as v left join clientes as c ON v.id_cliente_fk = c.id;
Select  nome, cpf FROM Vendas as v right join clientes as c ON v.id_cliente_fk = c.id;


INSERT INTO vendas (data_venda, hora, valor_total, desconto, valor_final, tipo, id_cliente_fk) VALUES ('2024-07-23', '10:30:00', 150.00, 10.00, 140.00, 'Cartão de Crédito', 1);
INSERT INTO vendas (data_venda, hora, valor_total, desconto, valor_final, tipo, id_cliente_fk) VALUES ('2028-07-23', '10:30:00', 24.00, 10.00, 14.00, 'PIX', 2);
INSERT INTO vendas (data_venda, hora, valor_total, desconto, valor_final, tipo, id_cliente_fk) VALUES ('2027-07-23', '10:30:00', 145.00, 10.00, 135.00, 'Cartão de Débito', null);
INSERT INTO vendas (data_venda, hora, valor_total, desconto, valor_final, tipo, id_cliente_fk) VALUES ('2026-07-23', '10:30:00', 150.00, 20.00, 130.00, 'Dinheiro', 3);
INSERT INTO vendas (data_venda, hora, valor_total, desconto, valor_final, tipo, id_cliente_fk) VALUES ('2025-07-23', '10:30:00', 150.00, 00.00, 150.00, 'PIX', null);


INSERT INTO vendasServicos (venda_fk, servico_fk) VALUES (1,1);
INSERT INTO vendasServicos (venda_fk, servico_fk) VALUES (2,3);
INSERT INTO vendasServicos (venda_fk, servico_fk) VALUES (1,4);
INSERT INTO vendasServicos (venda_fk, servico_fk) VALUES (3,4);
INSERT INTO vendasServicos (venda_fk, servico_fk) VALUES (2,4);
INSERT INTO vendasServicos (venda_fk, servico_fk) VALUES (4,4);


insert INTO caixas(saldo_inicial,total_entradas,total_saidas,status_caixa,func_fk ) VALUES (100, 120, 20, "Aberto", 1);
insert INTO caixas(saldo_inicial,total_entradas,total_saidas,status_caixa,func_fk ) VALUES (110, 220, 410, "fechado", 2);
insert INTO caixas(saldo_inicial,total_entradas,total_saidas,status_caixa,func_fk ) VALUES (110, 110, 30, "fechado", 2);



insert INTO despesas(valor, data_vencimento, data_pagamento, status_despesa, caixa_fk ) VALUES (110, '2025-08-23','2025-07-30', "fechado", 2);
insert INTO despesas(valor,data_vencimento,data_pagamento,status_despesa,caixa_fk ) VALUES (200, '2025-09-23',null, "aberto", 3);
insert INTO despesas(valor,data_vencimento,data_pagamento,status_despesa,caixa_fk ) VALUES (50, '2025-10-23', null, "aberto", 1);
insert INTO despesas(valor,data_vencimento,data_pagamento,status_despesa,caixa_fk ) VALUES (25, '2025-12-23',null, "aberto", 3);

insert INTO recebimentos(valor,data_vencimento,data_pagamento,status_recebimento,caixa_fk, venda_fk ) VALUES (24, '2025-12-23','2024-07-30', "aberto", 3,1);
insert INTO recebimentos(valor,data_vencimento,data_pagamento,status_recebimento,caixa_fk, venda_fk ) VALUES (24, '2025-12-23','2024-07-30', "fechado", 3,1);
insert INTO recebimentos(valor,data_vencimento,data_pagamento,status_recebimento,caixa_fk, venda_fk ) VALUES (250, '2024-12-23',null, "aberto", 1,2);
insert INTO recebimentos(valor,data_vencimento,data_pagamento,status_recebimento,caixa_fk, venda_fk ) VALUES (240, '2024-12-23','2023-12-23', "fechado", 1,1);

INSERT INTO encargos(valor, descricao, id_dispositivo_fk, recebimento_fk) value (10, 'Venda', 1,null);
INSERT INTO encargos(valor, descricao, id_dispositivo_fk, recebimento_fk) value (15, 'Venda', 1,2);
INSERT INTO encargos(valor, descricao, id_dispositivo_fk, recebimento_fk) value (14, 'Venda', 1,1);

INSERT INTO encargos(valor, descricao, id_dispositivo_fk, recebimento_fk) value (10, 'Venda', 1,null);
INSERT INTO encargos(valor, descricao, id_dispositivo_fk, recebimento_fk) value (15, 'Venda', 1,2);
INSERT INTO encargos(valor, descricao, id_dispositivo_fk, recebimento_fk) value (14, 'Venda', 1,1);


INSERT INTO vendasServicos (venda_fk, servico_fk,valor_unitario, Quantidade ) VALUES (3,4, 12, 1);
INSERT INTO vendasServicos (venda_fk, servico_fk,valor_unitario, Quantidade )  VALUES (2,4, 3, 4);
INSERT INTO vendasServicos (venda_fk, servico_fk,valor_unitario, Quantidade )  VALUES (4,4, 13, 2);


select * from servicos order by valor Desc;

select * from servicos where valor > 100;

select max(valor) as valor from servicos;
select min(valor) as valor from servicos;
select avg(valor) as valor from servicos;

select * from servicos where tempo > '01:10:30';



select nome, cpf from clientes;

select * from vendas as v inner join Clientes as c on v.id_cliente_fk = c.id ;
select * from vendas as v left join Clientes as c on v.id_cliente_fk = c.id ;
select * from vendas as v right join Clientes as c on v.id_cliente_fk = c.id ;
select nome, cpf from clientes union select nome, cpf from funcionarios;
select * from vendas;


select * from servicos where valor = (select max(valor) from servicos);
select * from servicos where valor > (select avg(valor) from servicos);

select * from vendasServicos where valor_unitario > (select avg(valor_unitario) from vendasServicos );


select * from recebimentos as r
 inner join caixas as c on r.caixa_fk = c.id
 inner join funcionario as f on  c.func_fk = f.id
 where (f.cpf = "9032213022" and r.status_recebimento = "fechado");
 

 update despesas SET fornecedor_fk = 1;
 
-- Selects

SELECT * FROM servicos;

UPDATE servicos SET valor = 50 WHERE id = 1;

SELECT vs.id, s.descricao, vs.valor_unitario, s.valor 
FROM vendasservicos AS vs 
INNER JOIN servicos AS s ON vs.servico_fk = s.id;

SELECT v.id, v.valor_total, c.nome 
FROM vendas AS v 
LEFT JOIN clientes AS c ON v.id_cliente_fk = c.id;

SELECT * 
FROM despesas AS d 
INNER JOIN fornecedores AS f ON d.fornecedor_fk = f.id;

SELECT f.nome 
FROM caixas AS c 
INNER JOIN funcionarios AS f ON c.func_fk = f.id;

SELECT * FROM servicos;

SELECT * FROM clientes WHERE nome = 'filipe';
