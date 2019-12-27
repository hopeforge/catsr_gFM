<img src="https://gitlab.com/dev4change/catsr_gFM/raw/master/cpj.portal/cpj.portal/wwwroot/images/cpjquadrado.png" width="220" height="120">

## Projeto CPJ - Sistema de Consulta de Processos Judiciais

O projeto é dividido em 03 (três) ambientes: 

1. Windows Service
> Serviço responsável em executar as leituras das páginas dos tribunais, identificando os processos que estão com status de "Execução Fiscal Municipal" e alimentando a base de dados com todas as informações de cada processo, principalmente o nome  do juiz responsável pelo julgamento e o valor do processo.

**Nota: Para que o serviço execute as leituras com sucesso, será necessário criar um usuário em um portal do Tribunal de Justiça, por exemplo, http://www.tjsp.jus.br/** 
 
2. API
> Responsável em acessar a base de dados com todas as informações necessárias para apresentação na aplicação Frontend.

3. Aplicação - Portal CPJ
> Aplicação Frontend responsável em apresentar as informações de forma agrupada / classificada para o usuário final.


## Licença
Projeto Open Source (GPLv3)