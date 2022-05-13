using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    float colorPoint = 0;
    Renderer targetRenderer;

    string prevPaintColor = "";

    public GameObject gameManager;
    GameManager gm;

    bool isCounted = false;

    public int targetPoint = 50;
    public int halfwayPoint = 30;

    private void Start()
    {
        targetRenderer = gameObject.GetComponent<Renderer>();
        gm = gameManager.GetComponent<GameManager>();
    }

    public void AddColorPoint(float point, string paintColor)
    {
        if(prevPaintColor != paintColor)
        {
            //if(colorPoint >= targetPoint)
            //{
                if (prevPaintColor == "OrangeBullet")
                {
                    gm.paintedBoxCount[0]--;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
                else if (prevPaintColor == "PinkBullet")
                {
                    gm.paintedBoxCount[1]--;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
                else if (prevPaintColor == "GreenBullet")
                {
                    gm.paintedBoxCount[2]--;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            //}
            
            prevPaintColor = paintColor;
            colorPoint = 0;
            isCounted = false;
        }

        colorPoint += point;

        if(paintColor == "OrangeBullet")
        {
            if (colorPoint >= targetPoint)
            {
                Color color = new Color32(255, 97, 48, 255);
                targetRenderer.material.SetColor("_Color", color);

                if(!isCounted)
                {
                    gm.paintedBoxCount[0]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
                
                
            }
            else if (colorPoint >= halfwayPoint)
            {
                Color color = new Color32(250, 152, 77, 255);
                targetRenderer.material.SetColor("_Color", color);

                if(!isCounted)
                {
                    gm.paintedBoxCount[0]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            }
            else
            {
                Color color = new Color32(252, 188, 139, 255);
                targetRenderer.material.SetColor("_Color", color);

                if(!isCounted)
                {
                    gm.paintedBoxCount[0]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            }
        }
        else if(paintColor == "PinkBullet")
        {
            if (colorPoint >= targetPoint)
            {
                Color color = new Color32(255, 77, 211, 255);
                targetRenderer.material.SetColor("_Color", color);

                if (!isCounted)
                {
                    gm.paintedBoxCount[1]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            }
            else if (colorPoint >= halfwayPoint)
            {
                Color color = new Color32(252, 131, 222, 255);
                targetRenderer.material.SetColor("_Color", color);

                if (!isCounted)
                {
                    gm.paintedBoxCount[1]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            }
            else
            {
                Color color = new Color32(252, 184, 235, 255);
                targetRenderer.material.SetColor("_Color", color);

                if (!isCounted)
                {
                    gm.paintedBoxCount[1]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            }
        }
        else if (paintColor == "GreenBullet")
        {
            if (colorPoint >= targetPoint)
            {
                Color color = new Color32(88, 186, 17, 255);
                targetRenderer.material.SetColor("_Color", color);

                if (!isCounted)
                {
                    gm.paintedBoxCount[2]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            }
            else if (colorPoint >= halfwayPoint)
            {
                Color color = new Color32(127, 191, 80, 255);
                targetRenderer.material.SetColor("_Color", color);

                if (!isCounted)
                {
                    gm.paintedBoxCount[2]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            }
            else
            {
                Color color = new Color32(156, 194, 128, 255);
                targetRenderer.material.SetColor("_Color", color);

                if (!isCounted)
                {
                    gm.paintedBoxCount[2]++;
                    isCounted = true;
                    Debug.Log("orange pink green: " + gm.paintedBoxCount[0] + " " + gm.paintedBoxCount[1] + " " + gm.paintedBoxCount[2]);
                }
            }
        }

    }
}
