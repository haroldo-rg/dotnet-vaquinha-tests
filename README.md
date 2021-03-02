# dotnet-vaquinha-tests
Projeto Base para Testes - .Net Core  

## Digital Innovation One

[Clique aqui para se inscrever na Digital Innovation One](https://digitalinnovation.one/sign-up?ref=H395IYS4Z6)  

## Eliézer Zarpelão
[GitHub Timeline](https://elizarp.github.io/timeline/)  
[Linkedin](http://br.linkedin.com/in/eliezerzarpelao)  
[Github](https://github.com/elizarp) 

## Marcos Freire
[Linkedin](https://www.linkedin.com/in/marcos-freire-a73891125/)  
[Github](https://github.com/marcosfreire) 

## Slides
[Slides em PDF](TesteNetCore.pdf)

## Haroldo Gomes
Alterações efetuadas como atividade do curso Implementando sua stack de testes de unidade e integrados em um projeto .NET de Crowdfunding da Digital Innovation One

Projeto **Vaquinha.Domain**
- Inclusão do atributo DoadorAceitouTaxa na entidade Doacao
- Inclusão do atributo DoadorAceitouTaxa nas view models DoacaoViewModel e DoadorViewModel

Projeto **Vaquinha.Unit.Tests**
- Inclusão do caso de teste Doacao_DoadorAceitouDoacaoComTaxa_DoacaoValida aplicando a taxa de 20% na doação

Projeto **Vaquinha.MVC**
- Inclusão do campo DoadorAceitouTaxa na view /Doacoes/Create
- Inclusão do campo DoadorAceitouTaxa na view /Doadores

Projeto **Vaquinha.Automated.UI.Tests**
- Inclusão do caso de teste DoacaoUI_CriacaoDoacaoComTaxaPagaPeloDoador
  Ações executadas no teste:
    1) Acessa a página */Home*
    2) Clica no botão *Doar*
    3) Verifica se a página */Doacoes/Create* foi carregada com sucesso
    4) Efetua uma doação digitando nos campos do formulário os dados de uma entidade Doacao com valores validos e com a opção *Aceito pagar a taxa de 20%* selecionada
    5) Verifica se a doação foi realizada com sucesso
    6) Acessa a página */Doadores* e verifica se o nome do doador consta na lista e se o checkbox da coluna *Taxa paga pelo doador* está marcado
