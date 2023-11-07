CREATE TABLE Account
(
	[id] int IDENTITY(1,1) PRIMARY KEY,
	[name] nvarchar(50) NOT NULL,
	[email] varchar(50) NOT NULL,
	[password] varchar(50) NOT NULL,
	[role] int NOT NULL,
)
GO

CREATE TABLE Bill
(
	[id] int IDENTITY(1,1) PRIMARY KEY,
	[amount] int NOT NULL,
	[date] smalldatetime NOT NULL,
	[id_account] int NOT NULL,
	CONSTRAINT Bill_idAccount_fk FOREIGN KEY (id_account) REFERENCES Account(id)
)
GO

CREATE TABLE Movie
(
	[id] int IDENTITY(1,1) PRIMARY KEY,
	[name] nvarchar(100) NOT NULL,
	[img] varchar(50) NOT NULL,
	[trailer] varchar(100) NOT NULL,
	[length_time] int NOT NULL,
	[description] nvarchar(Max) NOT NULL,
	[status] int NOT NULL,
)
GO

CREATE TABLE Room
(
	[id] int IDENTITY(1,1) PRIMARY KEY,
	[name] nvarchar(50) NOT NULL,
)
GO

CREATE TABLE Schedule
(
	[id] int IDENTITY(1,1) PRIMARY KEY,
	[date] smalldatetime NOT NULL,
	[time] time NOT NULL,
	[id_room] int NOT NULL,
	[id_movie] int NOT NULL,
	CONSTRAINT Schedule_idRoom_fk FOREIGN KEY (id_room) REFERENCES Room(id),
	CONSTRAINT Schedule_idMovie_fk FOREIGN KEY (id_movie) REFERENCES Movie(id)
)
GO

CREATE TABLE Ticket
(
	[id] int IDENTITY(1,1) PRIMARY KEY,
	[price] int NOT NULL,
	[status] int NOT NULL,
	[seat_id] int NOT NULL,
	[id_bill] int NOT NULL,
	[id_schedule] int NOT NULL,
	CONSTRAINT Ticket_idBill_fk FOREIGN KEY (id_bill) REFERENCES Bill(id),
	CONSTRAINT Ticket_idSchedule_fk FOREIGN KEY (id_schedule) REFERENCES Schedule(id)
)
GO



CREATE TABLE Seat
(
	[id] int IDENTITY(1,1) PRIMARY KEY,
	[name] nvarchar(50) NOT NULL,
	[type] nvarchar(50) NOT NULL,
	[price] int NOT NULL,
	[id_room] int NOT NULL,
	CONSTRAINT Seat_idRoom_fk FOREIGN KEY (id_room) REFERENCES Room(id)
)
GO


CREATE TABLE Genre
(
	[id]  int IDENTITY(1,1) PRIMARY KEY,
	[name] nvarchar(100) NOT NULL,
)
GO

CREATE TABLE Movie_Genre
(
	[id_movie] int NOT NULL,
	[id_genre] int NOT NULL,
	CONSTRAINT Movie_Genre_idMovie_fk FOREIGN KEY (id_movie) REFERENCES Movie(id),
	CONSTRAINT Movie_Genre_idGenre_fk FOREIGN KEY (id_genre) REFERENCES Genre(id)
)
GO