---
title: Technisch mitmachen
linkTitle: Technisch (Code)
description: Info über das Mitmachen im technischen Bereich.
---

## Inhaltsverzeichnis

Du bist hier richtig, wenn Du Dich für die Entwicklung interessierst, d.h. Du

* entwickelst Code für das Spiel,
* entwickelst Erweiterungen für den Unity-Editor,
* möchtest bestehende Bugs im Spiel beseitigen und/oder
* möchtest mit Shadern bzw. dem Shader-Graph visuellen Pepp hinzufügen (siehe auch [hier](../visual/))

## Worauf muss ich achten?

* Lies Dir die [Technische Dokumentation](../../docs-technical/) durch.
* Du musst die korrekte Unity-Version installieren, das ist aktuell **Unity 2021.3.9f1**. Schaue gerne [hier](https://github.com/boundfoxstudios/community-project/tree/develop/CommunityProject/ProjectSettings/ProjectVersion.txt) nach, welche Version im Projekt eingesetzt wird, falls vergessen wurde, dieses Dokument zu aktualisieren. 
  * Um Unity in dieser Version zu installieren, klicke [hier](unityhub://2021.3.9f1/ad3870b89536), wenn Du den Unity Hub installiert hast. 
    Alternativ, schaue gerne [hier auf der Unity-Seite](https://unity3d.com/get-unity/download/archive) nach der Version.
* Unity tendiert gerne dazu, dass es Dinge mitändert, die man nicht oder vermeintlich nicht angefasst hat.
  Konzentriere Dich daher generell nur auf Deine Aufgabe und prüfe in Git, ob Du auch nur die Sachen committest, die Du auch wirklich geändert hast.

## Was soll ich nicht tun?

Bitte mache folgende Dinge **nicht** oder nur nach Rücksprache mit einem [Ansprechpartner](https://github.com/boundfoxstudios/community-project/#ansprechpartner):

* Aktualisierung der Unity-Version.
* Neues Package dem Projekt hinzufügen.
* Bestehendes Package aktualisieren.
* Lösche keine Assets oder Szenen, die nicht unmittelbar mit Deiner Aufgabe zu tun haben. Refactoring, Rename _kann_ in Ordnung sein.
* Füge keine Test-Szenen oder Test-Scripte hinzu. Du kannst diese gerne für Dich erstellen, aber committe und pushe sie nicht.
* Nutze nicht den alten Unity-Text. Wir setzten ausschließlich auf TextMeshPro.
* Nutze nicht das alte Unity Input. Wir nutzen ausschließlich das neue Unity [Input System](https://www.youtube.com/playlist?list=PLxVAs8AY4TgdZTkklVi739QeL-YTYU8in).

## Wo lade ich meine Ergebnisse hin?

Im Gegensatz zur kreativen Mitarbeit, arbeitest Du als als Techniker direkt am Unity-Projekt und machst dort Deine Änderungen.

### Was passiert dann?

[Siehe hier](../#ich-habe-eine-aufgabe-fertig-was-mache-ich-damit).

### Hilfe, ich komme mit Git gar nicht klar

Als technischer Mitwirkender musst Du Dir Git soweit aneignen, dass Du es für das Projekt nutzen kannst. :)

Frag gerne auf dem [Discord]({{< param "project_links.discord.url" >}}) nach, wenn Du so gar nicht weiter kommst. 
