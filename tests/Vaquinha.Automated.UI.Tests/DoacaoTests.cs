using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
	{
		//private const string APPLICATION_URL = "https://vaquinha.azurewebsites.net/";
		private const string APPLICATION_URL = "http://localhost:5000";

		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl(APPLICATION_URL);
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}

		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl(APPLICATION_URL);
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert
			_driver.Url.Should().Contain("/Doacoes/Create");
		}

		[Fact]
		public void DoacaoUI_CriacaoDoacaoComTaxaPagaPeloDoador()
		{
			//Arrange #1
			var doacao = _doacaoFixture.DoacaoValida(doadorAceitouTaxa: true);
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl(APPLICATION_URL);
			_driver = _driverFactory.GetWebDriver();

			//Act #1 -- Navegar para o formulário de doação
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert #1
			_driver.Url.Should().Contain("/Doacoes/Create");
			

			//Arrange #2

			// Valor do pagamento
			_driver.FindElement(By.Id("valor")).SendKeys(doacao.Valor.ToString());

			if(doacao.DoadorAceitouTaxa)
				_driver.FindElement(By.Id("DoadorAceitouTaxa")).Click();

			// Dados Pessoais
			_driver.FindElement(By.Id("DadosPessoais_Nome")).SendKeys(doacao.DadosPessoais.Nome);
			_driver.FindElement(By.Id("DadosPessoais_Email")).SendKeys(doacao.DadosPessoais.Email);
			_driver.FindElement(By.Id("DadosPessoais_MensagemApoio")).SendKeys(doacao.DadosPessoais.MensagemApoio);
			
			if(doacao.DadosPessoais.Anonima)
				_driver.FindElement(By.Id("DadosPessoais_Anonima")).Click();

			// Endereço de Cobrança			
			_driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco")).SendKeys(doacao.EnderecoCobranca.TextoEndereco);
			_driver.FindElement(By.Id("EnderecoCobranca_Numero")).SendKeys(doacao.EnderecoCobranca.Numero);
			_driver.FindElement(By.Id("EnderecoCobranca_Cidade")).SendKeys(doacao.EnderecoCobranca.Cidade);
			_driver.FindElement(By.Id("estado")).SendKeys(doacao.EnderecoCobranca.Estado);
			_driver.FindElement(By.Id("cep")).SendKeys(doacao.EnderecoCobranca.CEP);
			_driver.FindElement(By.Id("EnderecoCobranca_Complemento")).SendKeys(doacao.EnderecoCobranca.Complemento);
			_driver.FindElement(By.Id("telefone")).SendKeys(doacao.EnderecoCobranca.Telefone);

			// Dados para pagamento
			_driver.FindElement(By.Id("FormaPagamento_NomeTitular")).SendKeys(doacao.FormaPagamento.NomeTitular);
			_driver.FindElement(By.Id("cardNumber")).SendKeys(doacao.FormaPagamento.NumeroCartaoCredito);
			_driver.FindElement(By.Id("validade")).SendKeys(doacao.FormaPagamento.Validade);
			_driver.FindElement(By.Id("cvv")).SendKeys(doacao.FormaPagamento.CVV);
			
			// Act #2 -- Fazer doação
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			// Assert #2

			// Verificar mensagem de doacao efetuada com sucesso
			_driver.PageSource.Should().Contain(@"Doação realizada com sucesso!", because: "a doação foi realizada com sucesso");


			 // Navega para a lsta de doadores para confirmar que a doacao foi efetuada com o pagamento da taxa de 20%
			_driverFactory.NavigateToUrl(APPLICATION_URL + "/Doadores");
			_driver = _driverFactory.GetWebDriver();

			_driver.PageSource.Should().Contain(doacao.DadosPessoais.Nome, because: "o nome do doador deve constar na lista de doadores");
			_driver.FindElement(By.ClassName("check-box")).Selected.Should().Be(true, because: "o doador aceitou pagar a taxa de 20%");
		}

	}
}