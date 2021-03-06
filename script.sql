USE [master]
GO
/****** Object:  Database [Parking]    Script Date: 19/07/2021 11:37:54 ******/
CREATE DATABASE [Parking]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Parking', FILENAME = N'C:\Users\mmena\Parking.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Parking_log', FILENAME = N'C:\Users\mmena\Parking_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Parking] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Parking].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Parking] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Parking] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Parking] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Parking] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Parking] SET ARITHABORT OFF 
GO
ALTER DATABASE [Parking] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Parking] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Parking] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Parking] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Parking] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Parking] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Parking] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Parking] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Parking] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Parking] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Parking] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Parking] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Parking] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Parking] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Parking] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Parking] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Parking] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Parking] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Parking] SET  MULTI_USER 
GO
ALTER DATABASE [Parking] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Parking] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Parking] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Parking] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Parking] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Parking]
GO
/****** Object:  Table [dbo].[Abonados]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Abonados](
	[id_abonado] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[dominio] [varchar](50) NULL,
	[id_tipoVehiculo] [int] NOT NULL,
	[telefono] [bigint] NULL,
	[dni] [bigint] NULL,
	[id_tipoAbono] [int] NOT NULL,
 CONSTRAINT [PK_Abonados] PRIMARY KEY CLUSTERED 
(
	[id_abonado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clientes](
	[dominio] [varchar](50) NULL,
	[codigo] [int] NULL,
	[fecha] [datetime] NULL,
	[id_tipoVehiculo] [int] NULL,
	[estado] [bit] NULL,
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [pk_id_cliente] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Detalles_Pago]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalles_Pago](
	[id_detallePago] [int] IDENTITY(1,1) NOT NULL,
	[total] [real] NOT NULL,
 CONSTRAINT [PK_Detalles_Pago] PRIMARY KEY CLUSTERED 
(
	[id_detallePago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Detalles_Venta]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalles_Venta](
	[importe] [real] NULL,
	[id_detalle_venta] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [pk_id_detalle_venta] PRIMARY KEY CLUSTERED 
(
	[id_detalle_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gastos]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gastos](
	[detalle] [varchar](50) NULL,
	[monto] [float] NULL,
	[id_gasto] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [pk_id_gasto] PRIMARY KEY CLUSTERED 
(
	[id_gasto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pagos_Abonos]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos_Abonos](
	[id_pago] [int] IDENTITY(1,1) NOT NULL,
	[id_abonado] [int] NOT NULL,
	[fecha] [date] NULL,
	[id_detallePago] [int] NOT NULL,
	[id_periodo] [int] NOT NULL,
 CONSTRAINT [PK_Pagos_Abonos] PRIMARY KEY CLUSTERED 
(
	[id_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Periodos]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Periodos](
	[id_periodo] [int] IDENTITY(1,1) NOT NULL,
	[periodo] [varchar](50) NULL,
 CONSTRAINT [PK_Periodos] PRIMARY KEY CLUSTERED 
(
	[id_periodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tarifas]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarifas](
	[id_tarifa] [int] IDENTITY(1,1) NOT NULL,
	[precio] [float] NULL,
	[id_tipo_vehiculo] [int] NULL,
 CONSTRAINT [pk_id_tarifa] PRIMARY KEY CLUSTERED 
(
	[id_tarifa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[id_ticket] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
	[id_cliente] [int] NULL,
	[id_detalle_venta] [int] NULL,
 CONSTRAINT [pk_id_ticket] PRIMARY KEY CLUSTERED 
(
	[id_ticket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tipos_abonos]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tipos_abonos](
	[id_tipoAbono] [int] IDENTITY(1,1) NOT NULL,
	[tipoAbono] [varchar](50) NOT NULL,
	[monto] [real] NOT NULL,
 CONSTRAINT [PK_Tipos_abonos] PRIMARY KEY CLUSTERED 
(
	[id_tipoAbono] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tipos_de_Vehiculos]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tipos_de_Vehiculos](
	[id_tipoVehiculo] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](50) NULL,
 CONSTRAINT [PK_Tipos_de_Vehiculos] PRIMARY KEY CLUSTERED 
(
	[id_tipoVehiculo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tipos_Estadia]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tipos_Estadia](
	[tipo] [varchar](50) NULL,
	[monto] [real] NULL,
	[id_tipo_estadia] [int] IDENTITY(1,1) NOT NULL,
	[horas] [int] NULL,
 CONSTRAINT [pk_id_tipo_estadia] PRIMARY KEY CLUSTERED 
(
	[id_tipo_estadia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](50) NOT NULL,
	[password] [varchar](50) NULL,
	[admin] [bit] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ventas]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ventas](
	[horaEgreso] [datetime] NULL,
	[id_detalle_venta] [int] NULL,
	[id_venta] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NULL,
 CONSTRAINT [pk_id_venta] PRIMARY KEY CLUSTERED 
(
	[id_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ventas_Estadias]    Script Date: 19/07/2021 11:37:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ventas_Estadias](
	[dominio] [varchar](50) NOT NULL,
	[id_tipo_estadia] [int] NULL,
	[id_venta_estadia] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
	[fecha_egreso] [datetime] NULL,
 CONSTRAINT [pk_id_estadia] PRIMARY KEY CLUSTERED 
(
	[id_venta_estadia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Abonados] ON 

INSERT [dbo].[Abonados] ([id_abonado], [nombre], [apellido], [dominio], [id_tipoVehiculo], [telefono], [dni], [id_tipoAbono]) VALUES (2, N'carlos', N'ssiidu', N'aad485', 1, 484986186, 36985741, 1)
INSERT [dbo].[Abonados] ([id_abonado], [nombre], [apellido], [dominio], [id_tipoVehiculo], [telefono], [dni], [id_tipoAbono]) VALUES (3, N'maxi', N'mena', N'WE78', 3, 444447, 8486, 1)
INSERT [dbo].[Abonados] ([id_abonado], [nombre], [apellido], [dominio], [id_tipoVehiculo], [telefono], [dni], [id_tipoAbono]) VALUES (1003, N'awwad', N'dddddddd', N'123213', 1, 3123213, 123, 1)
SET IDENTITY_INSERT [dbo].[Abonados] OFF
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'ad486', 0, CAST(N'2020-12-01 12:21:17.973' AS DateTime), 2, 0, 1)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'wer488', 0, CAST(N'2020-12-01 15:53:25.850' AS DateTime), 3, 0, 2)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'sdf888', 0, CAST(N'2020-12-03 13:42:28.817' AS DateTime), 2, 0, 3)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'sudh', NULL, CAST(N'2020-01-11 12:55:10.000' AS DateTime), 2, 0, 4)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'rer555', 0, CAST(N'2020-12-03 13:57:13.760' AS DateTime), 3, 0, 5)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'f8', 0, CAST(N'2020-12-03 14:13:23.670' AS DateTime), 2, 0, 6)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'f8', 0, CAST(N'2020-12-03 14:13:23.670' AS DateTime), 2, 1, 7)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'e7', 0, CAST(N'2020-12-03 14:17:05.760' AS DateTime), 2, 0, 8)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'gh487', 0, CAST(N'2020-12-03 16:09:48.970' AS DateTime), 1, 0, 9)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'ghs963', 0, CAST(N'2020-12-03 16:10:26.397' AS DateTime), 1, 0, 10)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'tre999', 0, CAST(N'2020-12-03 16:11:00.157' AS DateTime), 1, 0, 11)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'yu741', 0, CAST(N'2020-12-03 16:12:07.367' AS DateTime), 1, 0, 12)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'ty89', 0, CAST(N'2020-12-03 16:18:42.777' AS DateTime), 1, 0, 13)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'frt879', 0, CAST(N'2020-12-03 19:00:17.347' AS DateTime), 3, 0, 1005)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'd8d8', 0, CAST(N'2021-06-23 15:18:37.620' AS DateTime), 2, 0, 2005)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'rt48w', 0, CAST(N'2021-06-23 15:50:30.080' AS DateTime), 1, 0, 2006)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N't8g4', 0, CAST(N'2021-06-23 15:52:03.787' AS DateTime), 2, 0, 2007)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'frs8w', 0, CAST(N'2021-06-23 15:55:30.887' AS DateTime), 2, 1, 2008)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'fre8w7', 0, CAST(N'2021-06-23 16:00:07.940' AS DateTime), 2, 1, 2009)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N's8w78d', 0, CAST(N'2021-06-23 16:04:41.063' AS DateTime), 3, 0, 2010)
INSERT [dbo].[Clientes] ([dominio], [codigo], [fecha], [id_tipoVehiculo], [estado], [id_cliente]) VALUES (N'izf1900', 0, CAST(N'2021-07-05 16:09:54.203' AS DateTime), 1, 0, 3005)
SET IDENTITY_INSERT [dbo].[Clientes] OFF
SET IDENTITY_INSERT [dbo].[Detalles_Pago] ON 

INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (1, 5500)
INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (2, 4884)
INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (1002, 880)
INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (1003, 880)
INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (1004, 880)
INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (1005, 880)
INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (1006, 123123)
INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (1007, 1231)
INSERT [dbo].[Detalles_Pago] ([id_detallePago], [total]) VALUES (1008, 980)
SET IDENTITY_INSERT [dbo].[Detalles_Pago] OFF
SET IDENTITY_INSERT [dbo].[Detalles_Venta] ON 

INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (450, 9)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (452, 10)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (26, 1002)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (27, 1003)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (1255, 1004)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (0, 1005)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (0, 1006)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (76, 1007)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (4, 1008)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (11, 1009)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (1, 1010)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (3, 1011)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (0, 1012)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (0, 1013)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (39, 1014)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (1170, 2010)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (350, 3010)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (1640, 4010)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (1640, 4011)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (2150, 4012)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (1840, 4013)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (100, 4014)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (120, 4015)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (1850, 5010)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (720, 5011)
INSERT [dbo].[Detalles_Venta] ([importe], [id_detalle_venta]) VALUES (1560, 5012)
SET IDENTITY_INSERT [dbo].[Detalles_Venta] OFF
SET IDENTITY_INSERT [dbo].[Gastos] ON 

INSERT [dbo].[Gastos] ([detalle], [monto], [id_gasto], [fecha]) VALUES (N'me compre ropa', 980, 2, CAST(N'2021-07-19 09:46:36.123' AS DateTime))
INSERT [dbo].[Gastos] ([detalle], [monto], [id_gasto], [fecha]) VALUES (N'el gasto de junio', 675, 3, CAST(N'2021-06-05 10:46:36.123' AS DateTime))
INSERT [dbo].[Gastos] ([detalle], [monto], [id_gasto], [fecha]) VALUES (N'el gasto de enero de 2020', 675, 4, CAST(N'2020-01-12 12:46:36.123' AS DateTime))
SET IDENTITY_INSERT [dbo].[Gastos] OFF
SET IDENTITY_INSERT [dbo].[Pagos_Abonos] ON 

INSERT [dbo].[Pagos_Abonos] ([id_pago], [id_abonado], [fecha], [id_detallePago], [id_periodo]) VALUES (1, 1003, CAST(N'2021-03-02' AS Date), 2, 2)
INSERT [dbo].[Pagos_Abonos] ([id_pago], [id_abonado], [fecha], [id_detallePago], [id_periodo]) VALUES (2, 2, CAST(N'2021-07-05' AS Date), 1002, 2)
INSERT [dbo].[Pagos_Abonos] ([id_pago], [id_abonado], [fecha], [id_detallePago], [id_periodo]) VALUES (3, 2, CAST(N'2021-07-05' AS Date), 1008, 2)
SET IDENTITY_INSERT [dbo].[Pagos_Abonos] OFF
SET IDENTITY_INSERT [dbo].[Periodos] ON 

INSERT [dbo].[Periodos] ([id_periodo], [periodo]) VALUES (1, N'enero')
INSERT [dbo].[Periodos] ([id_periodo], [periodo]) VALUES (2, N'febrero')
INSERT [dbo].[Periodos] ([id_periodo], [periodo]) VALUES (3, N'marzo')
INSERT [dbo].[Periodos] ([id_periodo], [periodo]) VALUES (4, N'abril')
INSERT [dbo].[Periodos] ([id_periodo], [periodo]) VALUES (5, N'mayo')
SET IDENTITY_INSERT [dbo].[Periodos] OFF
SET IDENTITY_INSERT [dbo].[Tarifas] ON 

INSERT [dbo].[Tarifas] ([id_tarifa], [precio], [id_tipo_vehiculo]) VALUES (1, 85, 1)
INSERT [dbo].[Tarifas] ([id_tarifa], [precio], [id_tipo_vehiculo]) VALUES (2, 129, 2)
SET IDENTITY_INSERT [dbo].[Tarifas] OFF
SET IDENTITY_INSERT [dbo].[Tickets] ON 

INSERT [dbo].[Tickets] ([id_ticket], [fecha], [id_cliente], [id_detalle_venta]) VALUES (1, CAST(N'2020-12-03 16:49:08.117' AS DateTime), 9, 1014)
INSERT [dbo].[Tickets] ([id_ticket], [fecha], [id_cliente], [id_detalle_venta]) VALUES (2, CAST(N'2020-12-08 14:30:10.000' AS DateTime), 1005, 2010)
INSERT [dbo].[Tickets] ([id_ticket], [fecha], [id_cliente], [id_detalle_venta]) VALUES (1002, CAST(N'2021-07-05 12:24:20.333' AS DateTime), 2005, 4012)
INSERT [dbo].[Tickets] ([id_ticket], [fecha], [id_cliente], [id_detalle_venta]) VALUES (1003, CAST(N'2021-07-05 16:27:16.863' AS DateTime), 3005, 4015)
INSERT [dbo].[Tickets] ([id_ticket], [fecha], [id_cliente], [id_detalle_venta]) VALUES (2002, CAST(N'2021-07-07 10:20:33.167' AS DateTime), 2007, 5010)
INSERT [dbo].[Tickets] ([id_ticket], [fecha], [id_cliente], [id_detalle_venta]) VALUES (2003, CAST(N'2021-07-07 10:46:33.640' AS DateTime), 2010, 5011)
INSERT [dbo].[Tickets] ([id_ticket], [fecha], [id_cliente], [id_detalle_venta]) VALUES (2004, CAST(N'2021-07-07 10:52:49.537' AS DateTime), 2006, 5012)
SET IDENTITY_INSERT [dbo].[Tickets] OFF
SET IDENTITY_INSERT [dbo].[Tipos_abonos] ON 

INSERT [dbo].[Tipos_abonos] ([id_tipoAbono], [tipoAbono], [monto]) VALUES (1, N'Abono Barato', 450)
INSERT [dbo].[Tipos_abonos] ([id_tipoAbono], [tipoAbono], [monto]) VALUES (2, N'abono saladix', 2484)
INSERT [dbo].[Tipos_abonos] ([id_tipoAbono], [tipoAbono], [monto]) VALUES (1002, N'nuevo abono', 547)
SET IDENTITY_INSERT [dbo].[Tipos_abonos] OFF
SET IDENTITY_INSERT [dbo].[Tipos_de_Vehiculos] ON 

INSERT [dbo].[Tipos_de_Vehiculos] ([id_tipoVehiculo], [tipo]) VALUES (1, N'Auto')
INSERT [dbo].[Tipos_de_Vehiculos] ([id_tipoVehiculo], [tipo]) VALUES (2, N'Pick-up')
INSERT [dbo].[Tipos_de_Vehiculos] ([id_tipoVehiculo], [tipo]) VALUES (3, N'Moto')
SET IDENTITY_INSERT [dbo].[Tipos_de_Vehiculos] OFF
SET IDENTITY_INSERT [dbo].[Tipos_Estadia] ON 

INSERT [dbo].[Tipos_Estadia] ([tipo], [monto], [id_tipo_estadia], [horas]) VALUES (N'4 horas', 300, 1, 4)
INSERT [dbo].[Tipos_Estadia] ([tipo], [monto], [id_tipo_estadia], [horas]) VALUES (N'8 horas', 500, 2, 8)
INSERT [dbo].[Tipos_Estadia] ([tipo], [monto], [id_tipo_estadia], [horas]) VALUES (N'Diurno', 1114, 3, 12)
INSERT [dbo].[Tipos_Estadia] ([tipo], [monto], [id_tipo_estadia], [horas]) VALUES (N'Nocturno', 255, 4, 12)
SET IDENTITY_INSERT [dbo].[Tipos_Estadia] OFF
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([id_usuario], [usuario], [password], [admin]) VALUES (1, N'eze', N'123', 1)
INSERT [dbo].[Usuarios] ([id_usuario], [usuario], [password], [admin]) VALUES (2, N'maxi', N'11111', 0)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
SET IDENTITY_INSERT [dbo].[Ventas] ON 

INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-01-12 19:51:47.000' AS DateTime), 9, 3, NULL)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-01-12 19:53:29.000' AS DateTime), 10, 4, NULL)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 12:47:41.000' AS DateTime), 1002, 1002, NULL)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 12:48:06.000' AS DateTime), 1003, 1003, NULL)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 12:48:50.000' AS DateTime), 1004, 1004, NULL)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 13:42:57.000' AS DateTime), 1005, 1005, NULL)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 13:57:22.000' AS DateTime), 1006, 1006, 5)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 14:11:37.000' AS DateTime), 1007, 1007, 4)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 14:17:17.000' AS DateTime), 1008, 1008, 6)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 14:28:26.000' AS DateTime), 1009, 1009, 8)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 16:13:09.000' AS DateTime), 1010, 1010, 12)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 16:14:12.000' AS DateTime), 1011, 1011, 11)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (NULL, 1012, 1012, 10)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 16:18:54.000' AS DateTime), 1013, 1013, 13)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-03-12 16:48:57.000' AS DateTime), 1014, 1014, 9)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2020-08-12 14:30:07.000' AS DateTime), 2010, 2006, 1005)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-05-07 12:22:42.000' AS DateTime), 4010, 4006, 9)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-05-07 12:23:45.000' AS DateTime), 4011, 4007, 13)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-05-07 12:24:07.000' AS DateTime), 4012, 4008, 2005)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-05-07 15:52:33.000' AS DateTime), 4013, 4009, 10)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-05-07 15:59:19.000' AS DateTime), 4014, 4010, 6)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-05-07 16:27:13.000' AS DateTime), 4015, 4011, 3005)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-07-07 10:20:31.000' AS DateTime), 5010, 5006, 2007)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-07-07 10:46:31.000' AS DateTime), 5011, 5007, 2010)
INSERT [dbo].[Ventas] ([horaEgreso], [id_detalle_venta], [id_venta], [id_cliente]) VALUES (CAST(N'2021-07-07 10:52:47.000' AS DateTime), 5012, 5008, 2006)
SET IDENTITY_INSERT [dbo].[Ventas] OFF
SET IDENTITY_INSERT [dbo].[Ventas_Estadias] ON 

INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'izf4938', 1, 6, CAST(N'2021-07-01 15:47:15.050' AS DateTime), CAST(N'2021-07-01 19:47:15.047' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'izf190', 2, 7, CAST(N'2021-07-01 15:56:57.040' AS DateTime), CAST(N'2021-07-01 23:56:57.033' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'duf73', 1, 8, CAST(N'2021-07-01 16:22:23.903' AS DateTime), CAST(N'2021-07-01 20:22:23.903' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'awd24', 1, 9, CAST(N'2021-07-01 16:24:25.820' AS DateTime), CAST(N'2021-07-01 20:24:25.820' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'awd24', 1, 10, CAST(N'2021-07-01 16:25:07.730' AS DateTime), CAST(N'2021-07-01 20:25:07.730' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'dwd2', 1, 11, CAST(N'2021-07-01 16:25:23.747' AS DateTime), CAST(N'2021-07-01 20:25:23.747' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'dwd2', 1, 12, CAST(N'2021-07-01 16:25:38.250' AS DateTime), CAST(N'2021-07-01 20:25:38.250' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'asdasd', 1, 13, CAST(N'2021-07-01 16:25:52.347' AS DateTime), CAST(N'2021-07-01 20:25:52.347' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'asdasd', 1, 14, CAST(N'2021-07-01 16:28:09.120' AS DateTime), CAST(N'2021-07-01 20:28:09.120' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'asdaw22', 1, 15, CAST(N'2021-07-01 16:28:19.580' AS DateTime), CAST(N'2021-07-01 20:28:19.580' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'afaw3', 2, 16, CAST(N'2021-07-01 16:34:23.197' AS DateTime), CAST(N'2021-07-02 00:34:23.197' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'fdbd455', 1, 17, CAST(N'2021-07-01 16:36:24.497' AS DateTime), CAST(N'2021-07-01 20:36:24.497' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'htr44', 1, 18, CAST(N'2021-07-01 16:37:50.303' AS DateTime), CAST(N'2021-07-01 20:37:50.303' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'efr43', 1, 19, CAST(N'2021-07-01 16:38:46.413' AS DateTime), CAST(N'2021-07-01 20:38:46.413' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'awd244', 1, 20, CAST(N'2021-07-01 16:39:31.650' AS DateTime), CAST(N'2021-07-01 20:39:31.650' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'gre44', 1, 21, CAST(N'2021-07-01 16:41:38.077' AS DateTime), CAST(N'2021-07-01 20:41:38.077' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'awddf334', 1, 22, CAST(N'2021-07-01 16:42:56.563' AS DateTime), CAST(N'2021-07-01 20:42:56.563' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'dsfsdf4', 2, 23, CAST(N'2021-07-01 16:45:12.770' AS DateTime), CAST(N'2021-07-02 00:45:12.770' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'asd48', 3, 1006, CAST(N'2021-07-02 10:19:41.950' AS DateTime), CAST(N'2021-07-02 22:19:41.950' AS DateTime))
INSERT [dbo].[Ventas_Estadias] ([dominio], [id_tipo_estadia], [id_venta_estadia], [fecha], [fecha_egreso]) VALUES (N'izf0284', 2, 1007, CAST(N'2021-07-02 10:26:15.273' AS DateTime), CAST(N'2021-07-02 18:26:15.273' AS DateTime))
SET IDENTITY_INSERT [dbo].[Ventas_Estadias] OFF
/****** Object:  Index [IX_Abonados]    Script Date: 19/07/2021 11:37:54 ******/
CREATE NONCLUSTERED INDEX [IX_Abonados] ON [dbo].[Abonados]
(
	[id_abonado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Abonados]  WITH CHECK ADD  CONSTRAINT [FK_Abonados_Tipos_abonos] FOREIGN KEY([id_tipoAbono])
REFERENCES [dbo].[Tipos_abonos] ([id_tipoAbono])
GO
ALTER TABLE [dbo].[Abonados] CHECK CONSTRAINT [FK_Abonados_Tipos_abonos]
GO
ALTER TABLE [dbo].[Abonados]  WITH CHECK ADD  CONSTRAINT [FK_Abonados_Tipos_de_Vehiculos] FOREIGN KEY([id_tipoVehiculo])
REFERENCES [dbo].[Tipos_de_Vehiculos] ([id_tipoVehiculo])
GO
ALTER TABLE [dbo].[Abonados] CHECK CONSTRAINT [FK_Abonados_Tipos_de_Vehiculos]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Tipos_de_Vehiculos] FOREIGN KEY([id_tipoVehiculo])
REFERENCES [dbo].[Tipos_de_Vehiculos] ([id_tipoVehiculo])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Tipos_de_Vehiculos]
GO
ALTER TABLE [dbo].[Pagos_Abonos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Abonos_Abonados] FOREIGN KEY([id_abonado])
REFERENCES [dbo].[Abonados] ([id_abonado])
GO
ALTER TABLE [dbo].[Pagos_Abonos] CHECK CONSTRAINT [FK_Pagos_Abonos_Abonados]
GO
ALTER TABLE [dbo].[Pagos_Abonos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Abonos_Detalles_Pago] FOREIGN KEY([id_detallePago])
REFERENCES [dbo].[Detalles_Pago] ([id_detallePago])
GO
ALTER TABLE [dbo].[Pagos_Abonos] CHECK CONSTRAINT [FK_Pagos_Abonos_Detalles_Pago]
GO
ALTER TABLE [dbo].[Pagos_Abonos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Abonos_Periodos] FOREIGN KEY([id_periodo])
REFERENCES [dbo].[Periodos] ([id_periodo])
GO
ALTER TABLE [dbo].[Pagos_Abonos] CHECK CONSTRAINT [FK_Pagos_Abonos_Periodos]
GO
ALTER TABLE [dbo].[Tarifas]  WITH CHECK ADD  CONSTRAINT [fk_id_tipo_ve] FOREIGN KEY([id_tipo_vehiculo])
REFERENCES [dbo].[Tipos_de_Vehiculos] ([id_tipoVehiculo])
GO
ALTER TABLE [dbo].[Tarifas] CHECK CONSTRAINT [fk_id_tipo_ve]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [fk_id_cliente_ticket] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [fk_id_cliente_ticket]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [fk_id_detalle_venta] FOREIGN KEY([id_detalle_venta])
REFERENCES [dbo].[Detalles_Venta] ([id_detalle_venta])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [fk_id_detalle_venta]
GO
ALTER TABLE [dbo].[Ventas]  WITH CHECK ADD  CONSTRAINT [fk_id_cliente_venta] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Clientes] ([id_cliente])
GO
ALTER TABLE [dbo].[Ventas] CHECK CONSTRAINT [fk_id_cliente_venta]
GO
ALTER TABLE [dbo].[Ventas]  WITH CHECK ADD  CONSTRAINT [fk_id_detalle_venta_ventas] FOREIGN KEY([id_detalle_venta])
REFERENCES [dbo].[Detalles_Venta] ([id_detalle_venta])
GO
ALTER TABLE [dbo].[Ventas] CHECK CONSTRAINT [fk_id_detalle_venta_ventas]
GO
ALTER TABLE [dbo].[Ventas_Estadias]  WITH CHECK ADD  CONSTRAINT [fk_id_tipo_estadia_venta] FOREIGN KEY([id_tipo_estadia])
REFERENCES [dbo].[Tipos_Estadia] ([id_tipo_estadia])
GO
ALTER TABLE [dbo].[Ventas_Estadias] CHECK CONSTRAINT [fk_id_tipo_estadia_venta]
GO
USE [master]
GO
ALTER DATABASE [Parking] SET  READ_WRITE 
GO
