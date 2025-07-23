After starting frontend & backend, run the tests through VSC Testing Sidebar Panel.
View Playwright tracing of the tests by running the command below in ArgusTrialTest folder.
The command to open the test tracing follows the structure of "TestModule.TestNameTestID.zip"

powershell -File bin/Debug/net8.0/playwright.ps1 show-trace bin/Debug/net8.0/playwright-traces/ArgusTrialTest.Tests.LoginTest.EmptyEmailLoginAIT2.zip

### Login Test Module ###
SuccessfulUserLoginAIT1
SuccessfulAdminLoginAIT1
EmptyEmailLoginAIT2
EmptyPWLoginAIT3
InvalidLoginAIT4
ForgotPasswordAIT5
PWVisibilityToggleAIT6

### Client List Test Module ###
ViewClientListAIT9
SearchForClientAIT10
FilterActiveClientListAIT11
FilterInactiveClientListAIT11
CustomiseColumnsAIT12
CheckAddNewClientRedirectAIT13
CheckEditClientRedirectAIT14
CheckClientEndDateAIT15
CheckPaginationNavigationAIT16
CheckPaginationRowDisplayAIT16

### Open Terminal, go to ArgusTrialTest and run either of the following commands ###:

1) 
### Windows PowerShell ###
$env:PWDEBUG="console"
dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Channel=chrome
dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Channel=msedge

2)
dotnet test --settings playwright.runsettings



### You may need to run the following commands first ###:

1) Installing Playwright for .NET
dotnet add package Microsoft.Playwright
dotnet add package Microsoft.Playwright.NUnit

2) Install Browsers
pwsh bin/Debug/net8.0/playwright.ps1 install
OR 
powershell -File bin/Debug/net8.0/playwright.ps1 install


#### TO VIEW THE STEP BY STEP TRACING OF THE TESTS, RUN ####

powershell -File bin/Debug/net8.0/playwright.ps1 show-trace bin/Debug/net8.0/playwright-traces/ArgusTrialTest.Tests.LoginTest.EmptyEmailLoginAIT2.zip

