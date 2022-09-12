# Presentation

In diesem Ordner sind alle Präsentationen, die auch in den YouTube-Videos zu sehen sind bzw. zu sehen sein werden.

## Setup

Um die Präsentationen zu generieren benötigst Du [Node.js 14](https://nodejs.org/) oder höher. 
Um zu schauen, ob Node.js korrekt installiert wurde, kannst Du ein einer Kommandozeile folgenden Befehl ausführen:

```
node --version
```

Ist es erfolgreich, wird die aktuell installierte und aktive Version von Node.js angezeigt.
Siehst Du einen Fehler, wurde Node.js noch nicht oder nicht richtig installiert. 

Nach erfolgreicher Installation musst Du einmalig in diesem Ordner, wo sich auch diese README.md befindet, die Du gerade liest, folgenden Befehl ausführen:

```
npm install
```

Mit diesem Befehl werden alle Abhängigkeiten installiert, die für die Präsentationen benötigt werden.

## Präsentationen anzeigen

Die Präsentationen werden mit der [Marp CLI](https://marp.app) erstellt und angezeigt.

Du kannst Dir eine Präsentation ansehen, wenn Du in einer Kommandozeile folgenden Befehl ausführst:

```
npm run 01-getting-started
```

Nach einem kurzem Moment öffnet sich die Präsentation in einem kleinen Vorschaufenster zum Ansehen.

Generell wirst Du jede Präsenation, die sich im Laufe des Projekts ergibt, mit einem `npm run <NameDesOrdner>` Dir ansehen können.
