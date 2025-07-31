using Microsoft.Playwright;
using System.Threading.Tasks;
using ArgusTrialTest.Utils;
using NUnit.Framework.Internal;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Constraints;
using Microsoft.VisualBasic;
using Microsoft.Win32.SafeHandles;



namespace ArgusTrialTest.Pages

{
    public class AddClientPage
    {
        private readonly IPage _page;
        private string? _clientId;
        private const string AddClientPageUrl = "http://127.0.0.1:57123/clients/add";
        public AddClientPage(IPage page) => _page = page;

        public ILocator CancelButton => _page.GetByRole(AriaRole.Button, new() { Name = "Cancel", Exact = true });
        public ILocator AddClientButton => _page.GetByRole(AriaRole.Button, new() { Name = "Add Client", Exact = true });


        public ILocator ClientId => _page.Locator("input[formcontrolname='accountNumber']");
        public ILocator CompanyName => _page.Locator("input[formcontrolname='companyName']");
        public ILocator Phone => _page.Locator("input[formcontrolname='phone']");
        public ILocator Mobile => _page.Locator("input[formcontrolname='mobile']");
        public ILocator Contact => _page.Locator("input[formcontrolname='contact']");
        public ILocator TradingNameOptional => _page.Locator("input[formcontrolname='tradingName']");

        public ILocator LoginEmail => _page.Locator("input[formcontrolname='loginEmail']");
        public ILocator Password => _page.Locator("input[type='password']");

        public ILocator AccountName => _page.Locator("input[formcontrolname='accountName']");
        public ILocator AccountEmail => _page.Locator("input[formcontrolname='accountEmail']");
        public ILocator AccountEmailBill => _page.Locator("input[formcontrolname='accountEmailBill']");
        public ILocator AccountPhone => _page.Locator("input[formcontrolname='accountPhone']");
        public ILocator AccountCompanyName => _page.Locator("input[formcontrolname='accountCompanyName']");
        public ILocator AccountManager => _page.Locator("input[formcontrolname='accountManager']");

        public ILocator BillingStreet => _page.Locator("[data-testid='street']").First;
        public ILocator BillingCity => _page.Locator("[data-testid='city']").First;
        public ILocator BillingState => _page.Locator("[data-testid='state']").First;
        public ILocator BillingZip => _page.Locator("[data-testid='zip']").First;
        public ILocator BillingCountry => _page.Locator("[data-testid='country']").First;

        public ILocator CopyBillingAddress => _page.Locator("button[class='btn p-0']");
        public ILocator ShippingStreet => _page.Locator("[data-testid='street']").Last;
        public ILocator ShippingCity => _page.Locator("[data-testid='city']").Last;
        public ILocator ShippingState => _page.Locator("[data-testid='state']").Last;
        public ILocator ShippingZip => _page.Locator("[data-testid='zip']").Last;
        public ILocator ShippingCountry => _page.Locator("[data-testid='country']").Last;

        public ILocator ActiveAccountCheckbox => _page.Locator("input[id='activeAccount']");
        public ILocator MasterAccountCheckbox => _page.Locator("input[id='masterAccount']");
        public ILocator BillingCsvCheckbox => _page.Locator("input[id='billingCsv']");
        public ILocator EjobsClientCheckbox => _page.Locator("input[id='ejobsClient']");
        public ILocator GogCheckbox => _page.Locator("input[id='gog']");

        public ILocator SmartRenewCheckbox => _page.Locator("input[id='smartRenew']");
        public ILocator CustomBrandingCheckbox => _page.Locator("input[id='customBranding']");
        public ILocator SendMessageCheckbox => _page.Locator("input[id='sendMessage']");

        public ILocator SosEventPushCheckbox => _page.Locator("input[id='sosEventPush']");
        public ILocator PvbsClientCheckbox => _page.Locator("input[id='pvbsClient']");
        public ILocator VworkClientCheckbox => _page.Locator("input[id='vworkClient']");
        public ILocator SsoCheckbox => _page.Locator("input[id='sso']");
        public ILocator ApiKeyCheckbox => _page.Locator("input[id='apiKey']");

        public ILocator ContractStartDateInput => _page.Locator("input[formcontrolname='startDate']");
        public ILocator RadioButton36 => _page.Locator("input[value='36']");
        public ILocator RadioButtonOpen => _page.Locator("input[value='open']");
        public ILocator RadioButtonOther => _page.Locator("input[value='custom']");
        public ILocator OtherRadioButtonInput => _page.Locator("input[formcontrolname='customValue']");

        public ILocator PPAVLCheckbox => _page.Locator("input[id='assignPPToAllAVLs']");
        public ILocator RolloverAgreementCheckbox => _page.Locator("input[id='rolloverAgreement']");
        public ILocator NonBillingAccountLCheckbox => _page.Locator("input[id='nonBillingAccount']");


        public ILocator ErrorMessage => _page.Locator(".Mui-error");
        public ILocator InvalidFormErrorMessage => _page.GetByText("Form is invalid");
        public ILocator RequiredFieldErrorMessage => _page.GetByText("Required");

        public ILocator RoadRedPlanDropdown => _page.Locator(".custom-select").First;
        public ILocator IOTPlanDropdown => _page.Locator("div:nth-child(6) > .custom-select");
        public ILocator SoftwarePlanDropdown => _page.Locator("div:nth-child(7) > .custom-select");

        public async Task GoTo()
        {
            await _page.GotoAsync(AddClientPageUrl);
        }

        public async Task<string> GetClientId()
        {
            string clientId = await ClientId.InputValueAsync();
            _clientId = clientId;
            return clientId;
        }

        public async Task ClickAddClientButton()
        {
            await AddClientButton.ClickAsync();
        }

        public async Task ClickCancelButton()
        {
            await CancelButton.ClickAsync();
        }

        public async Task FillGeneralInformation(
            string companyName,
            string phone,
            string mobile,
            string contact,
            string? tradingNameOptional = null
            )
        {
            await CompanyName.FillAsync(companyName);
            await Phone.FillAsync(phone);
            await Mobile.FillAsync(mobile);
            await Contact.FillAsync(contact);
            if (!string.IsNullOrEmpty(tradingNameOptional))
            {
                await TradingNameOptional.FillAsync(tradingNameOptional);
            }
        }

        public async Task FillLoginInformation(string loginEmail)
        {
            await LoginEmail.FillAsync(loginEmail);
        }

        public async Task FillAccountInformation(string accountName, string accountEmail, string accountEmailBill,
            string accountPhone, string accountCompanyName, string accountManager)
        {
            await AccountName.FillAsync(accountName);
            await AccountEmail.FillAsync(accountEmail);
            await AccountEmailBill.FillAsync(accountEmailBill);
            await AccountPhone.FillAsync(accountPhone);
            await AccountCompanyName.FillAsync(accountCompanyName);
            await AccountManager.FillAsync(accountManager);
        }

        public async Task FillBillingAddress(string billingStreet, string billingCity, string billingState,
            string billingZip, string billingCountry)
        {
            await BillingStreet.FillAsync(billingStreet);
            await BillingCity.FillAsync(billingCity);
            await BillingState.FillAsync(billingState);
            await BillingZip.FillAsync(billingZip);
            await BillingCountry.FillAsync(billingCountry);
        }

        public async Task CopyBillingAddressToShipping()
        {
            await CopyBillingAddress.ClickAsync();
        }

        public async Task CheckForInvalidFormErrorMessage()
        {
            await Microsoft.Playwright.Assertions.Expect(InvalidFormErrorMessage).ToBeVisibleAsync();
        }

        public async Task CheckForRequiredErrorMessage()
        {
            int count = await RequiredFieldErrorMessage.CountAsync();
            Console.WriteLine($"Count of required field error messages: {count}");
            Assert.That(count, Is.GreaterThanOrEqualTo(1));
        }

        public async Task CheckAllTickboxes()
        {
            await ActiveAccountCheckbox.CheckAsync();
            await MasterAccountCheckbox.CheckAsync();
            await BillingCsvCheckbox.CheckAsync();
            await EjobsClientCheckbox.CheckAsync();
            await GogCheckbox.CheckAsync();
            await SmartRenewCheckbox.CheckAsync();
            await CustomBrandingCheckbox.CheckAsync();
            await SendMessageCheckbox.CheckAsync();
            await SosEventPushCheckbox.CheckAsync();
            await PvbsClientCheckbox.CheckAsync();
            await VworkClientCheckbox.CheckAsync();
            await SsoCheckbox.CheckAsync();
            await ApiKeyCheckbox.CheckAsync();
        }
        public async Task FillContractPeriod(string startDate, string? duration = null)
        {
            await ContractStartDateInput.FillAsync(startDate);

            await RadioButtonOpen.CheckAsync();
            await RadioButton36.CheckAsync();
            await RadioButtonOther.CheckAsync();
            if (!string.IsNullOrEmpty(duration))
            {
                await OtherRadioButtonInput.FillAsync(duration);
            }
        }

        public async Task SelectDefaultPlans()
        {
            await RoadRedPlanDropdown.ClickAsync();
            await _page.GetByRole(AriaRole.Listitem).First.ClickAsync(new() { Force = true });
            await IOTPlanDropdown.ClickAsync();
            await _page.GetByRole(AriaRole.Listitem).First.ClickAsync(new() { Force = true });
            await SoftwarePlanDropdown.ClickAsync();
            await _page.GetByRole(AriaRole.Listitem).First.ClickAsync(new() { Force = true });
        }

        public async Task SelectContractOptionsCheckbox()
        {
            await PPAVLCheckbox.CheckAsync();
            await RolloverAgreementCheckbox.CheckAsync();
            await NonBillingAccountLCheckbox.CheckAsync();
        }
        public async Task UnselectContractOptionsCheckbox()
        {
            await PPAVLCheckbox.UncheckAsync();
            await RolloverAgreementCheckbox.UncheckAsync();
            await NonBillingAccountLCheckbox.UncheckAsync();
        }
    }
}