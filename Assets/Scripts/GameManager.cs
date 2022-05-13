using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    List<int> randomIndexList = new List<int>();
    
    public GameObject[] woodenBoxes;

    public GameObject gun;
    public GameObject paintIcon;
    public GameObject coloringBoxes;

    public int[] paintedBoxCount;
    int targetNum;

    public GameObject[] path;
    //bool showPath = false;
    int maxBoxCountIndex = -1;
    int prevMaxIndex = -1;

    public float timeRemaining = 10;
    public Text textPlayTime;
    public Text textTotalBoxCount;
    public Text[] textColoredBoxCount;

    public GameObject[] highlight;
    public float blinkingTimer = 0.0f;
    public float blinkingDuration = 0.3f;

    public GameObject boxCountUI;

    public bool isEndedGame = false;
    public GameObject endGUI;
    public Text textEndGUI;
    public GameObject crossHairGUI;

    public Text guideText;
    public float guideShowDuration = 2f;
    float hideGuideTime;

    public CheckDroppedObj droppedChecker;
    bool isPlayerDrop;
    bool isGunDrop;
    bool[] isPaintDrop = new bool[3];
    bool isAllPaintDrop = false;

    public PlayerController playerController;
    public bool isMetPrincess;

    void Start()
    {

        if (SceneManager.GetActiveScene().name != "Start")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        int woodenBoxNum = GameObject.FindGameObjectsWithTag("WoodenBox").Length;

        //GetRandomIndexList(4, woodenBoxNum);

        woodenBoxes[0].GetComponent<WoodenTarget>().paintHolder[0] = true; //orange
        woodenBoxes[1].GetComponent<WoodenTarget>().paintHolder[1] = true; //pink
        woodenBoxes[2].GetComponent<WoodenTarget>().paintHolder[2] = true;//green
        woodenBoxes[6].GetComponent<WoodenTarget>().gunHolder = true;

        // woodenBoxes[randomIndexList[0]].GetComponent<WoodenTarget>().paintHolder[0] = true; //orange
        // woodenBoxes[randomIndexList[1]].GetComponent<WoodenTarget>().paintHolder[1] = true; //pink
        // woodenBoxes[randomIndexList[2]].GetComponent<WoodenTarget>().paintHolder[2] = true;//green
        // woodenBoxes[randomIndexList[3]].GetComponent<WoodenTarget>().gunHolder = true;

        isPlayerDrop = droppedChecker.isPlayerDrop;
        isGunDrop = droppedChecker.isGunDrop;
        isMetPrincess = playerController.isMetPrincess;

        for(int i = 0;i<isPaintDrop.Length;i++)
        {
            isPaintDrop[i] = droppedChecker.isPaintDrop[i];
        }

        if(isPaintDrop[0] && isPaintDrop[1] && isPaintDrop[2])
        {
            isAllPaintDrop = true;
        }

        showGuideText("RIGHT CLICK TO THROW GRENADE AT THE BOXES");

    }

    // Update is called once per frame
    void Update()
    {
        if(isEndedGame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crossHairGUI.SetActive(false);

            coloringBoxes.SetActive(false);

            if(isMetPrincess)
            {
                textEndGUI.text = "CLEAR";
            }
            else
            {
                textEndGUI.text = "FAIL";
            }

            endGUI.SetActive(true);
        }
        else
        {
            timeRemaining -= Time.deltaTime;
            displayTime(timeRemaining);

            isPlayerDrop = droppedChecker.isPlayerDrop;
            isGunDrop = droppedChecker.isGunDrop;
            isMetPrincess = playerController.isMetPrincess;

            for(int i = 0;i<isPaintDrop.Length;i++)
            {
                isPaintDrop[i] = droppedChecker.isPaintDrop[i];
            }

            if(isPaintDrop[0] && isPaintDrop[1] && isPaintDrop[2])
            {
                isAllPaintDrop = true;
            }

            if(isPlayerDrop || isGunDrop || isAllPaintDrop || timeRemaining <= 0 || isMetPrincess) 
            {
                if(isGunDrop)
                {
                    showGuideText("YOU NEED A GUN BUT IT IS GONE");
                }
                if(isAllPaintDrop)
                {
                    showGuideText("YOU NEED A PAINT BUT ALL COLOR PAINTS ARE GONE");
                }
                if(isPlayerDrop)
                {
                    showGuideText("YOU WENT OUT OF BOUNDS");
                }
                if(timeRemaining <= 0)
                {
                    showGuideText("YOU RAN OUT OF TIME");
                }
                Debug.Log("GameEnded!");
                isEndedGame = true;
            }

            if(gun.activeInHierarchy && paintIcon.activeInHierarchy && !boxCountUI.activeInHierarchy)
            {
                boxCountUI.SetActive(true);
                coloringBoxes.SetActive(true);
                showGuideText("LEFT CLICK TO PAINT OUT YOUR LOVE");
                targetNum = GameObject.FindGameObjectsWithTag("Box").Length;
            }

            if(coloringBoxes.activeInHierarchy)
            {
                int totalColoredNum = 0;
                for (int i = 0; i < paintedBoxCount.Length; i++)
                {
                    totalColoredNum += paintedBoxCount[i];
                    textColoredBoxCount[i].text = "x " + paintedBoxCount[i].ToString("00");
                }

                textTotalBoxCount.text = totalColoredNum.ToString("00") + " / " + targetNum.ToString("00");

                if (targetNum == totalColoredNum)
                {
                    if(prevMaxIndex == -1)
                    {
                        showGuideText("NOW GO RESCUE YOUR LOVE");
                    }
                    maxBoxCountIndex = GetMaxIndex();
                    if((prevMaxIndex != -1) && (prevMaxIndex!=maxBoxCountIndex)) //바뀐 경우. 이전 것 비활성화
                    {
                        highlight[prevMaxIndex].SetActive(false);
                        path[prevMaxIndex].SetActive(false);
                    }
                    BlinkHighlight(maxBoxCountIndex);
                    //showPath = true;
                    path[maxBoxCountIndex].SetActive(true);
                    prevMaxIndex = maxBoxCountIndex;
                }
                // else
                // {
                //     if(showPath)
                //     {
                //         showPath = false;
                //         highlight[maxBoxCountIndex].SetActive(false);
                //         path[maxBoxCountIndex].SetActive(false);
                //     }
                // }
            }
        }

        if(guideText.gameObject.activeInHierarchy && (Time.time >= hideGuideTime))
        {
            guideText.gameObject.SetActive(false);
        }

        
        
    }

    void showGuideText(string guideInfo)
    {
        guideText.text = guideInfo;
        guideText.gameObject.SetActive(true);
        hideGuideTime = Time.time + guideShowDuration;
    }

    void displayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        int seconds = (int)(timeToDisplay % 60f);
        int minutes = (int)((timeToDisplay / 60f) % 60f);
        textPlayTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void restartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void BlinkHighlight(int maxIndex)
    {
        blinkingTimer += Time.deltaTime;
        if(blinkingTimer >= blinkingDuration)
        {
            blinkingTimer = 0.0f;
            if(highlight[maxIndex].activeInHierarchy)
            {
                highlight[maxIndex].SetActive(false);
            }
            else
            {
                highlight[maxIndex].SetActive(true);
            }
        }
    }

    int GetMaxIndex()
    {
        int max = 0;
        int maxIndex = 0;

        for(int i = 0;i<paintedBoxCount.Length;i++)
        {
            if(paintedBoxCount[i]>max)
            {
                max = paintedBoxCount[i];
                maxIndex = i;

            }
        }

        return maxIndex;
    }

    // void GetRandomIndexList(int neededNum, int max)
    // {
    //     int currentNumber = Random.Range(0, max);

    //     for(int i = 0;i<neededNum;)
    //     {
    //         if(randomIndexList.Contains(currentNumber))
    //         {
    //             currentNumber = Random.Range(0, max);
    //         }
    //         else
    //         {
    //             randomIndexList.Add(currentNumber);
    //             i++;
    //         }
    //     }
    // }
}
