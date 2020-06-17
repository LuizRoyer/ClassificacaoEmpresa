# ClassificacaoEmpresa
Projeto em Angular 9 e .Net Core 3.1
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

* no projeto segue duas notas em Excel como o sistema espera receber.
