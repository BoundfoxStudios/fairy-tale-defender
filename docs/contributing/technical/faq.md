# Häufig gestellte Fragen

## Wenn ich PR gestellt habe, bricht es ab beim "Lint code base"-Check.

Linting bedeutet, dass automatisiert eine Software (in diesem Fall dotnet-format) prüft, ob Du den Code so formatiert hast, wie wir es gerne in diesem Projekt hätten.
Wenn dies fehlschlägt, dann ist Dein Code noch nicht korrekt formatiert.

Um das Problem zu behben, musst Du in einer Kommandozeile, die im Ordner `CommunityProject` geöffnet wurde, folgendes ausführen:

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
