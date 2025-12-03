# Unity-MapScreenshotCapturer

A free and easy-to-use Unity tool to capture screenshots of multiple maps in your scene. Supports optional additional cameras, custom image names per map, particle simulation, and restores the previously active map. Ideal for map previews, level snapshots, or promotional images.

---

## Features

- Capture multiple maps with optional additional cameras.
- Custom image names per camera.
- Particle simulation included for accurate screenshots.
- Automatically restores active map and hidden objects.
- Saves screenshots in a specified folder inside `Assets`.
- Free and open-source.

---

## Installation

1. Clone or download the repository:

```bash
git clone https://github.com/Q1in/Unity-MapScreenshotCapturer.git
```

2. Copy `MapScreenshotCapturer.cs` into your Unity project `Scripts` folder.

3. Attach the script to an empty GameObject in your scene.

---

## Usage

1. **Assign Cameras**  
   Assign cameras that point directly at the map or area you want to capture, not just the default scene camera. The default camera field is used for each map unless overridden by additional cameras.

2. **Add Maps**  
   Add each map GameObject to the `Maps` list. Optionally assign additional cameras and custom image names. If names are left empty, screenshots will use automatic naming (`Map_1.png`, `Map_1_1.png`, etc.).

3. **Objects to Hide**  
   Add any objects to hide during capture, such as UI or props.

4. **Screenshot Settings**  
   Specify the `Folder Location` in `Assets` and the desired resolution.

5. **Capture Screenshots**  
   Right-click the MapScreenshotCapturer component in the Inspector and select **Capture Maps**. Screenshots will be saved to the chosen folder.

6. **Scene Restoration**  
   The script restores the map that was active before capture and unhides any hidden objects.

---

## License

MIT License — free to use, modify, and distribute.

---

## Screenshots

<img width="385" height="1031" alt="image" src="https://github.com/user-attachments/assets/8c730bdb-757b-4b3f-9565-25012fb6dfc3" />
<img width="802" height="148" alt="image" src="https://github.com/user-attachments/assets/aeb5ec97-1572-4a6d-a547-9871e6af3f5d" />


