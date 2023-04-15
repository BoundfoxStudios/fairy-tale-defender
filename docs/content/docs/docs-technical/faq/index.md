---
title: Häufig gestellte Fragen
linkTitle: Häufig gestellte Fragen
weight: 10000
description: Häufig gestellte Fragen zum Projekt.
---

## Wenn ich PR gestellt habe, bricht es ab beim "Lint code base"-Check.

Linting bedeutet, dass automatisiert eine Software (in diesem Fall dotnet-format) prüft, ob Du den Code so formatiert hast, wie wir es gerne in diesem Projekt hätten.
Wenn dies fehlschlägt, dann ist Dein Code noch nicht korrekt formatiert.

Um das Problem zu behben, musst Du in einer Kommandozeile, die im Ordner `FairyTaleDefender` geöffnet wurde, folgendes ausführen:

```bash
# Windows:
format.bat

# Linux/mac:S
./format.sh
```

Wenn der Befehl fertig ist, prüfst Du in GitHub Desktop, ob es geändert Dateien gibt, das sollte jetzt auch der Fall sein.
Die geänderten Dateien (und prüfe bitte, ob es wirklich nur Deine Dateien sind) kannst Du nun committen und pushen.

Durch den Push wird Dein PR erneut geprüft und sollte zumindest bei "Lint code base" kein Problem mehr machen.

## Ich erhalte einen Fehler beim Ausführen von format.bat/format.sh.

Es scheint, als hättest Du nicht .NET 6 installiert, dort kommt dotnet-format, was von format.bat/format.sh ausgeführt wird, mit. 
Bitte installiere das [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)

## Wie kann ich Dinge entwickeln, für die ich Steam benötige?

Um permanente Fehlermeldungen zu vermeiden, ist im Unity-Projekt die Steam-Integration per Standard ausgeschaltet.
Erst durch den Build durch GitHub wird die Steam-Integration aktiviert.

Wenn Du eine Aufgabe übernimmst, bei der Du etwas implementieren möchtest, dass mit Steam zu tun hat, kannst Du die Steam-Integration lokal aktivieren.
Dazu findest Du rechts oben im Editor einen Steam-Button, der per Standard rot (= aus) ist.
Wenn Du diesen Button klickst, wird er grün und damit aktiviert sich die Steam-Integration.
Klicke ihn erneut an, um die Integration wieder auszuschalten.

![Steam-Integration](assets/steam-integration.png)

{{% alert title="Achtung" color="success" %}}
Bevor Du einen PR stellst, musst Du die Steam-Integration wieder ausschalten, ansonsten werden die automatischen Prüfungen Deines PR fehlschlagen.
{{% /alert %}}