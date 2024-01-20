using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea
{
    private const float SCREEN_WIDTH    = 1280;
    private const float SCREEN_HEIGHT   = 720;
    public static void SetSafeArea(RectTransform rectTransform)
    {
        //rectTransform에 해당하는 UI를 safeArea에 맞춘다.

        Rect safeArea = Screen.safeArea;
        Vector2 minAnchor = safeArea.position;
        Vector2 maxAnchor = minAnchor + safeArea.size;
        Vector2 newMinPos = Vector2.zero;
        Vector2 newMaxPos = Vector2.zero;
        if (safeArea.width * SCREEN_HEIGHT < SCREEN_WIDTH * safeArea.height)
        {
            float newHeight = safeArea.width * (SCREEN_HEIGHT / SCREEN_WIDTH);

            float minX = minAnchor.x;
            float minY = minAnchor.y + (safeArea.height - newHeight) / 2.0f;

            float maxX = maxAnchor.x;
            float maxY = maxAnchor.y - (safeArea.height - newHeight) / 2.0f;

            newMinPos = new Vector2(minX, minY);
            newMaxPos = new Vector2(maxX, maxY);
        }
        else
        {
            float newWidth = safeArea.height * (SCREEN_WIDTH / SCREEN_HEIGHT);

            float minY = minAnchor.y;
            float minX = minAnchor.x + (safeArea.width - newWidth) / 2.0f;

            float maxY = maxAnchor.y;
            float maxX = maxAnchor.x - (safeArea.width - newWidth) / 2.0f;

            newMinPos = new Vector2(minX, minY);
            newMaxPos = new Vector2(maxX, maxY);
        }

        newMinPos.x /= Screen.width;
        newMinPos.y /= Screen.height;
        newMaxPos.x /= Screen.width;
        newMaxPos.y /= Screen.height;

        rectTransform.anchorMin = newMinPos;
        rectTransform.anchorMax = newMaxPos;
    }
}
