# https://docs.fairytaledefender.de/docs/docs-visual/blender/#export-script
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
