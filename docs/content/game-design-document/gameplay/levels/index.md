---
title: Levels
linkTitle: Levels
description: Informationen über Levels
weight: 70
---

Im Spiel hat der Spieler die Möglichkeit, verschiedene Levels zu spielen.
In jedem Level muss er eine gewisse Anzahl an [Wellen schaffen](../enemies/), bevor er das nächste Level spielen kann.

Die Levels werden auf zwei verschiedene Arten schwieriger:

1) Jede Welle eines Levels wird schwieriger als die vorangegangene Welle.
2) Jedes weitere Level ist etwas schwieriger als das Level zuvor. 

Es wird daher kein genereller Schwierigkeitsgrad benötigt.
Die Schwierigkeit steigt kontinuierlich innerhalb jedes und mit jedem Level an.

Durch das [loop-basierte Gameplay](../loop-gameplay/) kann der Spieler ein Level mehrmals spielen.
Daher ist es so, dass der Spieler nicht in der Lage sein soll, jedes Level beim ersten Durchspielen bis zur letzten Welle zu schaffen.
Das wird erreicht, dass nach der x-ten Welle, die x+1-te Welle überproportional schwerer ist.
Erst durch Forschung und Freischaltung weiterer Türme kann der Spieler bis zur letzten Welle vordringen.

Es braucht zudem eine Mechanik, die es erlaubt, dass der Spieler Wellen vorzeitig rufen kann.
Das ermöglicht dem Spieler beim Wiederspielen eines leichteren Levels, die anfänglichen Wellen zu überspringen und sich auf die Herausforderung der späteren Wellen zu konzentrieren.

## Herausforderungsmodus

Jedes Level kann in einem Herausforderungsmodus gestartet werden.
In diesem Modus werden:

* Gegner stärker/schneller
* Pausezeiten zwischen Wellen kürzer
* Türme schwächer

Dieser Modus richtet sich an erfahrene Spieler, die bereits viel freigeschaltet haben und eine extra Portion Herausforderung wünschen.

Der Spieler soll in diesem Modus auf eine noch zu bestimmende Art und Weise belohnt werden.
