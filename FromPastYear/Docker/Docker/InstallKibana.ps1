[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string] $esurl
)

docker pull kibana
docker run -p 5601:5601 -e ELASTICSEARCH_URL=http://$esurl:9200 --name kb kibana

