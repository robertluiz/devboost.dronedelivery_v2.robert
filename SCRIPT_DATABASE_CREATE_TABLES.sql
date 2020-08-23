/* BASTA CRIAR O DATABASE QUE O SISTEMA CRIARÁ AS TABELAS VIA CODEFIRST */

CREATE DATABASE [DroneDelivery]
GO

USE [DroneDelivery]
GO
/****** Object:  Table [dbo].[Drone]    Script Date: 22/08/2020 00:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drone](
	[Id] [uniqueidentifier] NOT NULL,
	[Capacidade] [int] NOT NULL,
	[Velocidade] [int] NOT NULL,
	[Autonomia] [int] NOT NULL,	
	[Status] varchar(8000) NULL, /* EmTransito, Pronto, Carregando */
	[Carga] [int] NOT NULL,
	[DataAtualizacao] [datetime] NULL	
 CONSTRAINT [PK_Drone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 22/08/2020 00:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[Id] [uniqueidentifier] NOT NULL,
	[Peso] [int] NOT NULL,
	--[LatLong] [geography] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,	
	[Status] varchar(8000) NULL, /* PendenteEntrega, EmTransito, Entregue */
	[DataHora] [datetime] NOT NULL,
	[DroneId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]-- TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Drone] FOREIGN KEY([DroneId])
REFERENCES [dbo].[Drone] ([Id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Drone]
GO

/* INSERIR DADOS */
insert into [Drone]
values(NEWID(), 12, 50, 35, 'Pronto', 60, getdate())
insert into [Drone]		   
values(NEWID(), 10, 50, 35, 'Pronto', 60, getdate())
insert into [Drone]		   
values(NEWID(), 8, 50, 35, 'Pronto', 60, getdate())

/*
insert into [Pedido]
values(NEWID(), 5, geography::Point(-23.596864, -46.685760, 4326), -23.596864, -46.685760, getdate(), 'PendenteEntrega', 1)

Select * From Drone
Select * From Pedido

SELECT geography::Point(-23.596864, -46.685760, 4326)
*/

/* APAGA BASE */
/*
USE [DroneDelivery]
GO

DROP TABLE [Drone]
GO

DROP TABLE [Pedido]
GO

USE [Master]
DROP DATABASE [DroneDelivery];
*/
/* APAGA BASE */