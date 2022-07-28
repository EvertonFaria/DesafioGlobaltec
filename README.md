Desafio API C# - Globaltec
1. OBJETIVO
O objetivo desse documento é auxiliar na contratação de candidatos a vaga de Analista/desenvolvedor de uma maneira que simule os desafios do dia a dia onde o candidato tem um prazo pré-definido de 1 semana, para conclusão do desafio.

2. CONSIDERAÇÕES
O Desafio, está separado em 2 partes, uma criação de uma aplicação web (API) e outra resolução de um problema em SQL.
Parte 1: Para simplicidade do desafio, os dados, tanto para inserção/alteração ou consulta, podem ser armazenados na memória, não existindo a necessidade de se criar um banco de dados.
Todas rotas, com exceção da de autenticação, devem validar se a requisição possui um Token autenticado.
Serão avaliados:
- Organização de código;
- Lógica;
- Estrutura da API;
- Padrões REST, uso dos verbos, códigos de retorno e etc;
- Padronização de código C# (nome de classes, propriedades e etc).

O objeto de pessoas proposto no desafio pode ser simples, possuindo apenas código, nome, CPF, UF e data de nascimento.
Parte 2: Consulta em SQL que, preferencialmente, deve ser simulada para rodar em um servidor SQL Server.

3. O DESAFIO
Desafio 1: 
Criar uma API REST utilizando a linguagem C#, contendo as seguintes funcionalidades:
1. Uma rota para autenticação;
2. Uma rota para consulta de pessoas, que deve retornar uma lista de objeto de pessoas;
3. Uma rota para consultar uma pessoa pelo seu código;
4. Uma rota para consultar pessoas de uma determinada UF;
5. Uma rota de gravar pessoa, que deve retornar o objeto “salvo”;
a. O método deve ser capaz de validar as informações recebidas;
6. Uma rota para atualizar os dados de uma pessoa, que deve retorno o objeto atualizado;
7. Uma rota para excluir uma pessoa.

Desafio 2 (Consta no arquivo .zip anexo): 
O Gerente financeiro requisitou junto a Globaltec, uma consulta que traga as informações de contas a pagar e contas pagas, sendo que ele precisa do número do processo de pagamento, nome do fornecedor, data de vencimento, data de pagamento, valor líquido e um identificador se é conta a pagar ou paga.
Dado a requisição acima, o sistema trabalha com duas tabelas uma para armazenar as contas a pagar e outra para armazenar as contas pagas, o número do processo de pagamento é único entre as tabelas, e o cliente precisa que essa informação venha em uma única consulta.

(Os fontes para ambos desafios encontram-se no arquivo .zip, existente na raiz do projeto.)
