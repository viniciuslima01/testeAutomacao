using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDeAutomacao.Geral;

namespace TesteDeAutomacao.Principal
{
    [TestFixture]
    class principalQA : setupQA
    {
        [Test]
        public void Test2_Procurar_CEP_Inexistente()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));

            driver.Navigate().GoToUrl("https://www.correios.com.br/");

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("carol-fecha")));

            driver.FindElement(By.Id("carol-fecha")).Click();

            driver.FindElement(By.Id("relaxation")).SendKeys("80700000");
            string originalWindow = driver.CurrentWindowHandle;

            driver.FindElement(By.XPath("//*[@id='content']/div[3]/div/div[2]/div[2]/form/div[2]/button/i")).Click();

            foreach (string window in driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    driver.SwitchTo().Window(window);                    
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='mensagem-resultado-alerta']")));
                    var validaMensagem = driver.FindElement(By.XPath("//*[@id='mensagem-resultado-alerta']/h6")).Text;

                    Assert.AreEqual("Dados não encontrado", validaMensagem);
                    driver.Close();
                    break;                    
                }
            }
            driver.SwitchTo().Window(originalWindow);
            driver.Close();
        }

        [Test]
        public void Test2_Procurar_CEP_Existente()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));

            driver.Navigate().GoToUrl("https://www.correios.com.br/");

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("carol-fecha")));

            driver.FindElement(By.Id("carol-fecha")).Click();

            driver.FindElement(By.Id("relaxation")).SendKeys("01013-001");
            string originalWindow = driver.CurrentWindowHandle;

            driver.FindElement(By.XPath("//*[@id='content']/div[3]/div/div[2]/div[2]/form/div[2]/button/i")).Click();

            foreach (string window in driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    driver.SwitchTo().Window(window);                    
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='resultado-DNEC']/tbody/tr/td[1]")));
                    var validaMensagem = driver.FindElement(By.XPath("//*[@id=\"resultado-DNEC\"]/tbody/tr/td[1]")).Text;
                    Assert.AreEqual("Rua Quinze de Novembro - lado ímpar", validaMensagem);
                    driver.Close();
                    break;
                }
            }
            driver.SwitchTo().Window(originalWindow);
            driver.Close();
        }
    }
}
