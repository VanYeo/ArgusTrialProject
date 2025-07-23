using Microsoft.Playwright;
using System.Threading.Tasks;
using ArgusTrialTest.Utils;
using NUnit.Framework.Internal;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Constraints;
using Microsoft.VisualBasic;

namespace ArgusTrialTest.Pages

{
    public class DashboardPage
    {
        private readonly IPage _page;
        private const string DashboardUrl = "http://127.0.0.1:57123/clients";
        public DashboardPage(IPage page) => _page = page;


        // Table 
        public ILocator TableHeaders => _page.Locator("table thead tr th");
        public ILocator VisibleTableHeaders => _page.Locator("table thead tr th:visible");
        public ILocator TableRows => _page.Locator("table tbody tr");
        public ILocator ClientID(ILocator TableRows) => TableRows.Locator(":scope > td:first-child");
        public ILocator ActionsColumn(ILocator TableRows) => TableRows.Locator(":scope > td:last-child");
        public ILocator ContractEndColumn(ILocator TableRows) => TableRows.Locator(":scope > td:nth-child(8)");
        public ILocator PaginationRow => _page.Locator("table tbody tr:last-child");
        public ILocator PaginationNextButton => PaginationRow.GetByRole(AriaRole.Button).Last;
        public ILocator PaginationPreviousButton => PaginationRow.GetByRole(AriaRole.Button).First;

        // Side Navigation Bar
        // public ILocator ClientsNavButton => _page.GetByRole(AriaRole.Tab, new() { Name = "Clients" });
        public ILocator ClientsNavButton => _page.GetByRole(AriaRole.Link, new() { Name = "Clients Clients" });
        public ILocator AssetsNavButton => _page.GetByRole(AriaRole.Tab, new() { Name = "Assets" });
        public ILocator UsersNavButton => _page.GetByRole(AriaRole.Tab, new() { Name = "Users" });
        public ILocator UnassignedAVLsNavButton => _page.GetByRole(AriaRole.Tab, new() { Name = "UnassignedAVLs" });
        public ILocator PricingNavButton => _page.GetByRole(AriaRole.Tab, new() { Name = "Pricing Plan" });
        public ILocator ToolsNavButton => _page.GetByRole(AriaRole.Tab, new() { Name = "Tools" });

        // Search Bar
        public ILocator SearchBar => _page.Locator("app-search-combobox div").Nth(1);
        public ILocator SearchBarInput => _page.Locator("app-search-combobox").GetByRole(AriaRole.Textbox, new() { Name = "Search" });
        public ILocator OpenSearchBarDropdownButton => SearchBar.GetByRole(AriaRole.Button);
        public ILocator CloseSearchBarDropdownButton => SearchBar.GetByRole(AriaRole.Button).First;
        public ILocator SBClientIDFilterButton => _page.Locator("label[class='filter-item custom-checkbox']").Nth(0);
        public ILocator SBTradingNameFilterButton => _page.Locator("label[class='filter-item custom-checkbox']").Nth(2);
        public ILocator SBPhoneFilterButton => _page.Locator("label[class='filter-item custom-checkbox']").Nth(4);
        public ILocator SBEmailFilterButton => _page.Locator("label[class='filter-item custom-checkbox']").Nth(6);
        public ILocator SBConnectionsFilterButton => _page.Locator("app-search-combobox").GetByText("Connections");
        public ILocator SBCompanyNameFilterButton => _page.Locator("label[class='filter-item custom-checkbox']").Nth(1);
        public ILocator SBContactFilterButton => _page.Locator("label[class='filter-item custom-checkbox']").Nth(3);
        public ILocator SBMobileFilterButton => _page.Locator("label[class='filter-item custom-checkbox']").Nth(5);
        public ILocator SBContractEndFilterButton => _page.Locator("label[class='filter-item custom-checkbox']").Nth(7);

        // Filter Dropdown
        public ILocator FilterDropdownButton => _page.GetByRole(AriaRole.Button, new() { Name = "Filter", Exact = true });
        public ILocator FilterSelectAllButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Select All" });
        public ILocator FilterActiveButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Active", Exact = true });
        public ILocator FilterInactiveButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Inactive", Exact = true });
        public ILocator FilterClearAllButton => _page.GetByRole(AriaRole.Button, new() { Name = "Clear All", Exact = true });
        public ILocator FilterApplyButton => _page.GetByRole(AriaRole.Button, new() { Name = "Apply", Exact = true });

        // Columns Dropdown
        public ILocator ColumnsDropdownButton => _page.GetByRole(AriaRole.Button, new() { Name = "Columns", Exact = true });
        public ILocator ColumnsSelectAllButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Select All" });
        public ILocator ColumnsClientIDButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Client ID", Exact = true });
        public ILocator ColumnsCompanyNameButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Company Name", Exact = true });
        public ILocator ColumnsTradingNameButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Trading Name", Exact = true });
        public ILocator ColumnsContactButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Contact", Exact = true });
        public ILocator ColumnsPhoneButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Phone", Exact = true });
        public ILocator ColumnsMobileButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Mobile", Exact = true });
        public ILocator ColumnsEmailButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Email", Exact = true });
        public ILocator ColumnsContractEndButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Contract End", Exact = true });
        public ILocator ColumnsConnectionsButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Connections", Exact = true });
        public ILocator ColumnsActionsButton => _page.GetByRole(AriaRole.Checkbox, new() { Name = "Actions", Exact = true });
        public ILocator ColumnsApplyButton => _page.GetByRole(AriaRole.Button, new() { Name = "Apply", Exact = true });
        public ILocator ColumnsClearAllButton => _page.GetByRole(AriaRole.Button, new() { Name = "Clear All", Exact = true });


        public ILocator AddClientButton => _page.GetByRole(AriaRole.Button, new() { Name = "Add Client", Exact = true });

        public async Task GoTo()
        {
            await _page.GotoAsync(DashboardUrl);
        }

        // public async Task ClickClientsTab()
        // {
        //     throw new NotImplementedException("Method not implemented yet.");
        // }
        public async Task ClickAddClientButton()
        {
            await AddClientButton.ClickAsync();
        }

        public async Task FillInSearchBar(string searchText)
        {
            await SearchBarInput.FillAsync(searchText);
        }
        public async Task OpenSBFilterButton()
        {
            await OpenSearchBarDropdownButton.ClickAsync();
        }

        public async Task CloseSBFilterButton()
        {
            await CloseSearchBarDropdownButton.ClickAsync();
        }

        public async Task ClickSBFilterClearAllButton()
        {
            await ClickSBClientIDFilterButton();
            await ClickSBTradingNameFilterButton();
            await ClickSBPhoneFilterButton();
            await ClickSBEmailFilterButton();
            await ClickSBConnectionsFilterButton();
            await ClickSBCompanyNameFilterButton();
            await ClickSBContactFilterButton();
            await ClickSBMobileFilterButton();
            await ClickSBContractEndFilterButton();
        }

        public async Task ClickSBClientIDFilterButton()
        {
            await SBClientIDFilterButton.ClickAsync();
        }
        public async Task ClickSBTradingNameFilterButton()
        {
            await SBTradingNameFilterButton.ClickAsync();
        }
        public async Task ClickSBPhoneFilterButton()
        {
            await SBPhoneFilterButton.ClickAsync();
        }
        public async Task ClickSBEmailFilterButton()
        {
            await SBEmailFilterButton.ClickAsync();
        }
        public async Task ClickSBConnectionsFilterButton()
        {
            await SBConnectionsFilterButton.ClickAsync();
        }
        public async Task ClickSBCompanyNameFilterButton()
        {
            await SBCompanyNameFilterButton.ClickAsync();
        }
        public async Task ClickSBContactFilterButton()
        {
            await SBContactFilterButton.ClickAsync();
        }
        public async Task ClickSBMobileFilterButton()
        {
            await SBMobileFilterButton.ClickAsync();
        }
        public async Task ClickSBContractEndFilterButton()
        {
            await SBContractEndFilterButton.ClickAsync();
        }

        public async Task ClickFilterButton()
        {
            await FilterDropdownButton.ClickAsync();
        }
        public async Task ClickFilterSelectAllButton()
        {
            await FilterSelectAllButton.ClickAsync();
        }
        public async Task ClickFilterActiveButton()
        {
            await FilterActiveButton.ClickAsync();
        }
        public async Task ClickFilterInactiveButton()
        {
            await FilterInactiveButton.ClickAsync();
        }
        public async Task ClickFilterClearAllButton()
        {
            await FilterClearAllButton.ClickAsync();
        }
        public async Task ClickFilterApplyButton()
        {
            await FilterApplyButton.ClickAsync();
        }

        public async Task ClickColumnsButton()
        {
            await ColumnsDropdownButton.ClickAsync();
        }
        public async Task ClickColumnsSelectAllButton()
        {
            await ColumnsSelectAllButton.ClickAsync();
        }
        public async Task ClickColumnsClientIDButton()
        {
            await ColumnsClientIDButton.ClickAsync();
        }
        public async Task ClickColumnsCompanyNameButton()
        {
            await ColumnsCompanyNameButton.ClickAsync();
        }
        public async Task ClickColumnsTradingNameButton()
        {
            await ColumnsTradingNameButton.ClickAsync();
        }
        public async Task ClickColumnsContactButton()
        {
            await ColumnsContactButton.ClickAsync();
        }
        public async Task ClickColumnsPhoneButton()
        {
            await ColumnsPhoneButton.ClickAsync();
        }
        public async Task ClickColumnsMobileButton()
        {
            await ColumnsMobileButton.ClickAsync();
        }
        public async Task ClickColumnsEmailButton()
        {
            await ColumnsEmailButton.ClickAsync();
        }
        public async Task ClickColumnsContractEndButton()
        {
            await ColumnsContractEndButton.ClickAsync();
        }
        public async Task ClickColumnsConnectionsButton()
        {
            await ColumnsConnectionsButton.ClickAsync();
        }
        public async Task ClickColumnsActionsButton()
        {
            await ColumnsActionsButton.ClickAsync();
        }
        public async Task ClickColumnsApplyButton()
        {
            await ColumnsApplyButton.ClickAsync();
        }
        public async Task ClickColumnsClearAllButton()
        {
            await ColumnsClearAllButton.ClickAsync();
        }

        public async Task CheckTableHeaders(List<string> expectedHeaders)
        {
            // This method can be used to check if the table headers are present
            var headers = (await VisibleTableHeaders.AllInnerTextsAsync())
            .Select(h => h.Trim()).ToList();
            Assert.That(headers, Is.EqualTo(expectedHeaders));
            foreach (var header in headers)
            {
                Console.WriteLine("Header: " + header);
            }
        }

        public async Task CheckClientIDsExists(List<string> clientID)
        {
            var rows = TableRows;
            int rowCount = await rows.CountAsync();
            var clientIds = new List<string>();

            for (int i = 0; i < rowCount - 1; i++)
            {
                var currentRow = rows.Nth(i);
                var clientId = await ClientID(currentRow).TextContentAsync();
                if (clientId == null)
                {
                    continue;
                }
                clientIds.Add(clientId);
            }
            var filteredClientIds = clientIds.Where(id => id.All(char.IsDigit)).ToList();
            Assert.That(filteredClientIds, Is.EqualTo(clientID));
        }

        public async Task GoToClientDetailsPage(string clientId)
        {
            await OpenSBFilterButton();
            await ClickSBFilterClearAllButton();
            await ClickSBClientIDFilterButton();
            await FillInSearchBar(clientId);
            await CloseSBFilterButton();
            var rows = TableRows;
            await ActionsColumn(rows).GetByRole(AriaRole.Button).First.ClickAsync();

        }
        public async Task SearchClientByID(string clientId)
        {
            await OpenSBFilterButton();
            await ClickSBFilterClearAllButton();
            await ClickSBClientIDFilterButton();
            await FillInSearchBar(clientId);
            await CloseSBFilterButton();
        }

        public async Task CheckClientContractEndRed(string clientId)
        {
            await SearchClientByID(clientId);
            var rows = TableRows;
            var contractEndColumn = ContractEndColumn(rows);
            var contractEndSpan = contractEndColumn.Locator("span.text-danger");
            Assert.That(await contractEndSpan.IsVisibleAsync(), Is.True);
        }

        public async Task ClickPaginationNextButton()
        {
            await PaginationNextButton.ClickAsync();
        }
        public async Task ClickPaginationPreviousButton()
        {
            await PaginationPreviousButton.ClickAsync();
        }
        public async Task ChangePaginationRowsTo5()
        {
            var paginationRow = PaginationRow;
            await paginationRow.Locator("div[class='custom-select w-100']").ClickAsync();
            await _page.GetByRole(AriaRole.List).GetByText("5", new() { Exact = true }).ClickAsync();
        }
        public async Task ChangePaginationRowsTo10()
        {
            var paginationRow = PaginationRow;
            await paginationRow.Locator("div[class='custom-select w-100']").ClickAsync();
            await _page.GetByRole(AriaRole.List).GetByText("10", new() { Exact = true }).ClickAsync();
        }
        public async Task ChangePaginationRowsTo20()
        {
            var paginationRow = PaginationRow;
            await paginationRow.Locator("div[class='custom-select w-100']").ClickAsync();
            await _page.GetByRole(AriaRole.List).GetByText("20", new() { Exact = true }).ClickAsync();
        }
        public async Task ChangePaginationRowsTo50()
        {
            var paginationRow = PaginationRow;
            await paginationRow.Locator("div[class='custom-select w-100']").ClickAsync();
            await _page.GetByRole(AriaRole.List).GetByText("50", new() { Exact = true }).ClickAsync();
        }
    }
}