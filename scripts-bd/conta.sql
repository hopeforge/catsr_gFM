CREATE TABLE `catsr_cpj`.`Conta` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Login` VARCHAR(25) NOT NULL,
  `Senha` VARCHAR(25) NOT NULL,
  `UsuarioId` int NOT NULL,
  `PortalId` int NOT NULL,
  PRIMARY KEY (`Id`),
  FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    FOREIGN KEY (PortalId) REFERENCES Portal(Id));
