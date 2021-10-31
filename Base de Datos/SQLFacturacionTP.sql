
--Creacion de Data Base

create database facturacion
go

use facturacion
go


create table Users
(
	UserID int identity(1,1) primary key,
	LoginName varchar (100) unique not null,
	Password varchar (100) not null,
	FirstName varchar(100) not null,
	LastName varchar(100) not null,
	Email varchar(150)not null,
	Position varchar(100)
) 


	insert into Users values ('admin','admin','Jackson','Collins','Support@SystemAll.biz','Administrador')
	insert into Users values ('Ben','abc123456','Benjamin','Thompson','BenThompson@MyCompany.com','')                                                         
	insert into Users values ('Kathy','abc123456','Kathrine','Smith','KathySmith@MyCompany.com','')


create table formas_pago
(
	id_forma_pago int identity(1,1)
	Constraint pk_forma_pago primary key (id_forma_pago),
	nombre varchar(75)
)

create table facturas
(
	nro_factura int, 
	Constraint pk_factura primary key (nro_factura),
	fecha datetime,
	id_forma_pago int,
	fecha_baja date null,
	total decimal(12,2) not null,
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

create proc pa_Registrar_Users
	@LoginName varchar(100),
	@Password varchar(100),
	@FirstName varchar(100),
	@LastName varchar(100),
	@Email varchar(150),
	@Position varchar(100) = 'Empleado'
as
begin
	insert into Users values 
	(
		@LoginName,
		@Password,
		@FirstName,
		@LastName,
		@Email,
		@Position
	)
end
go

--No se puede tener el mismo nombre de usuario

exec pa_Registrar_Users 'egiam','fernandez357','Ezequiel','Giampaoli','ezegiampaoli@gmail.com','Gerente'
exec pa_Registrar_Users 'sampaoli','fernandez357','Ezequiel','Giampaoli','ezegiampaoli@gmail.com'
exec pa_Registrar_Users 'fabio','FABIOCA','Fabio','Caceres','fabioCC@gmail.com'

select * from Users





CREATE PROCEDURE [dbo].[SP_PROXIMO_ID]
@next int OUTPUT
AS
BEGIN
	SET @next = (SELECT MAX(nro_factura)+1  FROM facturas);
	IF @next is null
	set @next=1 

END


GO

alter PROCEDURE [dbo].[SP_INSERTAR_FACTURA] 
	@cliente varchar(255), 
	@forma int,
	@nro_factura int,
	@total decimal(10,2),
	@fecha datetime
AS
BEGIN
	INSERT INTO facturas(nro_factura, fecha, cliente, id_forma_pago, total)
	VALUES (@nro_factura, GETDATE(), @cliente, @forma, @total);
END
GO



alter PROCEDURE [dbo].[SP_INSERTAR_DETALLES] 
	@nro_factura int,
	@id_articulo int, 
	@cantidad int,
	@id_detalle int
AS
BEGIN
	INSERT INTO detalles_factura(nro_factura, id_articulo, cantidad)
    VALUES (@nro_factura, @id_articulo, @cantidad);
  
END
select * from facturas

GO
------

alter PROCEDURE [dbo].[SP_EDITAR_FACTURA] 
	@cliente varchar(255), 
	@forma int,
	@nro_factura int,
	@total decimal(10,2)
	--@fecha datetime
AS

	UPDATE facturas
	SET  cliente=@cliente,id_forma_pago=@forma,total=@total
	WHERE nro_factura=@nro_factura

GO
exec [dbo].[SP_EDITAR_FACTURA]  'Luis', 1, 1, 3500

select * from facturas

	UPDATE facturas
	SET  cliente='pepe', id_forma_pago=2, total=3500
	WHERE nro_factura=1

CREATE PROCEDURE [dbo].[SP_EDITAR_DETALLES] 
	@nro_factura int,
	@id_articulo int, 
	@cantidad int,
	@id_detalle int
AS
BEGIN
	UPDATE detalles_factura
	SET id_articulo=@id_articulo, cantidad=@cantidad
	WHERE nro_factura=@nro_factura and id_detalle=@id_detalle 
END

GO
-----

CREATE PROCEDURE [dbo].[SP_CONSULTAR_ARTICULOS]
AS
BEGIN
	
	SELECT * from articulos ORDER BY id_articulo;
END
GO

create PROCEDURE [dbo].[SP_CONSULTAR_FORMAS_DE_PAGO]
AS
BEGIN
	
	SELECT * from formas_pago ORDER BY id_forma_pago;
END
GO
exec SP_CONSULTAR_FORMAS_DE_PAGO


CREATE PROCEDURE [dbo].[SP_PROXIMO_ID_ART]
@next int OUTPUT
AS
BEGIN
	SET @next = (SELECT MAX(id_articulo)+1  FROM articulos);
	IF @next is null
	set @next=1 

END


GO


CREATE PROCEDURE [dbo].[SP_INSERTAR_ARTICULO] 
	@nombre varchar(255), 
	@precio decimal(10,2)
AS
BEGIN
	INSERT INTO articulos( nombre, pre_unitario)
	VALUES (@nombre, @precio);
END
GO

exec SP_INSERTAR_ARTICULO 'Cama', 15000
select * from articulos

GO


alter PROCEDURE [dbo].[SP_CONSULTAR_FACTURAS] 
	@fecha_desde datetime = null,
	@fecha_hasta datetime = null,
	@cliente varchar(255) =null,
	@datos_baja varchar(1) = null
AS
BEGIN
print @fecha_hasta
	SELECT * FROM facturas f
	join formas_pago fp on f.id_forma_pago=fp.id_forma_pago
	WHERE 
	 ((@fecha_desde is null and @fecha_hasta is null) OR (fecha >= @fecha_desde and fecha <= @fecha_hasta))
	 AND(@cliente is null OR (cliente like '%' + @cliente + '%'))
	 AND (@datos_baja is null OR (@datos_baja = 'S') OR (@datos_baja = 'N' and fecha_baja is  null))
	 
END
GO

exec SP_CONSULTAR_FACTURAS @fecha_desde = '30/10/2021',@fecha_hasta = '31/10/2021 12:50:21'

alter PROCEDURE [dbo].[SP_CONSULTAR_FACTURA_POR_ID]
	@id int	
AS
BEGIN
	SELECT f.nro_factura, fecha, f.id_forma_pago,f.fecha_baja, f.total, cliente,d.id_articulo,cantidad,
	a.nombre 'nombre_articulo',pre_unitario,fp.nombre 'nombre_forma_pago' 
	FROM facturas f, detalles_factura d, articulos a, formas_pago fp
	WHERE f.nro_factura = d.nro_factura
	AND f.id_forma_pago=fp.id_forma_pago
	AND d.id_articulo = a.id_articulo
	AND f.nro_factura = @id;
END
GO
exec [SP_CONSULTAR_FACTURA_POR_ID] 4

SELECT * from facturas
select * from detalles_factura
select * from articulos
