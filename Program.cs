namespace MyTest
{

    using Microsoft.Playwright;
    using System.Threading.Tasks;
   
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var authTests = new AuthTestsRegistration();
            Task.Run(async () => await authTests.RunTest()).GetAwaiter().GetResult(); // запускается тест регистрации


            var authTests2 = new AuthTestsAuthorization();
            Task.Run(async () => await authTests2.RunTest()).GetAwaiter().GetResult();// запускается тест авторизации
        }
    }

    public  class AuthTestsRegistration
    {
        public async Task RunTest()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var page = await browser.NewPageAsync();

            try
            {
                Console.WriteLine("Тестируем регистрацию...");
                await page.GotoAsync("URL"); // вставить ссылку на главную страницу сайта/insert a link to the main page of the site

                // Нажатие кнопки регистрации/Clicking the registration button
                await page.ClickAsync("#header > a.registration-button.scroll"); //использование селектора class/using the class selector

                // Подождем, пока поле для ввода почты станет доступным, и затем введем почту
                var emailInput = await page.WaitForSelectorAsync("input[type='text']");
                await emailInput.FillAsync("insert your email here/сюда вставляем почту");

                await page.ClickAsync("#registration-form > div.form-wrapper.registration > button");

                // Проверка успешности
                var profilePageTitle = await page.TitleAsync();
                Console.WriteLine("Регистрация прошла успешно: " + profilePageTitle);

                var successMessage = await page.InnerTextAsync("#successMessage"); // Пример селектора
                Console.WriteLine("Сообщение после регистрации: " + successMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при регистрации: {ex.Message}");
            }
            finally
            {
                await browser.CloseAsync();       
            }
                                                  
        }                                                           
    } 

     public class AuthTestsAuthorization
     {
          public async Task RunTest()
          {
              var playwright = await Playwright.CreateAsync();
              var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
              var page = await browser.NewPageAsync();

              try
              {
                  Console.WriteLine("Тестируем авторизацию...");
                  await page.GotoAsync("URL");  // вставить ссылку на главную страницу сайта/insert a link to the main page of the site

                  //Нажатие кнопки авторизации на главной странице/Clicking the login button on the main page
                  await page.ClickAsync("#header > a.login-button");

                  // Ввод данных для авторизации/Entering data for authorization
                  await page.FillAsync("#email", "insert your email here/сюда вставляем почту");        //использование селектора id почты или логина(селекторы одинаковы)/
                                                                                                        //using mail id selector or login (selectors are the same)

                  await page.FillAsync("#password", "insert your password here/сюда вставляем почту");  //использование селектора id пароля/ using mail id password

                  // Нажатие кнопки авторизации на странице ввода данных пользователя/Clicking the authorization button on the user data entry page
                  await page.ClickAsync("#login-form > div:nth-child(1) > div:nth-child(4) > button"); ////использование селектора class/using class id selector

                  // Проверка успешности
                  var dashboardText = await page.InnerTextAsync("#welcomeMessage"); 
                  Console.WriteLine($"Авторизация прошла успешно: {dashboardText}"); 
              }
              catch (Exception ex)
              {
                  Console.WriteLine($"Ошибка при авторизации: {ex.Message}");
              }
              finally
              {
                  await browser.CloseAsync();
              }
          }
     }
  

}