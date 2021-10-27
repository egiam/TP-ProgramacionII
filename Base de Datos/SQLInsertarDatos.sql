use facturacion
go

insert into articulos (nombre, pre_unitario)
values ('silla', 2000)

insert into articulos (nombre, pre_unitario)
values ('mesa', 3500)

insert into articulos (nombre, pre_unitario)
values ('alacena', 7000)

select * from facturas
select* from detalles_factura
select*from articulos

select * from formas_pago

insert into formas_pago(nombre) values ('Contado')
insert into formas_pago(nombre) values ('Tarjeta de credito')
insert into formas_pago(nombre) values ('Tarjeta de debito')
insert into formas_pago(nombre) values ('Mercado Pago')


