# Kibana

## Installation de Kibana sur docker

### CMD pour installer Kibana

> docker pull kibana

Une image va donc etre crée depuis l'image officielle de Kibana disponible dans le hub de docker

### CMD pour lancer Kibana

> docker run -p 5601:5601 -e ELASTICSEARCH_URL=http://10.12.10.64:9200 --name kb kibana

Le -p spécifie les ports à binder le 1er étant celui de du host, le deuxieme celui du containeur

Le -e set une variable d'environnement du container, ici on spécifie donc le ELASTICSEARCH_URL

Le --link sert à lié un container à un autre, le nom des container deviens donc important ici. // plus utilisé ici

Le --name spécifie le nom donné au container.

### Accéder à Kibana

Dès lors elasticsearch est accessible sur http://10.12.10.64:5601




```

```

Nous pouvons maintenant installer Logstash afin de récupérer des logs et de les insérer dans elasticsearch

[Installation de Logstash](./Logstash.md)