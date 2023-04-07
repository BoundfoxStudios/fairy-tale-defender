---
title: Tooltips
linkTitle: Tooltips
description: Dokumentation über Tooltips.
---

Im Community-Projekt können wir sowohl für UI-Elemente als auch für GameObjects Tooltips anzeigen.
Tooltips sind Hinweistexte/Bilder, die erscheinen, sobald man mit der Maus über bestimmte Elemente fährt. 

Um einen Tooltip zu nutzen, kann eine der Klassen von [Tooltip-Typen](#tooltip-typen) auf einem GameObject platziert werden.
Sobald man das Spiel startet und über das Element fährt, wird der entsprechende Tooltip angezeigt.

Wichtig ist, dass alle GameObjects, die ein Tooltip anzeigen sollen, einen Collider benötigen!
Im Moment reagieren die Tooltips auch nur bei Objekten, die auf dem Layer "Tower" sind. 
Falls weitere benötigt werden, können wir das anpassen.

Bei UI-Elementen funktioniert es direkt, sofern der Canvas einen GraphicsRaycaster hat (per Standard so) und das UI-Element irgendwas hat, was raycast-bar ist (z.B. ein Image).

Aktuell sind folgende Tooltips implementiert.
Weitere können bei Bedarf hinzugefügt werden.

## Tooltip-Typen

### TextTooltip

Nutzt einen lokalisierbaren String.
