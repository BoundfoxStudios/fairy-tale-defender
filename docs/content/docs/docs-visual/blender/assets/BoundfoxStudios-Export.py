# https://docs.fairytaledefender.de/docs/docs-visual/blender/#export-script
import bpy
import os
import io_scene_fbx.export_fbx_bin
import mathutils

# export to blend file location
basedir = os.path.dirname(bpy.data.filepath)

if not basedir:
    raise Exception("Blend file is not saved")

view_layer = bpy.context.view_layer

obj_active = view_layer.objects.active
selection = bpy.context.selected_objects
selection_count = len(selection)

# Load the original unity settings for export
kwargs = io_scene_fbx.export_fbx_bin.defaults_unity3d()
kwargs["object_types"] = {'MESH', 'ARMATURE', 'EMPTY'};

class FakeOp:
    def report(self, tp, msg):
        print("%s: %s" % (tp, msg))

# use active collection if no object is selected
if selection_count == 0:
    name = bpy.path.display_name_from_filepath(bpy.context.blend_data.filepath)
    print(name)
    fn = os.path.join(basedir, name)
    
    kwargs["use_active_collection"] = True
    
    io_scene_fbx.export_fbx_bin.save(FakeOp(), bpy.context, filepath=fn + ".fbx", **kwargs)
else:
    kwargs["use_selection"] = True
    
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
        oldRotation = obj.rotation_euler.copy()
        obj.location = (0, 0, 0)
        obj.rotation_euler = mathutils.Euler((0, 0, 0), 'XYZ')

        # some exporters only use the active object
        view_layer.objects.active = obj

        name = bpy.path.clean_name(obj.name)
        fn = os.path.join(basedir, name)
        
        io_scene_fbx.export_fbx_bin.save(FakeOp(), bpy.context, filepath=fn + ".fbx", **kwargs)

        # Restore the old location    
        obj.location = oldLocation
        obj.rotation_euler = oldRotation

        obj.select_set(False)

        print("written:", fn)


    view_layer.objects.active = obj_active

    for obj in selection:
        obj.select_set(True)