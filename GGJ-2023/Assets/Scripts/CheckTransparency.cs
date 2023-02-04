using System;
using System.Linq;
using UnityEngine;

public class CheckTransparency : MonoBehaviour
{
    public GameObject filler;

    public Vector2 fillerSize = Vector2.one * 0.5f;
    public Vector2 spacing = Vector2.one * 0.1f;

    public SpriteRenderer spriteRenderer;

    [ContextMenu(nameof(Test))]
    private void Test()
    {
        var sprite = spriteRenderer.sprite;
        var texture = sprite.texture;
        // get the world space dimensions
        var worldBounds = spriteRenderer.bounds;
        // get the pixel space dimensions
        var pixelRect = sprite.rect;

        // Multiply by this factor to convert world space size to pixels
        var fillerSizeFactor = Vector2.one / worldBounds.size * pixelRect.size;
        var fillerSizeInPixels = Vector2Int.RoundToInt(fillerSize * fillerSizeFactor);

        var start = worldBounds.min;
        var end = worldBounds.max;


        start /= 2;
        end /= 2;

        Debug.Log(start);
        Debug.Log(end);

        // Use proper for loops instead of ugly and error prone while ;)
        for (var worldY = start.y; worldY < end.y; worldY += fillerSize.y + spacing.y)
        {
            // convert the worldY to pixel coordinate
            var pixelY = Mathf.RoundToInt((worldY - worldBounds.center.y + worldBounds.size.y / 2f) * fillerSizeFactor.y);

            // quick safety check if this fits into the texture pixel space
            if (pixelY + fillerSizeInPixels.y >= texture.height)
            {
                continue;
            }

            for (var worldX = start.x; worldX < end.x; worldX += fillerSize.x + spacing.x)
            {
                // convert worldX to pixel coordinate
                var pixelX = Mathf.RoundToInt((worldX - worldBounds.center.x + worldBounds.size.x / 2f) * fillerSizeFactor.x);

                // again the check if this fits into the texture pixel space
                if (pixelX + fillerSizeInPixels.x >= texture.width)
                {
                    continue;
                }

                // Cut out a rectangle from the texture at given pixel coordinates
                var pixels = texture.GetPixels(pixelX, pixelY, fillerSizeInPixels.x, fillerSizeInPixels.y);

                // Using Linq to check if all pixels are transparent
                if (pixels.All(p => Mathf.Approximately(p.a, 0f)))
                {
                    continue;
                }

                // otherwise spawn a filler here
                Instantiate(filler, new Vector3(worldX, worldY, 0), Quaternion.identity, transform);
            }
        }
    }
}