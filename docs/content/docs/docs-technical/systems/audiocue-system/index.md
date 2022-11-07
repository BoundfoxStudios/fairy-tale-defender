---
title: Audio Cue (Sound Effekte)
linkTitle: Audie Cue (Sound Effekte)
description: Dokumentation des Audio Cue Systems.
---

Das AudioCue-System dient dazu, einzelne Soundeffekte abzuspielen.
Man möchte vielleicht fragen, warum nicht einfach das GameObjekt, welches den Sound benötigt eine AudioSource hat,
welche dann den Sound abspielt. Dies ginge theoretisch, allerdings gibt es bei z.B. dem Tod einer Einheit das
Problem, dass das GameObjekt zerstört werden soll und hierbei würde das Abspielen abgebrochen. Desswegen
wird dies von einem System, welches diese Aufgabe zentraliesiert, übernommen.

## Verwendenung
### AudioCue
Zuerst muss eine Instanz des AudioCueSO erstellt werden. An dieser Stelle muss der Audio Clip spezifiziert werden.

### Abspielendes GameObject
Das abspielende GameObject muss eine Referenz, nennen wir sie `eventChannel` zu dem AudioCueEventChannelSO, abegelegt unter `ScriptableObjects/Events`,
haben. Außerdem muss das GameObject die Instanz, beispielsweise `PlayerDeathSound` des AudioCueSO mit dem entsprechenden Clip kennen. Anschließend
muss das Event mit `eventChannel.Invoke(PlayerDeathSound);` geworfen werden. Nun ist die Arbeit auf Seiten des Abspielenden GameObject erledigt
und es kann sich selbst zerstören/zerstört werden, ohne dass Probleme auftreten.
