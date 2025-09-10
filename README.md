# Blazor3D-Core

This project is a fork of https://github.com/SamuelReithmeir/Blazor3D, which in turn is a fork of https://github.com/HomagGroup/Blazor3D-Core.

## Changes from SamuelReithmeir's Fork

The changes that I made from [SamuelReithmeir](https://github.com/SamuelReithmeir)'s fork are a series of features which leverage the JavaScript side of the library.  These are all attached to the `Viewer` Razor component.

### Better Control of Mouse Selection

First, to allow direct control over which scene elements are selectable or not, I created an array (maintained on the JavaScript side) which can be filled with the scene elements that will participate in ray casting.

- If no scene elements are added to the selectable array, the selection mechanism will work as it previously does: all scene children will be recursively searched for a mouse selection.
- If there are elements in the selectable array, _only_ those elements will be searched for a mouse selection.
- Elements can be added to the selectable array by their UUID using the `AddSelectableByUuid(Guid uuid)` function on the `Viewer`. (The scene must have been updated before calling this, as it will try to retrieve existing scene objects without sending anything but the UUID across the JS interop boundary).

Second, key modifiers can be added to the library's left mouse click-to-select default behavior.  These are set as options in the `ViewerSettings` configuration object:

- `SelectCtrlKey` means that the control key must be held down during a left click in order to select a mesh
- `SelectAltKey` means that the alt key must be held down during a left click in order to select a mesh
- `SelectShiftKey` means that the shift key must be held down during a left click in order to select a mesh

Last, the opacity of the selected item can be altered in the same way that the color currently is. This is also changed in the `ViewerSettings` configuration object with its `SelectOpacity` property. The value may be set to between `0.0f` and `1.0f`. When the element loses selection, the original opacity will be restored.

### Fast Material Color/Opacity Changes

The `Blazor3D` doesn't have any way to update a scene based on the `C#` representation that doesn't involve serializing the entire scene and sending it through the JS interop mechanism.  For scenes that have a lot of geometry, this can take a long time, and doesn't lend itself well to property changes as a way of interacting with a user.

I added two mechanisms to change the visual properties of an object by its UUID.

- `SetColorByUuid(Guid uuid, string color)`
- `SetOpacityByUuid(Guid uuid, float opacity)`

These each wrap a JS interop call and will occur very quickly.  The scene update will be handled by `three.js` internally, so a `Viewer.UpdateScene()` will not be required.

### Point Light Attached to Camera

For whatever reason, I wasn't able to add a light to the camera through the normal `C#` API.  I added a mechanism to do so.

After the camera has been set up in the `Viewer` Razor component (either by you or through the defaults), you may call `AddPointLightOnCamera(string color, float intensity, float distance, float decay)` on the `Viewer`. This will create the point light, add it to the camera children's children, and then forcibly add the viewer's camera to the scene graph.


## SamuelReithmeir's Changes from Original HomagGroup/Blazor3D
* Added support for ShaderMaterial to allow for custom fragement/vertexShader
* Added support RawBufferGeometry to allow for setting of vertices/indices directly
* Added support for changing position/rotation/scale of objects based on uuid without rebuilding scene
* Added support for TextureMaterial for Meshes
* Added support for direct mouse events for objects: ObjectClicked,ObjectHovered,ObjectHoverEnd

## Build Instructions
### Build the Javascript bundle using webpack
* From a node.js command prompt, navigate to `Blazor3D-Core\src\javascript`
* Run `npx webpack --mode production`
* A `bundle.js` will be created inside `Blazor3D-Core\src\dotnet\Blazor3D\Blazor3D\wwwroot\js` which Blazor can reference

### Build the dotnet assembly 
* Build the csproj from VSCode or Visual Studio
* `dotnet pack` to package into a `nupkg`
