using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using ArgusTrialTest.Pages;
using ArgusTrialTest.Utils;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace ArgusTrialTest.Tests
{


    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class AddNewClientTest : PageTest

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
        public async Task GoToAddClientPageAIT17()
        {
            TestContext.Progress.WriteLine("Testing Add New Client Page as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");

        }

        [Test]
        public async Task RequiredFieldsValidationAIT18()
        {
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.ClickAddClientButton();

            await addClientPage.CheckForRequiredErrorMessage();
            await addClientPage.CheckForInvalidFormErrorMessage();

            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
        }

        [Test]
        public async Task CheckGeneralClientInformationAIT19()
        {
            TestContext.Progress.WriteLine("Testing General Client Information as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.FillGeneralInformation(
                TestConfig.Companyname,
                TestConfig.Phone,
                TestConfig.Mobile,
                TestConfig.Contact
            );
        }
        [Test]
        public async Task SetLoginCredentialsAIT20()
        {
            TestContext.Progress.WriteLine("Testing filling Login Credentials as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.FillLoginInformation(TestConfig.TestEmail);
        }

        [Test]
        public async Task CheckAccountBillingDetailsAIT21()
        {
            TestContext.Progress.WriteLine("Testing Account and Billing Details as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.FillAccountInformation(
                TestConfig.AccountName,
                TestConfig.AccountEmail,
                TestConfig.AccountEmailBill,
                TestConfig.AccountPhone,
                TestConfig.AccountCompanyName,
                TestConfig.AccountManager
                );
            await addClientPage.FillBillingAddress(
                TestConfig.BillingStreet,
                TestConfig.BillingCity,
                TestConfig.BillingState,
                TestConfig.BillingZip,
                TestConfig.BillingCountry
                );
            await addClientPage.CopyBillingAddressToShipping();
        }

        [Test]
        public async Task CheckTickboxAIT22()
        {
            TestContext.Progress.WriteLine("Checking all tickboxes as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.CheckAllTickboxes();
        }

        [Test]
        public async Task CheckContractPeriodAIT23()
        {
            TestContext.Progress.WriteLine("Checking Contract Period as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");

            await addClientPage.FillContractPeriod(
                TestConfig.ContractStartDate,
                TestConfig.ContractDuration
            );
        }

        [Test]
        public async Task SelectDefaultPlansAIT24()
        {
            TestContext.Progress.WriteLine("Checking Selection of Default Plans as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.SelectDefaultPlans();
        }

        [Test]
        public async Task SelectContractOptionsAIT25()
        {
            TestContext.Progress.WriteLine("Testing Selection of Contract Options as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.SelectContractOptionsCheckbox();
            await addClientPage.SelectContractOptionsCheckbox();
        }

        [Test]
        public async Task SaveNewClientAIT26()
        {
            TestContext.Progress.WriteLine("Saving New Client as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.FillGeneralInformation(
                TestConfig.Companyname,
                TestConfig.Phone,
                TestConfig.Mobile,
                TestConfig.Contact
            );
            var clientId = await addClientPage.GetClientId();
            await addClientPage.FillLoginInformation(TestConfig.TestEmail);
            await addClientPage.FillAccountInformation(
                TestConfig.AccountName,
                TestConfig.AccountEmail,
                TestConfig.AccountEmailBill,
                TestConfig.AccountPhone,
                TestConfig.AccountCompanyName,
                TestConfig.AccountManager
            );
            await addClientPage.FillBillingAddress(
                TestConfig.BillingStreet,
                TestConfig.BillingCity,
                TestConfig.BillingState,
                TestConfig.BillingZip,
                TestConfig.BillingCountry
            );
            await addClientPage.CopyBillingAddressToShipping();
            await addClientPage.CheckAllTickboxes();
            await addClientPage.FillContractPeriod(
                TestConfig.ContractStartDate,
                TestConfig.ContractDuration
            );
            await addClientPage.SelectDefaultPlans();
            await addClientPage.SelectContractOptionsCheckbox();
            await addClientPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync($"http://127.0.0.1:57123/clients/{clientId}");
        }

        [Test]
        public async Task CancelClientCreationAIT27()
        {
            TestContext.Progress.WriteLine("Cancelling Client Creation as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var addClientPage = new AddClientPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            await dashboardPage.ClickAddClientButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/add");
            await addClientPage.FillGeneralInformation(
                TestConfig.Companyname,
                TestConfig.Phone,
                TestConfig.Mobile,
                TestConfig.Contact
            );
            await addClientPage.ClickCancelButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            
        }
    }
}
