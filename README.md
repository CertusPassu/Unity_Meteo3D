
Description

Projet pédagogique d'une application 3D de météo avec les attendus suivant

Faire des appels API pour récupérer la météo d'une ville que l'on indiquerai dans un champ prévu à cet effet.
Faire tourner la Terre pour faire face à la ville demandée.
Obtenir la météo sur un clic de souris.

L'api utilisé.
Plusieurs choix d'API se sont présenté, la plupart étant payant et/ou limité dans leur utilisation, mon choix s'est arreté sur deux API.

Open Weather Map Une API tres interessante et en prtie gratuite, mais son utilisation restant limitée a 1000 appels par jours, je ne l'ai pas retenu.
Open Meteo : https://open-meteo.com/ Une API gratuite et libre d'acces dont les appels ne sont pas limité et qui ne nécéssite aucune clé pour etre utilisée. J'ai donc utilisé cette API.
La question s'est posée d'utiliser l'API Find Place de Google pour développer ma prédiction et l'autocompletion des ville sur mon champ de recherche, mais pour des raisons de performances reseaux, je suis partie sur un JSON contenant une grande selection de ville
