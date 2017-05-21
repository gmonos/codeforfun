# Elasticsearch

## Installation d'Elasticsearch sur docker

### CMD pour installer Elasticsearch

> docker pull elasticsearch

une image va donc etre crée depuis l'image officielle d'elasticsearch disponible dans le hub de docker

### CMD pour lancer Elasticsearch

> docker run -p 9200:9200 -e "http.host=0.0.0.0" -e "transport.host=127.0.0.1" --name es elasticsearch

le -p spécifie les ports à binder le 1er étant celui de du host, le deuxieme celui du containeur

le -e set une variable d'environnement du container, ici on spécifie donc le http.host et le transport.host d'elasticsearch

le --name spécifie le nom donné au container.

### Accéder à Elasticsearch

dès lors elasticsearch est accessible sur l'adresse ip de la machine distante : http://10.12.10.64:9200

vous pouvez utiliser postman et faire un get sur cette adresse, s'il est bien installé vous auriez une réponse de ce type :


```
{
  "name": "mHHVlGP",
  "cluster_name": "elasticsearch",
  "cluster_uuid": "mfHsvgDJQJu50KXVGHjX-w",
  "version": {
    "number": "5.2.0",
    "build_hash": "24e05b9",
    "build_date": "2017-01-24T19:52:35.800Z",
    "build_snapshot": false,
    "lucene_version": "6.4.0"
  },
  "tagline": "You Know, for Search"
}
```

Afin de monitorer elasticsearch nous allons installer Kibana:
[Installation de Kibana](./Kibana.md)