

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cotizaciones](
	[IdCotizacion] [int] IDENTITY(1,1) NOT NULL,
	[Dia] [date] NULL,
	[Valor] [decimal](10, 4) NULL,
 CONSTRAINT [PK_Cotizaciones] PRIMARY KEY CLUSTERED 
(
	[IdCotizacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_ActualizarCotizaciones]
	@path VARCHAR(MAX)
AS
BEGIN
	DECLARE @exec VARCHAR(MAX)

	SET @exec = 'DELETE FROM Cotizaciones
	
	DECLARE @JSON VARCHAR(MAX) 
	SELECT @JSON = BulkColumn
	FROM OPENROWSET 
	(BULK ''' + @path + ''', SINGLE_CLOB) 
	AS j
	
	INSERT INTO Cotizaciones (Dia, Valor)
	SELECT Fecha, valor
	FROM OPENJSON (@JSON) 
	WITH (Fecha DATE ''' + '$.d' + ''',
		  valor DECIMAL(10,4) ''' + '$.v' + ''')
	'

	EXECUTE(@exec)
END
GO
