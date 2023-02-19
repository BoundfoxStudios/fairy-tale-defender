---
title: Event-System
linkTitle: Event-System
description: Dokumentation des Event-Systems.
---

Solltest Du nicht Wissen, was genau ein Event-Sytem ist, dann schau am besten einmal [hier](explanation/).

## Bestehende Events

Bestehende Events findest Du im Ordner `_Game/ScriptableObjects/Events`.
Zu jedem Event gibt es dort auch eine kleine Erklärung, für was genau es zur Verfügung steht.

## Event-Folgen

### Spielstart

```mermaid
flowchart TD
    LoadScene --> ToggleLoadingScreen1
    ToggleLoadingScreen1[ToggleLoadingScreen] --> LoadLevel{{Level wird geladen}}
    LoadLevel --> ToggleLoadingScreen2[ToggleLoadingScreen]
    ToggleLoadingScreen2 --> InitLevel{{Level wird initialisiert}}
    InitLevel --> SceneReady
    SceneReady --> GameplayStart
```