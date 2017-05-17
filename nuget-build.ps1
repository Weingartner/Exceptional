param([string]$apikey) 

# Remove any old nuget packages
gci -r -include bin, obj | rm -rec -fo
# Build new nuget packages
msbuild Exceptional.sln /t:Weingartner_Exceptional:Pack /t:Weingartner_Exceptional_ReactiveUI:Pack /p:Configuration=Release
# Get all nuget packages under the specific folders
$packages = gci -r -include *.nupkg -path Weingartner.Exceptional,Weingartner.Exceptional.ReactiveUI
# Publish them all
foreach ($package in $packages) {
    & "C:\Program Files\dotnet\dotnet.exe" nuget push $package
}

