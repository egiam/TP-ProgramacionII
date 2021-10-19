
--Creacion de Data Base

create database facturacion
go

use facturacion
go


create table formas_pago
(
	id_forma_pago int identity(1,1)
	Constraint pk_forma_pago primary key (id_forma_pago),
	nombre varchar(75)
)

create table facturas
(
	nro_factura int identity(1,1)
	Constraint pk_factura primary key (nro_factura),
	fecha datetime,
	id_forma_pago int
	Constraint fk_facturas_forma_pago foreign key(id_forma_pago)
	References formas_pago (id_forma_pago),
	cliente varchar(75)
)

create table articulos 
(
	id_articulo int identity(1,1)
	Constraint pk_articulo primary key(id_articulo),
	nombre varchar(75),
	pre_unitario decimal(10,2)
)

create table detalles_factura
(
	id_detalle int identity(1,1)
	Constraint pk_detalle primary key (id_detalle),
	nro_factura int
	Constraint fk_detalle_factura foreign key (nro_factura)
	References facturas (nro_factura),
	id_articulo int
	Constraint fk_detalle_articulo foreign key (id_articulo)
	References articulos (id_articulo),
	cantidad int
)


--Insertacion de datos



--Productos almacenados, vistas, etc

