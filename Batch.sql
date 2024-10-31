CREATE TABLE Persona(
	idPersona INT  PRIMARY KEY IDENTITY (1,1) NOT NULL,
	nome VARCHAR (250) NOT NULL,
	cognome VARCHAR (250) NOT NULL,
	email VARCHAR (250) NOT NULL UNIQUE,
	telefono VARCHAR (15) NOT NULL, 
);

CREATE TABLE CodiceFiscale(
	idCodFis INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	codiceFis VARCHAR(16) NOT NULL UNIQUE,
	dataEmis DATE NOT NULL,
	dataScad DATE,
	personaRiff INT 
	FOREIGN KEY (personaRiff) REFERENCES Persona(idPersona) ON DELETE CASCADE,
	CONSTRAINT CKdata CHECK (dataEmis < dataScad),

);
INSERT INTO Persona (nome, cognome, email, telefono) 
VALUES 
('Mario', 'Rossi', 'mario.rossi@example.com', '1234567890'),
('Giulia', 'Verdi', 'giulia.verdi@example.com', '0987654321'),
('Luca', 'Bianchi', 'luca.bianchi@example.com', '1122334455');

INSERT INTO CodiceFiscale (codiceFis, dataEmis, dataScad, personaRiff)
VALUES 
('MRARSS80A01H501Z', '2022-01-01', '2032-01-01', 1),
('GLUVRD90B02H501A', '2023-02-02', '2033-02-02', 2),
('LCUBNC95C03H501B', '2024-03-03', '2034-03-03', 3);

SELECT * FROM Persona

SELECT * FROM CodiceFiscale