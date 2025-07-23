using Microsoft.Playwright;
using System.Threading.Tasks;
using ArgusTrialTest.Utils;
using NUnit.Framework.Internal;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Constraints;
using Microsoft.VisualBasic;

namespace ArgusTrialTest.Pages

{
    public class ClientDetailPage
    {
        private readonly IPage _page;
        private readonly string _clientId;
        private string ClientDetailPageUrl => $"http://127.0.0.1:57123/clients/{_clientId}";
        public ClientDetailPage(IPage page, string clientId)
        {
            _page = page;
            _clientId = clientId;
        }

        // Edit Button
        public ILocator EditButton => _page.GetByRole(AriaRole.Button, new() { Name = "Edit", Exact = true });
        public ILocator ContractStartDateInput => _page.GetByPlaceholder("Choose a date");
        public ILocator SaveButton => _page.GetByRole(AriaRole.Button, new() { Name = "Save Changes", Exact = true });
        public ILocator CancelButton => _page.GetByRole(AriaRole.Button, new() { Name = "Cancel", Exact = true });
        public async Task GoTo()
        {
            await _page.GotoAsync(ClientDetailPageUrl);
        }
        public async Task ClickEditButton()
        {
            await EditButton.ClickAsync();
        }
        public async Task FillInContractStartDate(string date)
        {
            await ContractStartDateInput.FillAsync(date);
        }
        public async Task ClickSaveButton()
        {
            await SaveButton.ClickAsync();
        }
        public async Task ClickCancelButton()
        {
            await CancelButton.ClickAsync();
        }
    }
}