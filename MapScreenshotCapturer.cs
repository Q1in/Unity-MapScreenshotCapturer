/*
 * MapScreenshotCapturer
 * 
 * Captures screenshots of multiple maps in your Unity scene.
 * Free to use and modify for any project.
 * 
 * Tips: Assign the capture camera, maps, and optional objects to hide during capture.
 * Each map can have its own additional cameras and optional custom image names.
 * Screenshots will be saved in the specified folder inside Assets.
 */

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class MapElement
{
    [Tooltip("The map GameObject to capture.")]
    public GameObject map;

    [Tooltip("Optional additional cameras to capture this map from different angles.")]
    public List<CameraWithName> additionalCameras = new List<CameraWithName>();

    [Tooltip("Custom image name for the default capture camera. Leave empty for default naming (Map_1, Map_2...).")]
    public string customName;
}

[System.Serializable]
public class CameraWithName
{
    [Tooltip("Camera to use for this capture.")]
    public Camera camera;

    [Tooltip("Custom image name for this camera. Leave empty for default naming.")]
    public string customImageName;
}

public class MapScreenshotCapturer : MonoBehaviour
{
    [Header("Default Capture Camera")]
    [Tooltip("Default camera to use if no additional cameras are assigned.")]
    public Camera captureCamera;

    [Header("Maps to Capture")]
    [Tooltip("List of maps. Each map can have additional cameras and custom image names.")]
    public List<MapElement> maps = new List<MapElement>();

    [Header("Objects to Hide During Capture")]
    [Tooltip("Objects that should be hidden when taking screenshots.")]
    public List<GameObject> objectsToHide = new List<GameObject>();

    [Header("Screenshot Settings")]
    [Tooltip("Folder inside Assets where screenshots will be saved.")]
    public string folderLocation = "CapturedMaps";
    [Tooltip("Width of the captured screenshots in pixels.")]
    public int resolutionWidth = 1920;
    [Tooltip("Height of the captured screenshots in pixels.")]
    public int resolutionHeight = 1080;

    [ContextMenu("Capture Maps")]
    public void CaptureMaps()
    {
        if (captureCamera == null)
        {
            Debug.LogError("No default capture camera assigned!");
            return;
        }

        if (maps.Count == 0)
        {
            Debug.LogWarning("No maps assigned!");
            return;
        }

        string folderPath = Path.Combine(Application.dataPath, folderLocation);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        SetActiveObjects(objectsToHide, false);

        // Remember which map is currently active
        GameObject activeMapBeforeCapture = null;
        foreach (var element in maps)
        {
            if (element.map != null && element.map.activeInHierarchy)
            {
                activeMapBeforeCapture = element.map;
                break;
            }
        }

        int mapCounter = 1;

        foreach (var element in maps)
        {
            if (element.map == null) continue;

            element.map.SetActive(true);
            SimulateParticles(element.map);

            // Capture with default camera
            string defaultFileName = string.IsNullOrEmpty(element.customName)
                ? $"Map_{mapCounter}.png"
                : $"{element.customName}.png";

            string defaultPath = Path.Combine(folderPath, defaultFileName);
            TakeScreenshot(captureCamera, defaultPath);

            // Capture with additional cameras
            int addCamIndex = 1;
            foreach (var camElement in element.additionalCameras)
            {
                if (camElement.camera == null) continue;

                string camFileName = string.IsNullOrEmpty(camElement.customImageName)
                    ? $"Map_{mapCounter}_{addCamIndex}.png"
                    : $"{camElement.customImageName}.png";

                string camPath = Path.Combine(folderPath, camFileName);
                TakeScreenshot(camElement.camera, camPath);
                addCamIndex++;
            }

            StopParticles(element.map);
            element.map.SetActive(false);

            mapCounter++;
        }

        // Restore scene: previously active map
        if (activeMapBeforeCapture != null)
            activeMapBeforeCapture.SetActive(true);

        SetActiveObjects(objectsToHide, true);
        Debug.Log("All maps captured with additional cameras. Previously active map restored, hidden objects restored.");
    }

    private void SetActiveObjects(List<GameObject> objects, bool active)
    {
        foreach (var obj in objects)
            if (obj != null) obj.SetActive(active);
    }

    private void SimulateParticles(GameObject map)
    {
        ParticleSystem[] particles = map.GetComponentsInChildren<ParticleSystem>(true);
        foreach (var ps in particles)
        {
            ps.Play(true);
            ps.Simulate(Time.deltaTime, true, true);
        }
    }

    private void StopParticles(GameObject map)
    {
        ParticleSystem[] particles = map.GetComponentsInChildren<ParticleSystem>(true);
        foreach (var ps in particles)
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void TakeScreenshot(Camera cam, string path)
    {
        DynamicGI.UpdateEnvironment();

#pragma warning disable CS0618
        foreach (var probe in FindObjectsOfType<ReflectionProbe>())
            probe.RenderProbe();
#pragma warning restore CS0618

        RenderTexture rt = new RenderTexture(resolutionWidth, resolutionHeight, 24, RenderTextureFormat.DefaultHDR);
        cam.targetTexture = rt;
        cam.Render();

        RenderTexture.active = rt;
        Texture2D screenshot = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        screenshot.Apply();

        ApplyGamma(screenshot, 2.2f);

        cam.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(rt);

        File.WriteAllBytes(path, screenshot.EncodeToPNG());
        DestroyImmediate(screenshot);

#if UNITY_EDITOR
        AssetDatabase.Refresh();

        string assetPath = "Assets" + path.Substring(Application.dataPath.Length).Replace("\\", "/");
        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (importer != null)
        {
            importer.maxTextureSize = 1024;
            importer.textureCompression = TextureImporterCompression.Compressed;
            importer.crunchedCompression = true;
            importer.compressionQuality = 30;
            importer.SaveAndReimport();
        }
#endif
    }

    private void ApplyGamma(Texture2D tex, float gamma)
    {
        Color[] pixels = tex.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i].r = Mathf.Pow(pixels[i].r, 1f / gamma);
            pixels[i].g = Mathf.Pow(pixels[i].g, 1f / gamma);
            pixels[i].b = Mathf.Pow(pixels[i].b, 1f / gamma);
        }
        tex.SetPixels(pixels);
        tex.Apply();
    }
}
