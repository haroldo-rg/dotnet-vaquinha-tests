using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Firefox;

namespace Vaquinha.AutomatedUITests
{
	public class DriverFactory
    {
        private IWebDriver _driver;

        // Construtor da classe. 
        public DriverFactory()
        {
            const string GECKO_DRIVER_DIR = @"C:\HAROLDO\bootcamp\Desafio Projeto 03\dotnet-vaquinha-tests\tests\Tools\";

            // Inicializa o browser utilizando o ChromeDriver que está no caminho /usr/share/applications/
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(GECKO_DRIVER_DIR);
            //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(GECKO_DRIVER_DIR);
            
            // Faz criação de porta para abrir o browser.
            service.Port = new Random().Next(64000, 64800);
            
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument("no-sandbox");
            //options.AddArgument("proxy-server='direct://'");
            options.AddArgument("proxy-auto-detect");
            options.AddArgument("proxy-bypass-list=*");
            options.AddUserProfilePreference("disable-popup-blocking", "true");

            // Inicializa o IWebDriver do selenium, é ele que disponibiliza as consultas e manipulacoes das paginas. 
            _driver = new ChromeDriver(service, options);
            CodePagesEncodingProvider.Instance.GetEncoding(437);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //FirefoxOptions options = new FirefoxOptions();
            //options.AddArgument("-headless");
            //options.AddArgument("-safe-mode");
            options.AddArgument("-ignore-certificate-errors");
            //FirefoxProfile profile = new FirefoxProfile();
            //FirefoxProfile profile = new FirefoxProfile();
            //profile.AcceptUntrustedCertificates = true;
            //profile.AssumeUntrustedCertificateIssuer = false;
            //options.Profile = profile;
            
            //_driver = new FirefoxDriver(service, options);
            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Manage().Window.Maximize();
        }

        // Navega para determinada URL
        public void NavigateToUrl(String url)
        {            
            _driver.Navigate().GoToUrl(url);
        }

        // Finaliza driver e serviço.
        public void Close()
        {
            _driver.Quit();
        }

        // Disponibiliza driver.
        public IWebDriver GetWebDriver()
        {
            return _driver;
        }
    }
}