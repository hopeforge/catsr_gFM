CREATE TABLE `catsr_cpj`.`Movimentacao` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Data` DATETIME NOT NULL,
  `Descricao` VARCHAR(700) NOT NULL,
  `IdProcesso` INT NOT NULL,
  PRIMARY KEY (`Id`),
  FOREIGN KEY (IdProcesso) REFERENCES Processo(Id)
  );
