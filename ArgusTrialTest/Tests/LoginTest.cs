using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using ArgusTrialTest.Pages;
using ArgusTrialTest.Utils;
using System.Threading.Tasks;

namespace ArgusTrialTest.Tests
{


    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class LoginTest : PageTest
    {
        // No constructor needed; use default
        [SetUp]
        public async Task Setup()
        {
            await Context.Tracing.StartAsync(new()
            {
                Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [TearDown]
        public async Task TearDown()
        {
            await Context.Tracing.StopAsync(new()
            {
                Path = Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "playwright-traces",
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                )
            });
        }

        [Test]
        public async Task SuccessfulUserLoginAIT1()
        {
            TestContext.Progress.WriteLine("Testing Successful User Login");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            var responseTask = Page.WaitForResponseAsync(response =>
                response.Url.Contains("/dashboard") && response.Status == 200);
            await loginPage.LogInUser();
            var response = await responseTask;
            Assert.That(response.Status, Is.EqualTo(200));
        }

        [Test]
        public async Task SuccessfulAdminLoginAIT1()
        {
            TestContext.Progress.WriteLine("Testing Successful Admin Login");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            var responseTask = Page.WaitForResponseAsync(response =>
                response.Url.Contains("/dashboard") && response.Status == 200);
            await loginPage.LogInAdmin();
            var response = await responseTask;
            Assert.That(response.Status, Is.EqualTo(200));
        }

        [Test]
        public async Task EmptyEmailLoginAIT2()
        {
            TestContext.Progress.WriteLine("Logging in with Empty Email");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            var responseTask = Page.WaitForResponseAsync(response =>
                response.Url.Contains("/login") && response.Status == 400);
            await loginPage.FillInAdminPass();
            await loginPage.ClickLogin();
            var response = await responseTask;
            Assert.That(response.Status, Is.EqualTo(400));
            var errorMessage = Page.Locator(".text-danger");
            await Expect(errorMessage).ToBeVisibleAsync();
            await Expect(errorMessage).ToHaveTextAsync("Email and password fields are required");
        }

        [Test]
        public async Task EmptyPWLoginAIT3()
        {
            TestContext.Progress.WriteLine("Logging in with Empty PW");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            var responseTask = Page.WaitForResponseAsync(response =>
                response.Url.Contains("/login") && response.Status == 400);
            await loginPage.FillInAdminEmail();
            await loginPage.ClickLogin();
            var response = await responseTask;
            Assert.That(response.Status, Is.EqualTo(400));
            var errorMessage = Page.Locator(".text-danger");
            await Expect(errorMessage).ToBeVisibleAsync();
            await Expect(errorMessage).ToHaveTextAsync("Email and password fields are required");
        }

        [Test]
        public async Task InvalidLoginAIT4()
        {
            TestContext.Progress.WriteLine("Logging in with Invalid Credentials");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            var responseTask = Page.WaitForResponseAsync(response =>
                response.Url.Contains("/login") && response.Status == 400);
            await loginPage.LogInInvalid();
            var response = await responseTask;
            Assert.That(response.Status, Is.EqualTo(400));
            var errorMessage = Page.Locator(".text-danger");
            await Expect(errorMessage).ToBeVisibleAsync();
            await Expect(errorMessage).ToHaveTextAsync("Invalid email or password");
        }

        [Test]
        public async Task ForgotPasswordAIT5()
        {
            TestContext.Progress.WriteLine("Testing Forgot Password Functionality");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.ClickForgotPW();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/forgot-password");
        }

        [Test]
        public async Task PWVisibilityToggleAIT6()
        {
            TestContext.Progress.WriteLine("Testing Toggle PW Visibility button, toggling 10 times");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.FillInAdminPass();
            for (int i = 0; i < 10; i++)
            {
                await loginPage.ClickTogglePWVisibility();
                await Page.WaitForTimeoutAsync(1000);
            }
        }
    }
}
