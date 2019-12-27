CREATE TABLE `catsr_cpj`.`Processo` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `NroProcesso` VARCHAR(50) NOT NULL,
  `OutrosAssuntos` VARCHAR(80) NOT NULL,
  `NomeJuiz` VARCHAR(80) NOT NULL,
  `ValorAcao` DECIMAL(10,2) NOT NULL,
  `Requerido` VARCHAR(255) NOT NULL,
  `Requerente` VARCHAR(255) NOT NULL,
  `DataCadastro` DATETIME NOT NULL,
  `Favoritado` tinyint(4) DEFAULT NULL, 
  PRIMARY KEY (`Id`));