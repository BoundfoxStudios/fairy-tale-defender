---
title: Blender
linkTitle: Blender
weight: 100
description: Informationen zur Modellierung in Blender.
---

Auf dieser Seite findest Du alles, was Du zum Modellieren in Blender wissen musst.

## Blender Starter-Kit

Hier findest Du das Blender Starter-Kit.

{{% alert title="Download" %}}
[Klicke hier, um das Blender Starter-Kit herunterzuladen](assets/BlenderStarterKit.zip).

Es beinhaltet:

* `Blender-Starter.blend`: Hiermit startest Du ein neues Asset für das Spiel.
* `House.blend`: Referenz-Datei mit den Häuser aus dem Spiel für Größen.
* `Tiles.blend`: Referenz-Datei mit den Tiles aus dem Spiel für Größen.
* `textures/ColorPalette_Summer.png`: Die sommerliche [Farbpalette](../colors/).
* `textures/Windscale.png`: Die Skala für [Wind-Effekte](../wind/).
* `scripts/BoundfoxStudios-Export.py`: Referenz-Datei mit dem Skript zum Exportieren der Modelle.

Bitte beachte, dass die Texture und das passende Material bereits in der `Blender-Starter.blend` eingebunden sind. 
Du musst diese nur noch benutzen.
{{% /alert %}}

## Blender-Einstellungen

Am besten ist es, wenn Du die `Blender-Starter.blend` als Grundlage nutzt, um eigene Assets zu erstellen.
Dort ist bereits die Farbpaletten-Textur und das [Export-Script](#export-script) hinterlegt.

Achte bitte darauf, dass Du das bestehende Material `ColorPalette` nutzt.
Bitte erstelle keine weiteren Materialien und nenne das bestehende Material auch nicht um. 
Das Material muss `ColorPalette` heißen, da wir beim Import in Unity nach diesem Suchen und mit dem Material der Engine ersetzen.

## Export-Einstellungen

Wenn Du mit Deinem Modell fertig bist und es exportieren willst, dann beachte bitte folgende Dinge:

* Exportiere pro Modell eine eigene FBX-Datei.
* Achte darauf, dass das Modell im Ursprung liegt, bevor Du es exportierst.
* Stelle die folgenden Einstellungen im Export-Dialog ein:

![Blender Einstellungen](assets/blender-settings.png)

Wenn Du mehrere Modelle in einer Blender-Datei hast, dann ist folgendes Script nützlich für den Export:

### Export-Script

> Das Export-Script ist in der `Blender-Starter.blend` im Blender Starter-Kit bereits enthalten.

Wechsle in das Scripting-Tab von Blender und erzeuge ein neues Script.
Kopiere dann folgendes Script in den leeren Text-Editor.

Mit dem Skript hast Du zwei Möglichkeiten, einen Export anzustoßen.

1. Export der aktiven Collection: Wähle hierzu einfach eine Scene-Collection an und starte das Skript. Es wird eine FBX-Datei erzeugt mit allem in der Scene-Collection.
   Achte hier bitte darauf, dass das Objekt im Nullpunkt steht!
2. Export der selektierten Objekte: Wähle ein oder mehrere Objekte an und starte das Skript. Es wird pro Objekt eine eigene FBX-Datei erzeugt. Außerdem wird jedes Objekt automatisch beim Export in den Ursprung geschoben.

In beiden Fällen werden die Dateien dort abgelegt, wo auch die Blender-Datei liegt.

Bitte beachte, dass Du im Object-Mode sein musst zum Exportieren.

{{< readfile file="assets/BoundfoxStudios-Export.py" code="true" lang="python" >}}
