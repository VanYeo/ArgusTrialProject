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
    public class EditClientTest : PageTest

    {
        // No constructor needed; use default
        [SetUp]
        public async Task Setup()
        {
            await Page.SetViewportSizeAsync(1920, 1080);
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
        public async Task GoToEditClientPageAIT45()
        {
            TestContext.Progress.WriteLine("Testing Navigation to Edit Client Page as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "1");
            var editClientPage = new EditClientPage(Page, "1");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/1");
            await Page.WaitForTimeoutAsync(2000);

        }

        [Test]
        public async Task EditClientGeneralInfoAIT46()
        {
            TestContext.Progress.WriteLine("Testing Edit Client General Information as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "1");
            var editClientPage = new EditClientPage(Page, "1");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/1");
            await editClientPage.FillGeneralInformation(
                TestConfig.Companyname,
                TestConfig.Phone,
                TestConfig.Mobile,
                TestConfig.Contact
            );
            await Page.ClickAsync("body"); // Click to remove focus from input fields
            await Page.WaitForTimeoutAsync(2000);
        }

        [Test]
        public async Task EditClientLoginAccountInfoAIT47()
        {
            TestContext.Progress.WriteLine("Testing Edit Client Login & Account Information as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "1");
            var editClientPage = new EditClientPage(Page, "1");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/1");
            await editClientPage.FillLoginInformation(TestConfig.TestEmail);
            await editClientPage.FillAccountInformation(
                TestConfig.AccountName,
                TestConfig.AccountEmail,
                TestConfig.AccountEmailBill,
                TestConfig.AccountPhone,
                TestConfig.AccountCompanyName,
                TestConfig.AccountManager
                );

            await Page.ClickAsync("body"); // Click to remove focus from input fields
            await Page.WaitForTimeoutAsync(2000);
        }

        [Test]
        public async Task EditClientDeliveryAddressAIT48()
        {
            TestContext.Progress.WriteLine("Testing Edit Client Delivery Address as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "1");
            var editClientPage = new EditClientPage(Page, "1");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/1");
            await editClientPage.FillBillingAddress(
                TestConfig.BillingStreet,
                TestConfig.BillingCity,
                TestConfig.BillingState,
                TestConfig.BillingZip,
                TestConfig.BillingCountry
                );
            await editClientPage.CopyBillingAddressToShipping();
            await Page.ClickAsync("body"); // Click to remove focus from input fields
            await Page.WaitForTimeoutAsync(2000);
        }

        [Test]
        public async Task EditClientTickboxesAIT49()
        {
            TestContext.Progress.WriteLine("Testing Edit Client Tickboxes as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "1");
            var editClientPage = new EditClientPage(Page, "1");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/1");
            await editClientPage.CheckAllTickboxes();
            await editClientPage.UncheckAllTickboxes();
            await editClientPage.CheckAllTickboxes();
            await Page.ClickAsync("body"); // Click to remove focus from input fields
            await Page.WaitForTimeoutAsync(2000);
        }

        [Test]
        public async Task EditClientContractAIT50()
        {
            TestContext.Progress.WriteLine("Testing Edit Client Contract as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "1");
            var editClientPage = new EditClientPage(Page, "1");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/1");
            await editClientPage.FillContractPeriod(TestConfig.ContractStartDate, "24");
            await editClientPage.SelectDefaultPlans();
            await Page.ClickAsync("body"); // Click to remove focus from input fields
            await Page.WaitForTimeoutAsync(2000);
        }

        [Test]
        public async Task EditClientNotesAIT51()
        {
            TestContext.Progress.WriteLine("Testing Client Notes Edit as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "1");
            var editClientPage = new EditClientPage(Page, "1");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/1");
            await editClientPage.FillNotes(TestConfig.Notes);
            await Page.ClickAsync("body"); // Click to remove focus from input fields
            await Page.WaitForTimeoutAsync(2000);
        }

        [Test]
        public async Task SaveEditClientAIT52()
        {
            TestContext.Progress.WriteLine("Testing Save Edit Client as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "2");
            var editClientPage = new EditClientPage(Page, "2");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("2");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/2");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/2");
            await editClientPage.FillGeneralInformation(
                TestConfig.Companyname,
                TestConfig.Phone,
                TestConfig.Mobile,
                TestConfig.Contact
            );
            await editClientPage.FillLoginInformation(TestConfig.TestEmail);
            await editClientPage.FillAccountInformation(
                TestConfig.AccountName,
                TestConfig.AccountEmail,
                TestConfig.AccountEmailBill,
                TestConfig.AccountPhone,
                TestConfig.AccountCompanyName,
                TestConfig.AccountManager
            );
            await editClientPage.FillBillingAddress(
                TestConfig.BillingStreet,
                TestConfig.BillingCity,
                TestConfig.BillingState,
                TestConfig.BillingZip,
                TestConfig.BillingCountry
            );
            await editClientPage.CopyBillingAddressToShipping();
            await editClientPage.CheckAllTickboxes();
            await editClientPage.FillContractPeriod(
                TestConfig.ContractStartDate,
                TestConfig.ContractDuration
            );
            await editClientPage.SelectDefaultPlans();
            await editClientPage.SelectContractOptionsCheckbox();
            await editClientPage.FillNotes(TestConfig.Notes);
            await editClientPage.ClickSaveButton();
            await Expect(Page).ToHaveURLAsync($"http://127.0.0.1:57123/clients/2");

        }

        [Test]
        public async Task CancelEditClientAIT53()
        {
            TestContext.Progress.WriteLine("Testing Cancel Edit Client as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            var clientdetailPage = new ClientDetailPage(Page, "2");
            var editClientPage = new EditClientPage(Page, "2");
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.GoToClientDetailsPage("2");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/2");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/2");
            await editClientPage.FillGeneralInformation(
                TestConfig.Companyname,
                TestConfig.Phone,
                TestConfig.Mobile,
                TestConfig.Contact
            );
            await editClientPage.ClickCancelButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/2");    
        }
    }
}
