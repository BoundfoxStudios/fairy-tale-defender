---
title: Save Games
linkTitle: Save Games
description: Dokumentation über unsere Save Games.
---

## Speicherort

Der Speicherort wird abgeleitet von Unity's `Application.persistentDataPath`:

* Windows: `%userprofile%\AppData\LocalLow\Boundfox Studios\Fairy Tale Defender\Save Games`
* macOS: `~/Library/Application Support/Boundfox Studios/Fairy Tale Defender/Save Games`
* Linux: `$HOME/.config/unity3d/Boundfox Studios/Fairy Tale Defender/Save Games`

## Layout

Innerhalb des Ordners `Save Games` wird für jedes Save Game ein Ordner angelegt.
In diesem Ordner befinden sich zwei Dateien:

* `meta.json`: Meta-Informationen zum Spielstand, z.B. Name.
* `save.json`: Der Speicherpunkt.

## Hash-Generierung

Für jeden Speicherstand legen wir in der `meta.json` einen Hash ab.

{{% alert title="Was ist ein Hash?" color="success" %}}
Ein Hash ist ein Fingerabdruck für Daten. Egal wie groß oder klein die Eingabedaten sind, der Hash hat immer eine konstante Länge.
Eine identische Eingabe erzeugt immer den gleichen Hash.
Er kann genutzt werden um zu schauen, ob die Daten verändert wurden.
{{% /alert %}}

Mit dem Hash prüfen wir, ob jemand eine Änderung am Spielstand vorgenommen hat.
Das bietet natürlich keinen sicheren Schutz gegen Manipulationen, aber es stellt eine kleine Hürde dar.
