USE akk_kercheval;
GO

CREATE TABLE CookieCutters
(
    CookieCutterId   BIGINT          IDENTITY(1,1) NOT NULL,
    Name             NVARCHAR(100)   NOT NULL,
    LengthInches     DECIMAL(6,2)    NULL,          -- e.g. 3.50
    WidthInches      DECIMAL(6,2)    NULL,          -- e.g. 2.25
    Type             NVARCHAR(30)    NOT NULL,      -- plastic | plastic with stamp | metal | copper
    Comments         NVARCHAR(MAX)   NULL,
    DateAcquired     DATE            NULL,
    CreatedAt        DATETIME2(0)    NOT NULL 
        CONSTRAINT DF_CookieCutters_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt        DATETIME2(0)    NOT NULL 
        CONSTRAINT DF_CookieCutters_UpdatedAt DEFAULT (SYSUTCDATETIME()),
    CreatedBy        NVARCHAR(100)   NULL,
    UpdatedBy        NVARCHAR(100)   NULL,

    CONSTRAINT PK_CookieCutters PRIMARY KEY CLUSTERED (CookieCutterId)
);
GO

-- Restrict Type to the four values you currently use
ALTER TABLE CookieCutters
ADD CONSTRAINT CK_CookieCutters_Type 
CHECK (Type IN (N'plastic', N'plastic with stamp', N'metal', N'copper'));
GO

-- Index for faster searching by name
CREATE NONCLUSTERED INDEX IX_CookieCutters_Name 
ON CookieCutters (Name);
GO

-- Automatically update UpdatedAt whenever a row is changed
CREATE TRIGGER TR_CookieCutters_UpdateTimestamp
ON CookieCutters
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE c
    SET UpdatedAt = SYSUTCDATETIME()
    FROM CookieCutters c
    INNER JOIN inserted i ON c.CookieCutterId = i.CookieCutterId;
END;
GO

ALTER TABLE CookieCutters
ADD ImageUrl NVARCHAR(500) NULL;
GO