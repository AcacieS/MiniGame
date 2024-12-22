using UnityEngine;

public class DynamicTextTexture : MonoBehaviour
{
    public Material wallMaterial; // Assign your wall material in the Inspector
    public int textureWidth = 512; // Width of the texture
    public int textureHeight = 256; // Height of the texture

    private Texture2D textTexture;
    private float startTime = 90f; // Time in seconds
    private float timeRemaining;

    void Start()
    {
        // Initialize timer
        timeRemaining = startTime;

        // Create a blank texture
        textTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

        // Clear the texture with a default color (e.g., black)
        ClearTexture(Color.black);

        // Assign the texture to the wall material
        wallMaterial.mainTexture = textTexture;
    }

    void Update()
    {
        // Decrease time
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
        }

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Format the time as "MM:SS"
        string timeText = $"{minutes:00}:{seconds:00}";

        // Update the texture with the new time
        UpdateTextureWithText(timeText, Color.white, Color.black);
    }

    void ClearTexture(Color backgroundColor)
    {
        // Fill the texture with the background color
        Color[] pixels = new Color[textureWidth * textureHeight];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = backgroundColor;
        }
        textTexture.SetPixels(pixels);
        textTexture.Apply();
    }

    void UpdateTextureWithText(string text, Color textColor, Color backgroundColor)
    {
        // Clear the texture
        ClearTexture(backgroundColor);

        // Create a temporary RenderTexture
        RenderTexture tempRT = RenderTexture.GetTemporary(textureWidth, textureHeight);
        RenderTexture.active = tempRT;

        // Set up the GUI rendering environment
        GL.Clear(true, true, backgroundColor);
        GUI.color = textColor;

        // Draw the text onto the RenderTexture
        GUIStyle style = new GUIStyle
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 50,
            normal = new GUIStyleState { textColor = textColor }
        };

        GUI.Label(new Rect(0, 0, textureWidth, textureHeight), text, style);

        // Copy the RenderTexture to the Texture2D
        textTexture.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
        textTexture.Apply();

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tempRT);
        RenderTexture.active = null;
    }
}