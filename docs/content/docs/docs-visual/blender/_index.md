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

* `Props.blend`: Referenz-Datei mit Assets aus dem Spiel als Referenz für Größen, Formen.
* `Tiles.blend`: Referenz-Datei mit den Tiles aus dem Spiel für Größen.
* `Blender-Starter.blend`: Hiermit startest Du ein neues Asset für das Spiel.
* `textures/ColorPalette_Summer.png`: Die sommerliche [Farbpalette](../colors/).
* `textures/Windscale.png`: Die Skala für [Wind-Effekte](../wind/).
* `scripts/Boxfound-Export.py`: Referenz-Datei mit dem Skript zum Exportieren der Modelle.

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

```python
import bpy
import os

# export to blend file location
basedir = os.path.dirname(bpy.data.filepath)

if not basedir:
    raise Exception("Blend file is not saved")

view_layer = bpy.context.view_layer

obj_active = view_layer.objects.active
selection = bpy.context.selected_objects
selection_count = len(selection)

# use active collection if no object is selected
if selection_count == 0:
    name = bpy.path.display_name_from_filepath(bpy.context.blend_data.filepath)
    print(name)
    fn = os.path.join(basedir, name)

    bpy.ops.export_scene.fbx(
        filepath=fn + ".fbx", 
        use_active_collection=True, 
        object_types= {'MESH', 'ARMATURE', 'EMPTY'}, 
        use_mesh_modifiers=True,
        mesh_smooth_type='OFF',
        use_custom_props=True,
        bake_anim_use_nla_strips=False,
        bake_anim_use_all_actions=False,
        use_space_transform=True,
        bake_space_transform=True,
        axis_forward='-Z',
        axis_up='Y',
        apply_scale_options='FBX_SCALE_ALL')
else:
    bpy.ops.object.select_all(action='DESELECT')

    for obj in selection:

        bpy.context.view_layer.objects.active = obj
        obj.select_set(True)
        bpy.ops.object.select_grouped(
            extend=True,
            type='CHILDREN_RECURSIVE'
        )
        
        # Save the initial location and set the object to 0/0/0
        oldLocation = obj.location.copy()
        obj.location = (0, 0, 0)

        # some exporters only use the active object
        view_layer.objects.active = obj

        name = bpy.path.clean_name(obj.name)
        fn = os.path.join(basedir, name)

        bpy.ops.export_scene.fbx(
            filepath=fn + ".fbx", 
            use_selection=True, 
            object_types= {'MESH', 'ARMATURE', 'EMPTY'}, 
            use_mesh_modifiers=True,
            mesh_smooth_type='OFF',
            use_custom_props=True,
            bake_anim_use_nla_strips=False,
            bake_anim_use_all_actions=False,
            use_space_transform=True,
            bake_space_transform=True,
            axis_forward='-Z',
            axis_up='Y',
            apply_scale_options='FBX_SCALE_ALL')

        # Restore the old location    
        obj.location = oldLocation

        obj.select_set(False)

        print("written:", fn)


    view_layer.objects.active = obj_active

    for obj in selection:
        obj.select_set(True)
```
