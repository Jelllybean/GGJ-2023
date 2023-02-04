using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckTransparency : MonoBehaviour
{

    public Transform buriedItem;
    public BuriedItem currentDugItem;
    public GameObject filler;

    public Vector2 fillerSize = Vector2.one * 0.5f;
    public Vector2 spacing = Vector2.one * 0.1f;

    public SpriteRenderer spriteRenderer;

    private Vector2 start;
    private Vector2 end;

    public void StartMiniGame()
    {
        start = new Vector2(buriedItem.transform.position.x - 1, buriedItem.transform.position.y - 1);
        end = new Vector2(buriedItem.transform.position.x + 1, buriedItem.transform.position.y + 1);
        Debug.Log(start);
        Debug.Log(end);
    }


    public void Test()
    {
        int _startingX = 500;
        int _startingY = 375;

        int totalCleared = 0;

        Texture2D _texture = spriteRenderer.sprite.texture;

        if (buriedItem.transform.position.x < 0)
        {
            for (int i = 0; i < Mathf.Abs(buriedItem.transform.position.x) + 1; i++)
            {
                _startingX -= 25;
            }
        }
        else if (buriedItem.transform.position.x > 0)
        {
            for (int i = 0; i < buriedItem.transform.position.x - 1; i++)
            {
                _startingX += 25;
            }
        }

        if (buriedItem.transform.position.y < 0)
        {
            for (int i = 0; i < Mathf.Abs(buriedItem.transform.position.y) + 1; i++)
            {
                _startingY -= 25;
            }
        }
        else if (buriedItem.transform.position.y > 0)
        {
            for (int i = 0; i < buriedItem.transform.position.y - 1; i++)
            {
                _startingY += 25;
            }
        }

        for (int i = _startingX; i < _startingX + 50; i++)
        {
            for (int j = _startingY; j < _startingY + 50; j++)
            {
                if(_texture.GetPixel(i, j).a == 0)
                {
                    totalCleared++;
                }
            }
        }

        if(totalCleared >= 2500)
        {
            if(currentDugItem)
            {
                currentDugItem.SetToFound();
            }
        }
    }

    public void CheckIfDugUp()
    {
        var sprite = spriteRenderer.sprite;
        var texture = sprite.texture;
        // get the world space dimensions
        var worldBounds = spriteRenderer.bounds;
        worldBounds.center = buriedItem.transform.position;
        worldBounds.min = start;
        worldBounds.max = end;
        // get the pixel space dimensions
        var pixelRect = sprite.rect;

        // Multiply by this factor to convert world space size to pixels
        var fillerSizeFactor = Vector2.one / worldBounds.size * pixelRect.size;
        var fillerSizeInPixels = Vector2Int.RoundToInt(fillerSize * fillerSizeFactor);

        //var start = worldBounds.min;
        //var end = worldBounds.max;

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
            Debug.Log(" test 1");
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
                    Debug.Log("YEAAAAH");
                    continue;
                }
            }
        }
    }
}