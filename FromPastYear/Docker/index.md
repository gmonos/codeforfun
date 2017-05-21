# Documentation relative à Docker

## Elasticseach
[Docker et Elasticseach](./Elasticsearch.md)

## Commandes utiles

Cette commande va executer un shell sur le container monimage.

> docker exec -it nomcontainer /bin/bash/

Bon à savoir, la debian a le strict minimum donc si vous voulez éditer un fichier text ou utiliser un telnet, il faudra utiliser cette commande :

> apt-get update

puis

> apt_get install XXX

XXX correspondant au soft que vous voulez installer.


Liste toutes les images montées sur votre instance docker.

> docker images

Liste tous les containers en cours et finis sur votre instance docker.

> docker ps -a

Supprime un container

> docker rm moncontainer

Supprime une image

> docker rmi monimage

Démarre et arrête un container.

> docker stop container

> docker start container

Démarre en mémoire un container

> docker run -it XXX

Construit une image à partir d'un fichier dockerfile

> docker build -t nomNouvelleImage repertoirecourant



