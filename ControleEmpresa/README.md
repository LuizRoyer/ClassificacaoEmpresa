# ControleEmpresa

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 9.1.7.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

----------------------------------------------------------------------------------------------------------------

                                        Descriçoes do Autor 


projeto de classificação de Empresas 

projeto dividido em duas partes API desenvolvida em .Net Core 3.1 e sua Interface Angular 9

API na porta:https://localhost:44321
Angular na porta: http://localhost:4200

Angular responsável por receber as notas fiscais, envia-las para API, e mostrar a classificação das Empresas já cadastradas

A API irá receber uma classe com as notas, irá cadastrar, fazer o cálculo da classificação e devolvera para o Angular apresentar os resultados na Tela.
Na API, Não Utilizando os Conceitos De OOP e Entidade Relacional, Apenas Gravando No banco Notas Fiscais e Notas de Debito temos a seguinte Estrutura

* ApiControleEmpresa
-> é o pacote contendo todo o projeto desenvolvido
* Controllers
-> Encontramos o EmpresaControllers que está responsável em receber a requisição do Angular e enviar para o Service fazer as validações 
* Service
-> Encontramos a EmpresaService, onde está toda a regra de negócio, contendo as consultas necessárias ,  o método de cálculo da classificação das empresas ,  a validação dos parâmetros recebidos , a inserção no banco de dados  e o controle de transações.
*IRepository
-> contém a Interface para uso de inversão de dependência, inclui a representação do repositório da Empresa, NotaFiscal e NotaDebito  e seus métodos.
*Repository
-> Estão os repositórios e seus métodos necessários neste caso, temos consultas no banco, inserção, e métodos de cálculo para obter a classificação da empresa.
*Entities
-> neste pacote está a representação das entidades no Banco de Dados, ou que usamos para auxiliar no desenvolvimento e inserção da Nota.
*IUnitOfWork e UnitOfWork
-> Desenvolvidos para gerenciar, a conexão e os repositories, garantindo maior controle e testabilidade.

No projeto na parte de Angular responsável pelo Front-End Temos 

* ControleEmpresa 
-> onde consta todo o projeto, a instalação dos módulos necessários, integração com o Boostrap e XlSX   entre outros que foram utilizados.
* temos o Componente de Cadastro-empresa
-> responsável pela parte onde o usuário ira informar as notas, a empresa responsável, o tipo de Nota Fiscal ou Nota de Debito, levando em conta que para o banco 1 = Nota Fiscal e 2 para Debito,  assim que informar todos os dados,  o componente ira validar algumas informações, e quando o usuário clicar em enviar  será enviado para a API executar o processo de Validação e caso os dados sejam validos era inseri-los.
* Temos o componente de classificacao-empresa 
-> é onde está a formação do gráfico com a demonstração da classificação da empresa.
como consta a menor empresa possuem 1, a maior é 100 e as demais são avaliadas conforme calculo

* calculo 
-> todas as empresas iniciam com 50, caso ela emita uma nota Fiscal são somados mais 2%, 
	caso a empresa emita uma nota de Debito, são subtraídos 4%.

* Nota Fiscal / Debito

-> é esperado uma nota com os seguintes campos 
--> Identificador da Empresa
--> Tipo de Nota (1 ou 2) Fiscal ou Debito como descrito acima.
--> Lista de Itens (as notas do usuário)
--> Nos Itens esperamos uma Lista de Itens com 1 ou mais itens  
   uma Lista dos Nomes dos itens, uma lista com as quantidades e preços e a data desta nota.


