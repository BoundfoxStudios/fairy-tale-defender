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