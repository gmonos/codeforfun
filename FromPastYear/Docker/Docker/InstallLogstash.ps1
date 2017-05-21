[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string] $logpath
)

docker build -t logstashzags .
docker run -v $logpath:/logs --name lt logstashzags
