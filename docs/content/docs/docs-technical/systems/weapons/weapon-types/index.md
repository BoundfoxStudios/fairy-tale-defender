---
title: Waffenarten
linkTitle: Waffenarten
description: Dokumentation über Waffenarten.
---

Die Idee der Gruppierung in Waffenarten ist, dass man ähnliche Waffen nicht mehrfach implementieren muss.
Ein Beispiel wäre ein Katapult und ein Trebuchet.
Beides sind ballistische Waffen, die in ihrer Grundfunktion gleich sind: Sie schleudern ein Projektil auf den Gegner.
Der Unterschied ist am Ende nur etwas in der Animation, (Mindest-)Reichweite und Stärke.
Ansonsten sind diese Waffen gleich.
Es wäre daher nicht gut, wenn wir beide Waffen jedes mal von Grund auf implementieren müssen.

Im Ideallfall nutzen beide das gleiche Script für eine ballistische Waffe und unterscheiden sich nur in den Parametern und natürlich im visuellen Modell.

## Ballistische Waffen

Zu ballistischen Waffen gehören z.B. Katapulte, Trebuchets, etc. - alles, was ein Projektil durch die Luft schleudert.
Charakterstisch für diese Waffen ist oft, dass sie ungenau sind, aber dafür Flächenschaden anrichten.
Auch sind die oft recht langsam und das Projektil benötigt einige Zeit, um das Ziel zu treffen.
Für ballistische Waffen exitiert ein ScriptableObject vom Typ `BallisticWeaponSO`.

Beispiel vom Katapult:

![Katapult](../assets/weapon-so.png)

### Projektile

Ballistische Waffen verschießen Projektile, die aktuell so implementiert sind, dass sie physikalisch korrekt fliegen.
Ein Projektil hat einen dynamischen Rigidbody und wird durch die Gravitation nach unten gezogen.
Beim Start ein Projektiles wird einmalig die Abschuss-Velocity bestimmt anhand dessen das Projektil zu seinem Zielpunkt fliegt.