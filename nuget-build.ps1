param([string]$apikey) 

function push {
    param ([string]$proj, [string]$config)

    $csproj = "$proj\$proj.csproj"

    Write-Host "PACKING NOW $csproj $config" -foregroundcolor Blue
    $cmd = "nuget pack $csproj -Prop Platform=AnyCPU -Prop Configuration=$config -symbols"

    # parse the output from nuget packaging command
    echo $cmd
    iex $cmd  | Tee-Object -Variable output

    $a = echo $output |  Select-String nupkg | Select-String -NotMatch symbols.nupkg
    $a = $a -Replace "^[^']*'", ""
    $a = $a -Replace "'[^']*$", ""


    write-host "PUSHING NOW $csproj $config" -ForegroundColor Blue
    $cmd = "nuget push $a $apikey -Source https://www.nuget.org/api/v2/package/" 
    echo $cmd
    iex $cmd

}

push "Exceptional" "Release"
push "Exceptional.ReactiveUI" "Release"