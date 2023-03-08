---
title: Continuous Integration & Continuous Delivery
linkTitle: Continuous Integration & Continuous Delivery
weight: 100
description: Dokumentation über Continuous Integration & Continuous Delivery
---

## Continuous Integration

Vereinfacht beschreibt CI automatisierte Systeme, die bei Änderungen im Repo loslaufen und diese Änderungen prüfen, ob sie gewisse Dinge einhalten.

Dieses Repo nutzt GitHub Actions zur Umsetzung von Continuous Integration (CI).
Alle Workflows finden sich unter [.github/workflows](https://github.com/BoundfoxStudios/community-project/tree/develop/.github/workflows).
Wir setzen hier stark auf [Game CI](https://game.ci), die eine Build-Umgebung für Unity bereitstellen.

In unserem Fall gibt es mehrere Prüfungen, die loslaufen, sobald ein Push auf `develop` erfolgt oder ein PR gestellt wird.

1. Code Linting: Prüfung, ob Code-Formatierung eingehalten wird.
2. Unit Tests: Starten der Unity Edit- & Play-Mode-Tests.
3. Export des Players: Es wird versucht, sowohl für Windows, Linux als auch für macOS das Spiel zu exportieren.

Sobald alle drei Prüfungen abgeschlossen sind, kann ein PR übernommen werden.
Schlägt auch nur eine der Prüfungen fehl, muss der Ersteller des PRs schauen, woran es liegt und diese Fehler beheben.

### Unity Lizenzierung

Ein _nerviges_ Problem bei lizenzierter Software ist oft das Aufsetzen von solchen automatischen Systemen, da diese in der Regel eine eigene Lizenz benötigen.
Daher funktioniert es in unserem Repo wie folgt:

1. PRs und Änderungen an `develop` werden mit einer Unity Personal License gebaut.
2. Änderungen an `main` werden mit einer Unity Professional License gebaut (sodass der typische "Made by Unity"-Splashscreen nicht auftaucht).

## Continuous Delivery

Vereinfacht beschreibt Continuous Delivery aus ständige Ausliefern von Änderungen an die Nutzer der Anwendung.

In unserem Fall ist das ein automatisches Ausliefern in einen Steam-Beta-Branch mit dem Namen `CI`.

Nach jedem Push auf `develop` erfolgt eine automatische Auslieferung. 
Wenn alles gut läuft bedeutet das, dass jede erfolgreiche Integration eines PRs innerhalb 1-2 Stunden spielbar auf einem Steam-Branch ist. 

### Technische Steam Infos

Unser Spiel hat auf Steam die App Id: `2350330`.

Folgende Depots sind konfiguriert:

| Depot ID | Beschreibung |
|----------|--------------|
| 2350331  | Windows      |
| 2350332  | macOS        |
| 2350333  | Linux        |

Dies ist bisher im Projekt noch nicht genutzt, soll aber implementiert werden.
