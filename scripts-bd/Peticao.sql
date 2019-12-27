CREATE TABLE `catsr_cpj`.`Peticao` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Data` DATETIME NOT NULL,
  `Tipo` VARCHAR(700) NOT NULL,
  `IdProcesso` int NOT NULL,
  PRIMARY KEY (`Id`),
  FOREIGN KEY (IdProcesso) REFERENCES Processo(Id));
