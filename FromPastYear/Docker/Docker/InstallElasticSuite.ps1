Write-Host "------------------ ElasticSearch installation ------------------"
Invoke-Expression ./installElasticsearch.ps1

Write-Host "------------------ Kibana installation ------------------"
Invoke-Expression ./installKibana.ps1

Write-Host "------------------ Logstash installation ------------------"
Invoke-Expression ./installLogstash.ps1
