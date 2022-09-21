# Roadmap

[ [Dokumentation](README.md) ]

Die Roadmap beschreibt sehr grob den Ablauf des Projektes und die Punkte, die wir erreichen wollen.

Oft werden Roadmaps auch mit einem Datum versehen, wann ungefähr was fertig sein soll.
Auf ein Terminieren verzichten wir hier bewusst, da wir uns die Zeit nehmen, die wir brauchen.

## Phasen

In diesem Abschnitt folgt eine sehr grobe Definition, welche Phasen durchlaufen werden.
Ein Pfeil markiert, an welcher Stelle wir uns aktuell befinden.

1. Findung des Genres. `<-`
2. Findung des Themas (z.B. ob das Spiel in Richtung "Modern", "Fantasy", "Mittelalter" geht).
3. Findung des Stils
   
   Hier werden wir auf Basis des Themas versuchen, einen grafischen Stil zu finden, an dem sich die kreativen Köpfe orientieren sollen. 
4. Erstellung des Game Design Documents.
   
   Hier halten wir grob fest, was genau wir eigentlich für ein Spiel entwickeln wollen.
   Hier drin werden wir auch den MVP (siehe weiter unten) definieren.
5. Implementierung der Basis-Infrastruktur.

   Das hier wird bereits parallel zum aktuellen "Pfeil" mitlaufen, da diese Basis unabhängig vom Spieltyp ist und bereits implementiert werden kann.
6. Umsetzung des MVPs.
   
   Das bedeutet natürlich, dass hier modelliert, designt, entwickelt, getestet, etc. wird.

Jeden einzelnen Punkt werden wir im Laufe der Zeit weiter und genauer ausarbeiten, sobald wir die jeweilige Phase erreichen.

Nach **Punkt 3** können wir uns auch an eine Namensfindung machen.

Ab **Punkt 6**, also wenn's vom Spiel quasi was zu sehen gibt, können wir bereits das Spiel in einen Beta-Branch auf Steam deployen, sodass jeder, der dann einen Steam Key hat (wie hier die Verteilung aussieht, müssen wir noch schauen), den aktuellen Stand laden und testen kann.

## MVP

Mit einem MVP, kurz für Minimal Viable Product, wird versucht herauszufinden, ob es für ein gewisses Spiel einen Markt bzw. Zielgruppe gibt. 
Das ist natürlich dann interessant, wenn man mit einem Spiel einen finanziellen Erfolg erreichen möchte.
Merkt man durch den MVP, dass sich niemand für das Spiel interessiert, kann es sein, dass die Idee nicht gut ist und man über das Spiel nochmal nachdenken muss.

In unserem Fall ist das ein bisschen anders, da wir das Spiel kostenfrei zur Verfügung stellen werden und es vor allem auch um den Lernfaktor geht für jeden, der mitmachen möchte. 
Daher müssen wir keine "Forschung" betreiben, ob unser Spiel auf einen Markt trifft und einen finanziellen Erfolg bedeuten könnte.

Nichtsdestotrotz beschreibt ein MVP eben die kleinste Umsetzung der Kernidee.

Da die aktuelle Tendenz der Abstimmung stark Richtung Tower Defense geht, folgt eine grobe Beschreibung, wie ein MVP eines Tower Defense aussehen könnte.

### MVP Tower Defense

Ziel des MVPs für ein Tower Defense ist es, einen ersten Game Loop zu erreichen.
Dieser könnte wie folgt aussehen.

* Spiel starten (Spieler landet direkt auf einer Map)
* Spieler sieht einen Weg, links ein Portal, rechts unsere Basis.
* Gegner spawnen im Interval und laufen den Weg entlang.
* Spieler kann 1-2 verschiedene Türme bauen.
* Türme schießen auf den Gegner.
* Werden alle Gegner besiegt -> Gewonnen
* Erreichen die Gegner unsere Basis -> Verloren

Wenn wir diesen MVP erreicht haben, haben wir bereits einiges umgesetzt.

Natürlich gibt's hier viele Dinge noch nicht, Menüs, Optionen, Map-Auswahl, und und und.
Das ist zu diesem Zeitpunkt auch noch nicht wichtig und wird nach dem MVP angegangen.
Natürlich kann, wenn sich genug beteiligen, auch in der MVP-Phase bereits eine Implementierung von Menüs, Optionen etc. geschehen. 
Bei gewissen Implementierungen kommt man sich hier ja auch nicht in die Quere.

Wichtig ist, dass das Hauptfokus bei der Umsetzung des Game Loops ist, denn nur mit diesem kann das Spiel letzendlich auch gespielt werden. :)
