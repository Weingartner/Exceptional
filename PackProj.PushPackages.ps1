$fakeExe = (gci .\packages\FAKE.*\tools\FAKE.exe)[0].FullName
$scriptPath = (gci .\packages\PackProj.*\scripts\push-all.fsx)[0].FullName

# If you use `nuget setApiKey` then set
$apiKey = ""
# else
# $apikey = "XXXXXXXXXXX"
$source = "https://api.nuget.org/v3/index.json"
if ($apiKey) { $apiKeyArg = "-ev api-key $apiKey" } else { $apiKeyArg = "" }
& $fakeExe $scriptPath -ev source $source $apiKeyArg
