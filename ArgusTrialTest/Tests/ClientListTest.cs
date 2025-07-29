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
    public class ClientListTest : PageTest

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
        public async Task ViewClientListAIT9()
        {
            TestContext.Progress.WriteLine("Testing View Client List as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            var expectedHeaders = new List<string>
            {
                "Client ID",
                "Company Name",
                "Trading Name",
                "Contact",
                "Phone",
                "Mobile",
                "Email",
                "Contract End",
                "Connections",
                "Actions"
            };
            await dashboardPage.CheckTableHeaders(expectedHeaders);
        }

        [Test]
        public async Task SearchForClientAIT10()
        {
            TestContext.Progress.WriteLine("Testing Search for Client");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            await dashboardPage.FillInSearchBar("Jane");
            var clientIDs = new List<string> { "6", "7" };
            await dashboardPage.CheckClientIDsExists(clientIDs);
        }

        [Test]
        public async Task FilterActiveClientListAIT11()
        {
            TestContext.Progress.WriteLine("Testing Filter Active Client List");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            await dashboardPage.ClickFilterButton();
            await dashboardPage.ClickFilterClearAllButton();
            await dashboardPage.ClickFilterActiveButton();
            await dashboardPage.ClickFilterApplyButton();
            var clientIDs = new List<string> { "1", "2", "4", "6", "8", "9", "11", "12", "13", "16" };
            await dashboardPage.CheckClientIDsExists(clientIDs);
        }
        [Test]
        public async Task FilterInactiveClientListAIT11()
        {
            TestContext.Progress.WriteLine("Testing Filter Inactive Client List");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            await dashboardPage.ClickFilterButton();
            await dashboardPage.ClickFilterClearAllButton();
            await dashboardPage.ClickFilterInactiveButton();
            await dashboardPage.ClickFilterApplyButton();
            var clientIDs = new List<string> { "3", "5", "7", "10", "14", "15", "18", "19", "23", "27" };
            await dashboardPage.CheckClientIDsExists(clientIDs);
        }

        [Test]
        public async Task CustomiseColumnsAIT12()
        {
            TestContext.Progress.WriteLine("Testing Columns Customisation as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");

            await dashboardPage.ClickColumnsButton();
            await dashboardPage.ClickColumnsContractEndButton();
            await dashboardPage.ClickColumnsConnectionsButton();
            await dashboardPage.ClickColumnsApplyButton();

            var expectedHeaders = new List<string>
            {
                "Client ID",
                "Company Name",
                "Trading Name",
                "Contact",
                "Phone",
                "Mobile",
                "Email",
                "Actions"
            };
            await dashboardPage.CheckTableHeaders(expectedHeaders);
        }

        [Test]
        public async Task CheckAddNewClientRedirectAIT13()
        {
            TestContext.Progress.WriteLine("Checking Add New Client Redirect as Admin");
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
        public async Task CheckClientDetailAIT14()
        {
            TestContext.Progress.WriteLine("Checking Client Detail Page as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
        }

        [Test]
        public async Task CheckClientEndDateAIT15()
        {
            TestContext.Progress.WriteLine("Checking Client End Date Highlight as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            await dashboardPage.GoToClientDetailsPage("1");
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/1");
            var clientdetailPage = new ClientDetailPage(Page, "1");
            await clientdetailPage.ClickEditButton();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients/edit/1");
            DateTime dateInput = DateTime.Now.AddYears(-3);
            string dateString = dateInput.ToString("yyyy-MM-dd");
            await clientdetailPage.FillInContractStartDate(dateString);
            await clientdetailPage.ClickSaveButton();
            // check if 200 response
            var response = await Page.WaitForResponseAsync(response =>
                response.Url.Contains("/clients/1") && response.Status == 200);
            await dashboardPage.GoTo();
            await dashboardPage.CheckClientContractEndRed("1");
        }

        [Test]
        public async Task CheckPaginationNavigationAIT16()
        {
            TestContext.Progress.WriteLine("Checking Pagination Navigation as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            for (int i = 0; i < 3; i++)
            {
                await dashboardPage.ClickPaginationNextButton();
            }
            for (int i = 0; i < 3; i++)
            {
                await dashboardPage.ClickPaginationPreviousButton();
            }
            var clientIDs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            await dashboardPage.CheckClientIDsExists(clientIDs);
        }
        [Test]
        public async Task CheckPaginationRowDisplayAIT16()
        {
            TestContext.Progress.WriteLine("Checking Pagination Row changing to 5 as Admin");
            var loginPage = new LoginPage(Page);
            var dashboardPage = new DashboardPage(Page);
            await loginPage.GoTo();
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/login");
            await loginPage.LogIn(TestConfig.Adminemail, TestConfig.Adminpass);
            await Expect(Page).ToHaveURLAsync("http://127.0.0.1:57123/clients");
            await dashboardPage.ChangePaginationRowsTo5();
            var clientIDs = new List<string> { "1", "2", "3", "4", "5" };
            await dashboardPage.CheckClientIDsExists(clientIDs);
        }
    }
}
