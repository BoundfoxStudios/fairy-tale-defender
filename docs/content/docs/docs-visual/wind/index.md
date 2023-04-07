---
title: Wind-Effekt
linkTitle: Wind-Effekt
weight: 100
description: Beschreibung wie Natur-Elemente (z.B. Baum, Gras) einen Wind-Effekt erhalten können.
---

Wir haben in Unity einen Fragment-Shader, der einen Wind-Effekt bei Natur-Elementen (z.B. Bäume, Gräser, Blumen) simulieren kann.
Die nötige Information, welche Teile des Modells wie stark im Wind bewegt werden, speichern wir auf den Modellen im 2. UV-Kanal.

Diesen 2. UV-Kanal kannst Du in Blender anlegen, in dem Du das Mesh auswählst, dann bei den Properties auf den "Object Data Properties"-Tab wechselst und dort beim Punkt "UV Maps" eine zweite Map anlegst.
Wie Du diese nennst, spielt keine Rolle, wir empfehlen aber den Namen "Wind", damit man weiß, für was diese UV Map existiert.

Jetzt ist wichtig zu wissen, wie die Vertices in dieser UV angeordnet werden müssen.
Vorab: Die X-Achse spielt keine Rolle, für den Wind-Effekt ist ausschließlich die Y-Achse relevant.

Die UV Map geht 0/0 (links unten) nach 1/1 (rechts oben).
Die Y-Achse bestimmt nun, wie stark der Wind Einfluss auf den Vertex hat.
Ist dieser bei 0, wird er nicht vom Wind beeinflusst und ist dieser bei 1, dann wird er maximal vom Wind beeinflusst.

Wenn man z.B. einen Baum nimmt, der aus einem Stamm und einer Krone besteht, dann könnte man alle Vertices vom Stamm auf 0 setzen, sodass dieser nicht im Wind mitbewegt wird und die Vertices der Krone auf 1, sodass diese maximal vom Wind erfasst wird. 

Alle Stuffen zwischen 0 und 1 bestimmen prozentual wie stark die Vertices vom Wind bewegt werden.
So könnte man z.B. den Teil vom Stamm, der in die Krone reicht, bei 0.5 platzieren, sodass der obere Teil vom Stamm auch etwas vom Wind bewegt wird.

Somit kann man gut bestimmen, welche Teile vom Modell wie stark vom Wind erfasst werden, ganz so, wie es in der echten Natur auch wäre. 