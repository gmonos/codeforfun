[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string] $esurl,

    [Parameter(Mandatory=$True,Position=2)]
    [string] $indextype,

    [Parameter(Mandatory=$True,Position=3)]
    [string] $indexname,

    [Parameter(Mandatory=$True,Position=4)]
    [string] $filepath
)


Write-Host "Insert "$indexname $indextype" started"

$url = $esurl + ".kibana/" + $indextype + "/" + $indexname

Write-Host $url

$content = Get-Content $filepath

Invoke-WebRequest -Uri $url -Method POST -ContentType "application/json" -Body $content

Write-Host "Insert "$indexname $indextype" done"