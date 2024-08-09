use ClinicaVeterinaria;

SELECT * FROM Animals;
SELECT * FROM Users;
SELECT * FROM Roles;


DROP TABLE IF EXISTS AspNetUserTokens;
DROP TABLE IF EXISTS AspNetUserLogins;
DROP TABLE IF EXISTS AspNetUserRoles;
DROP TABLE IF EXISTS AspNetUserClaims;
DROP TABLE IF EXISTS AspNetRoleClaims;
DROP TABLE IF EXISTS AspNetRoles;
DROP TABLE IF EXISTS AspNetUsers;

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL
);

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL
);

INSERT INTO Roles (RoleName) VALUES ('Veterinario'), ('Farmacista');

INSERT INTO Users (Username, Password, Role) VALUES
('veterinario@example.com', 'Password123!', 'Veterinario'),
('farmacista@example.com', 'Password123!', 'Farmacista');