use facturacion
go


delete articulos

INSERT INTO articulos(nombre, pre_unitario, dado_baja) values('Mesa',3500,'N')
INSERT INTO articulos(nombre, pre_unitario,dado_baja) values('Placard',7800,'N')
INSERT INTO articulos(nombre, pre_unitario,dado_baja) values('Silla',1500,'N')
INSERT INTO articulos(nombre, pre_unitario,dado_baja) values('Escritorio',6500,'N')
INSERT INTO articulos(nombre, pre_unitario,dado_baja) values('Biblioteca',8000,'N')
INSERT INTO articulos(nombre, pre_unitario,dado_baja) values('Vajillero',9900,'N')

select * from facturas
select* from detalles_factura
select*from articulos

select * from formas_pago

insert into formas_pago(nombre) values ('Contado')
insert into formas_pago(nombre) values ('Tarjeta de credito')
insert into formas_pago(nombre) values ('Tarjeta de debito')
insert into formas_pago(nombre) values ('Mercado Pago')


