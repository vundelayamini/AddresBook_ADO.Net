C
CREATE TABLE [dbo].[AddressBook] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]   VARCHAR (50) NULL,
    [LastName]    VARCHAR (50) NULL,
    [Address]     VARCHAR (50) NULL,
    [City]        VARCHAR (50) NULL,
    [State]       VARCHAR (50) NULL,
    [Zip]         INT          NULL,
    [PhoneNumber] VARCHAR (15) NULL,
    [Email]       VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);   
