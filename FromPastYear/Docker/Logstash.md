# Logstash

## Création de l'image de logstash et installation

> docker build -t logstashzags .

Cette commande va créer une image à partir d'un dockerfile présent dans le répertoire courant.

le dockerfile sert à créer une image, ici on va partir de l'image de logstash

```
FROM logstash

COPY logstash.conf /bin/
RUN mkdir logs
RUN mkdir logs/app
RUN mkdir logs/web

RUN logstash-plugin install logstash-filter-grok

CMD ["-f", "/bin/logstash.conf"]
```

Dans ce fichier on récupére l'image de logstash,

on copie le fichier de configuration dans le /bin du container,

on crée un dossier logs,

on exécute la commande d'installation d'un plugin afin d'utiliser le filtre grok pour formater comme on le désire les logs,

enfin on rajoute le -f pour forcer logstash à utiliser le fichier de conf.

le fichier de conf ressemble à ceci :
```
input {
  file {
    path => "/logs/*.log"
	start_position => beginning
	stat_interval => 1
  }
  stdin{}
}

filter {
	if [path] =~ "API" {
		grok {
			match => {"message" => "%{TIMESTAMP_ISO8601:datetime};%{LOGLEVEL:loglevel};(?<application>[\/(),:'a-zA-Z0-9 ._-]+);%{URIPROTO:method};(?<action>[\/(),:'a-zA-Z0-9 ._-]+);%{INT:code};(?<correlationid>[\/(),:'a-zA-Z0-9 ._-]+);%{TIME:duration}"}
		}
		mutate { add_field => { "kind" => "api" } }
	}
	else if [path] =~ "Error" {
		grok {
			match => {"message" => "%{TIMESTAMP_ISO8601:datetime};%{LOGLEVEL:loglevel};(?<application>[\/(),:'a-zA-Z0-9 ._-]+);(?<currentmessage>[\/(),:'a-zA-Z0-9 ._-]+);(?<type>[\/(),:'a-zA-Z0-9 ._-]+);(?<correlationid>[\/(),:'a-zA-Z0-9 ._-]+)"}
		}
		mutate { add_field => { "kind" => "error" } }
	} 
	date {
		match => [ "timestamp" , "dd/MMM/yyyy:HH:mm:ss Z" ]
	}
}

output { 
	if [kind] == "api" 
	{
		elasticsearch {
			hosts => ["172.17.0.2:9200"]
			index => "apilog-%{+YYYY.MM.dd}"
		}	
	}
	else if [kind] == "error" 
	{
		elasticsearch {
			hosts => ["172.17.0.2:9200"]
			index => "errorlog-%{+YYYY.MM.dd}"
		}
	}
	else
	{
		elasticsearch {
			hosts => ["172.17.0.2:9200"]
			index => "nothandledlog-%{+YYYY.MM.dd}"
		}
	}
  stdout { codec => rubydebug }
}
```

### CMD pour lancer Logstash

> docker run -v C:/LOG_IGO_FR/APP:/logs --name lt --rm logstashzags

Le -v spécifie un volume à monter du coté du container.

ici on veux partager le chemin C:/LOG_IGO_FR/APP dans la debian de destination à l'endroit /logs

Le --link sert à lié un container à un autre, le nom des container deviens donc important ici.// plus nécessaire maintenant

Le --rm supprimer automatiquement le container s'il existe déjà

Le --name spécifie le nom donné au container.

### Utile

pour le filtre grok voici un lien super qui permet de construire son expression en suivant des exemples de logs :

[http://grokconstructor.appspot.com/do/constructionstep]