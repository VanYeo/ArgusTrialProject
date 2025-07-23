using Microsoft.Playwright;
using System.Threading.Tasks;
using ArgusTrialTest.Utils;
using NUnit.Framework.Internal;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Constraints;

namespace ArgusTrialTest.Pages

{
    public class LoginPage
    {
        private readonly IPage _page;
        private const string LoginUrl = "http://127.0.0.1:57123/login";
        public LoginPage(IPage page) => _page = page;

        public ILocator UsernameInput => _page.GetByPlaceholder("Email");
        public ILocator PasswordInput => _page.GetByPlaceholder("Password");
        public ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Sign In", Exact = true });
        public ILocator ForgotPWButton => _page.GetByRole(AriaRole.Button, new() { Name = " Forgot password ", Exact = true });
        public ILocator CancelPWButton => _page.Locator("button[class='btn border-0 bg-transparent']").Nth(1);
        public ILocator CancelEmailButton => _page.Locator("button[class='btn border-0 bg-transparent']").Nth(0);
        public ILocator TogglePWVisibilityButton => _page.Locator("button[class='btn border-0 d-inline px-0']").Nth(0);
        public async Task GoTo()
        {
            await _page.GotoAsync(LoginUrl);
        }

        public async Task LogIn(string email, string password)
        {
            await UsernameInput.FillAsync(email);
            await PasswordInput.FillAsync(password);
            await _page.WaitForTimeoutAsync(2000);
            await LoginButton.ClickAsync();
        }

        public async Task FillInEmail(string email)
        {
            await UsernameInput.FillAsync(email);
        }

        public async Task FillInPassword(string password)
        {
            await PasswordInput.FillAsync(password);
        }

        public async Task ClickCancelPW()
        {
            await CancelPWButton.ClickAsync();
        }

        public async Task ClickCancelEmail()
        {
            await CancelEmailButton.ClickAsync();
        }

        public async Task ClickTogglePWVisibility()
        {
            await TogglePWVisibilityButton.ClickAsync();
        }
        public async Task ClickForgotPW()
        {
            await ForgotPWButton.ClickAsync();
        }
        public async Task ClickLogin()
        {
            await LoginButton.ClickAsync();
        }
    }
}