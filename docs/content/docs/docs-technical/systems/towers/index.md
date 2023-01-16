---
title: Towers
linkTitle: Towers
description: Dokumentation über Türme.
---

Auf dieser Seite findet sich die Dokumentation, wie man einen Turm im Spiel erstellen und organisiert ablegen kann.

## Prefab

Im Ordner `_Game/Prefabs/Buildings/Towers` befinden sich alle vom Spieler baubaren Türme.
Jeder Turm bekommt hier seinen eigenen Ordner.

Als Beispiel zeigt das Bild den "SampleTower":

![SampleTower](assets/towers.png)

Im Bild unten zu sehen ist das Basis-Prefab `Tower_Base`, das für alle Türme genutzt werden muss.
Aus diesem Prefab wird eine Variante für den eigentlichen Tower erstellt.

Jeder Turm benötigt drei Prefabs:

1. `Turm_Gfx`: Beinhaltet die grafische Umsetzung des Turms ohne weitere Scripts.
2. `Turm_Blueprint`: Ist eine Variante von `Turm_Gfx`, bei dem die Materialen zum `Blueprint` geändert wurden.
  Diese Repräsentation wird genutzt, um dem Spieler ein "Geist"-Element anzuzeigen, wo der Turm gebaut werden kann.
  Im Bild sieht man links den Blueprint/Geist und rechts den gebauten Turm.

  ![Geist und echter Turm](assets/ghost.png)

3. `Turm`: Der eigentliche Turm. Er nutzt `Turm_Gfx` für die visuelle Darstellung (siehe Abschnitt [Turmaufbau](#Turmaufbau)) und hat alle benötigten Scripts, um seine Funktion zu erfüllen.

## ScriptableObject

Zu jedem baubaren Turm gehört ein ScriptableObject, das im Ordner `_Game/ScriptableObjects/Buildings/Towers` abgelegt wird.

![ScriptableObject eines Turms](assets/scriptable-object.png)

In diesem ScriptableObject stehen aktuell nur, welches Prefab gebaut werden kann und welches Prefab als Blueprint dient.

Später werden hier noch weitere Informationen stehen, z.B. welche Basis-Reichweite, -Feuerkraft, etc. ein Turm hat.

## Turmaufbau

![Turmaufbau](assets/tower-organization.png)

Jeder Turm besteht aus der `Tower_Base`.
In dessen GameObject `GFX` wird das Prefab `Turm_Gfx` abgelegt.
Alle weiteren Scripts und was sonst so benötigt wird, um den Turm zum Leben zu erwecken, werden direkt auf Root-Objekt platziert.