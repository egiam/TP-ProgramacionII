
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

CREATE PROCEDURE [dbo].[SP_INSERTAR_FACTURA] 
	@cliente varchar(255), 
	@forma int,
	@nro_factura int
AS
BEGIN
	INSERT INTO facturas(nro_factura, fecha, cliente, id_forma_pago)
	VALUES (@nro_factura, GETDATE(), @cliente, @forma);
END
GO



CREATE PROCEDURE [dbo].[SP_INSERTAR_DETALLES] 
	@nro_factura int,
	@id_articulo int, 
	@cantidad int
AS
BEGIN
	INSERT INTO detalles_factura(nro_factura, id_articulo, cantidad)
    VALUES (@nro_factura, @id_articulo, @cantidad);
  
END

GO

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