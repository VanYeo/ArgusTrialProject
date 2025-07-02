### Open Terminal, go to ArgusTrialTest and run either of the following commands ###:

1) 
# Windows PowerShell
$env:PWDEBUG="console"
dotnet test

2)
dotnet test --settings playwright.runsettings



### You may need to run the following commands first ###:

1) For Installing EF Core CLI Tool
dotnet tool install --global dotnet-ef

2) Placing Tool in PATH
setx PATH "%PATH%;%USERPROFILE%\.dotnet\tools"

3) Installing Playwright for .NET
dotnet add package Microsoft.Playwright
dotnet add package Microsoft.Playwright.NUnit

4) Install Browsers
pwsh bin/Debug/net8.0/playwright.ps1 install
