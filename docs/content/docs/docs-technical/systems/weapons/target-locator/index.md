---
title: TargetLocator / Zielerfassung
linkTitle: TargetLocator / Zielerfassung
description: Dokumentation über TargetLocator / Zielerfassung.
---

Ein wichtiger Punkt bei jedem Tower Defense Spiel ist das Erfassen, welches Ziel als nächstes angegriffen wird.

Das übernimmt in unserem Spiel ein TargetLocator-ScriptableObject.
Ihn wird es in mehreren Ausführungen geben, aktuell ist nur ein TargetLocator für ballistische Waffen implementiert.

Im groben läuft das Herausfinden des nächsten Ziels so ab, sofern die Waffe noch kein Ziel hat:

1. Finde alle Gegner im Umkreis des Turms (der durch die [Waffe](../) selbst bestimmt wird.
2. Schaue, welche der Gegner, die gefunden wurden, auch im passenden Angriffswinkel der Waffe sind.
  Z.B. kann ein Katapult Gegner treffen, die in einem 90°-Winkel vor der Waffe sind, es kann also nicht seitlich oder nach hinten schießen.
  3. Falls Gegner im richtigen Angriffswinkel sind, suche je nach [TargetType](#TargetType) der Waffe einen Gegner aus.
4. Gegner wurde gefunden und wird solange verfolgt, bis er entweder zerstört wurde oder außerhalb der Angriffsreichweite ist.

## TargetType

Der TargetType bestimmt, welche Art von Gegner bevorzugt wird:

* `Closest`: Der Gegner, der am nahesten ist.
* `Random`: Irgendein Gegner.

Diese zwei sind bereits implementiert und können später noch erweitert werden.