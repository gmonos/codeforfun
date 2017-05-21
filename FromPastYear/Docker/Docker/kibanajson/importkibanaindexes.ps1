[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string] $esurl
)

$args = @()
$args += ("-esurl", $esurl)
$args += ("-indextype", "index-pattern")
$args += ("-indexname", "apilog-*")
$args += ("-filepath", "./apilog-index-pattern.json")

$cmd = "./genericimportkibanaindex.ps1"

Invoke-Expression "$cmd  $args"

$args = @()
$args += ("-esurl", $esurl)
$args += ("-indextype", "index-pattern")
$args += ("-indexname", "errorlog-*")
$args += ("-filepath", "./errorlog-index-pattern.json")

Invoke-Expression "$cmd  $args"

$args = @()
$args += ("-esurl", $esurl)
$args += ("-indextype", "search")
$args += ("-indexname", "apisearch")
$args += ("-filepath", "./api-search.json")

Invoke-Expression "$cmd  $args"

$args = @()
$args += ("-esurl", $esurl)
$args += ("-indextype", "search")
$args += ("-indexname", "errorsearch")
$args += ("-filepath", "./errors-search.json")

Invoke-Expression "$cmd  $args"

$args = @()
$args += ("-esurl", $esurl)
$args += ("-indextype", "visualization")
$args += ("-indexname", "errors-visualization")
$args += ("-filepath", "errors-visualisation.json")

Invoke-Expression "$cmd  $args"
