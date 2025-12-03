# Unity-MapScreenshotCapturer

A free and easy-to-use Unity tool to capture screenshots of multiple maps in your scene.
Supports optional additional cameras, custom image names per map, particle simulation, and automatically restores the previously active map. Ideal for creating high-quality map previews, level snapshots, or promotional images.

---

## Features

* Capture multiple maps in your Unity scene.
* Assign optional additional cameras per map for multiple angles.
* Custom image names for default and additional cameras.
* Particle system simulation ensures effects are visible in screenshots.
* Automatically restores the previously active map.
* Saves screenshots in a specified folder inside `Assets`.
* Fully free and open-source for any project.

---

## Installation

1. Download or clone this repository:

```bash
git clone https://github.com/Q1in/Unity-MapScreenshotCapturer.git
```

2. Copy the `MapScreenshotCapturer.cs` script into your Unity project `Scripts` folder (or any folder you prefer).

3. Open your Unity project and attach the script to an empty GameObject in your scene.

---

## Usage

1. **Assign the Default Camera**
   Drag a Camera into the `Capture Camera` field in the Inspector. This camera will be used to capture each map by default.

2. **Add Maps**

   * In the `Maps` list, add each map GameObject you want to capture.
   * For each map:

     * You can assign optional additional cameras in the `Additional Cameras` list.
     * Optionally set custom image names for both the default and additional cameras.
       *(If left empty, the script will use automatic naming: `Map_1.png`, `Map_1_1.png`, `Map_1_2.png`, etc.)*

3. **Objects to Hide**
   Add any objects that should be hidden while capturing screenshots (e.g., UI elements, props, or decorative objects).

4. **Screenshot Settings**

   * `Folder Location`: specify the folder inside `Assets` where screenshots will be saved.
   * `Resolution Width` & `Resolution Height`: set your desired screenshot dimensions.

5. **Capture Screenshots**

   * In the Inspector, right-click on the MapScreenshotCapturer component (gear icon on the top-right of the component).
   * Select **Capture Maps** from the context menu.
   * The script will capture screenshots for all maps, including additional cameras, and save them to the specified folder.

6. **Restoring Scene**

   * The script automatically restores the map that was active before capturing.
   * All hidden objects will also be restored.

---

## Example Naming

* Default camera: `Map_1.png`, `Map_2.png`, …
* Additional cameras: `Map_1_1.png`, `Map_1_2.png`, …
* Custom names: e.g., `LevelA_Main.png`, `LevelA_Side.png`, …

---

## Notes

* Particle effects are simulated for one frame so that they appear correctly in screenshots.
* Works in both Editor and Play mode.
* Fully free to use and modify for personal or commercial projects.
* Make sure the cameras have the correct settings (field of view, position, etc.) for the desired screenshot result.

---

## License

This project is **MIT Licensed** — you can use, modify, and distribute it freely.

---

## Screenshots

<img width="385" height="1031" alt="image" src="https://github.com/user-attachments/assets/8c730bdb-757b-4b3f-9565-25012fb6dfc3" />
<img width="802" height="148" alt="image" src="https://github.com/user-attachments/assets/aeb5ec97-1572-4a6d-a547-9871e6af3f5d" />


