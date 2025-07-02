using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using ArgusTrialTest.Pages;
using ArgusTrialTest.Utils;
using System.Threading.Tasks;

namespace ArgusTrialTest.Tests
{

    public class LoginTest : PageTest
    {
        // No constructor needed; use default

        [Test]
        public async Task SuccessfulUserLoginAIT1()
        {
            TestContext.Progress.WriteLine("Testing Successful User Login");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogInUser();
            await Page.WaitForTimeoutAsync(2000);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/dashboard");
            TestContext.Progress.WriteLine("Test Complete");
            await Page.WaitForTimeoutAsync(7000);
        }

        [Test]
        public async Task SuccessfulAdminLoginAIT1()
        {
            TestContext.Progress.WriteLine("Testing Successful Admin Login");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogInAdmin();
            await Page.WaitForTimeoutAsync(2000);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/dashboard");
            TestContext.Progress.WriteLine("Test Complete");
            await Page.WaitForTimeoutAsync(7000);
        }

        [Test]
        public async Task EmptyPWLoginAIT3()
        {
            TestContext.Progress.WriteLine("Logging in with Empty PW");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.FillInAdmin();
            await Page.WaitForTimeoutAsync(2000);
            await loginPage.ClickCancelPW();
            await Page.WaitForTimeoutAsync(2000);
            await loginPage.ClickLogin();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            TestContext.Progress.WriteLine("Test Complete");
            await Page.WaitForTimeoutAsync(7000);
        }

        [Test]
        public async Task PWVisibilityToggleAIT6()
        {
            TestContext.Progress.WriteLine("Testing Toggle PW Visibility button, toggling 10 times");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.FillInAdmin();
            for (int i = 0; i < 10; i++)
            {
                await loginPage.ClickTogglePWVisibility();
                await Page.WaitForTimeoutAsync(3000);
            }
            TestContext.Progress.WriteLine("Test Complete");
            await Page.WaitForTimeoutAsync(7000);
        }

        [Test]
        public async Task ForgotPasswordAIT5()
        {
            TestContext.Progress.WriteLine("Testing Forgot Password Functionality");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await Page.WaitForTimeoutAsync(2000);
            await loginPage.ClickForgotPW();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/forgot-password");
            await Page.WaitForTimeoutAsync(2000);
        }

        [Test]
        public async Task EmptyEmailLoginAIT2()
        {
            TestContext.Progress.WriteLine("Logging in with Empty Email");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.FillInAdmin();
            await Page.WaitForTimeoutAsync(2000);
            await loginPage.ClickCancelEmail();
            await Page.WaitForTimeoutAsync(2000);
            await loginPage.ClickLogin();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            TestContext.Progress.WriteLine("Test Complete");
            await Page.WaitForTimeoutAsync(7000);
        }
        
        [Test]
        public async Task InvalidLoginAIT4()
        {
            TestContext.Progress.WriteLine("Logging in with Invalid Credentials");
            var loginPage = new LoginPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogInInvalid();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            TestContext.Progress.WriteLine("Test Complete");
            await Page.WaitForTimeoutAsync(7000);
        }

    }
}
