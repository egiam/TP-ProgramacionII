use facturacion
go

insert into articulos (nombre, pre_unitario)
values ('silla', 2000)

insert into articulos (nombre, pre_unitario)
values ('mesa', 3500)

insert into articulos (nombre, pre_unitario)
values ('alacena', 7000)

delete articulos

INSERT INTO articulos(nombre, pre_unitario) values('Mesa',3500)
INSERT INTO articulos(nombre, pre_unitario) values('Placard',7800)
INSERT INTO articulos(nombre, pre_unitario) values('Silla',1500)
INSERT INTO articulos(nombre, pre_unitario) values('Escritorio',6500)
INSERT INTO articulos(nombre, pre_unitario) values('Biblioteca',8000)
INSERT INTO articulos(nombre, pre_unitario) values('Vajillero',9900)

select * from facturas
select* from detalles_factura
select*from articulos

select * from formas_pago

insert into formas_pago(nombre) values ('Contado')
insert into formas_pago(nombre) values ('Tarjeta de credito')
insert into formas_pago(nombre) values ('Tarjeta de debito')
insert into formas_pago(nombre) values ('Mercado Pago')


