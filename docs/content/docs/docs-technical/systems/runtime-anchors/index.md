---
title: Runtime Anchors
linkTitle: Runtime Anchors
description: Dokumentation über Runtime Anchors.
---

Oft benötgt man in Unity Referenzen auf andere GameObjects.
Gerade durch den Ansatz mit [Multi-Scene-Management](../multi-scene-management/) kommt es vor, dass ein GameObject nicht in der eigenen Scene, sondern in einer anderen liegt.
Leider wird zur Lösung hier oft ein Singleton-Pattern im Code implementiert, was die Wartbarkeit und Testbarkeit erschwert.

Anstelle von Singletons im Code verwenden wir Runtime Anchors.
Ein Runtime Anchor ist ein ScriptableObject, was eine Referenz auf ein bestimmtes GameObject kennt.
Dadurch entkoppeln wir verschiedene Systeme und es entsteht keine harte Abhängigkeit.
Als prominentes Beispiel wäre hier die MainCamera zu nennen, diese wird öfter mal in einem Spiel gebraucht, sei es zur Berechnung von Raycasts oder für Billboard-UI-Elemente.

Die Basis vom Runtime Anchor ist wie folgt implementiert:

```cs
public abstract class RuntimeAnchorBaseSO<T> : ScriptableObject
  where T : class
{
  public bool IsSet { get; private set; }
  private T? _item;

  public T? Item
  {
    get => _item;
    set
    {
      _item = value;
      IsSet = _item is not null;
    }
  }

  private void OnDisable()
  {
    _item = null;
    IsSet = false;
  }
}
```

Eine konkrete Implementierung für eine Camera wäre:

```cs
using UnityEngine;

[CreateAssetMenu]
public class CameraRuntimeAnchorSO : RuntimeAnchorBaseSO<Camera> { }
```

Jetzt werden zwei Dinge benötigt:

1. Ein Skript muss den Wert des `CameraRuntimeAnchorSO`s setzen.
2. Jemand muss diesen Wert konsumieren.

In beiden Fällen wird in einem `MonoBehaviour` oder auch in einem weiteren `ScriptableObject` ein Feld dafür angelegt:

```cs
[field: SerializeField]
private CameraRuntimeAnchorSO CameraRuntimeAnchor { get; set; } = default!;

// Anchor setzen:
private void Awake()
{
  CameraRuntimeAnchor.Item = GetComponent<Camera>();
}

private void OnDestroy()
{
  CameraRuntimeAnchor.Item = null;
}

// Anchor nutzen:
private void DoSomething()
{
  var camera = CameraRuntimeAnchor.Item;

  // camera.ScreenPointToRay...
}
```