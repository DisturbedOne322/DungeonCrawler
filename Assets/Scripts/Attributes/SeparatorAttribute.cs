using UnityEngine;

public class SeparatorAttribute : PropertyAttribute
{
    public readonly float Thickness;
    public readonly float Padding;
    public readonly Color Color;

    /// <param name="thickness">Line thickness in pixels.</param>
    /// <param name="padding">Space above and below the line.</param>
    /// <param name="r">Red (0–1)</param>
    /// <param name="g">Green (0–1)</param>
    /// <param name="b">Blue (0–1)</param>
    public SeparatorAttribute(float thickness = 1.35f, float padding = 4f, float r = 0.75f, float g = 0.75f, float b = 0.75f)
    {
        Thickness = thickness;
        Padding = padding;
        Color = new Color(r, g, b);
    }
}