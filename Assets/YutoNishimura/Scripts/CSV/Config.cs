using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static List<string[]> gameInfo;    //ÉQÅ[ÉÄÇÃè⁄ç◊ê›íË

    public static float playerSpeed;

    public static float targetSpeed;

    public static float subTargetSpeed;

    public static float mouseHorizon;

    public static float mouseVertical;

    public static float gravity;

    public static float numSubTargetInField;

    public static float subSubTargetGenerationCoolTime;

    public static float skillLevel1;
        
    public static float skillLevel2;

    public static float skillLevel3;

    public static float raisePlayerSpeed;

    public static float intervalActiveTargetMInimap;

    public static float targetShutterPlusCount;

    public static float airShutterMinusCount;

    public static float easyModeTime;

    public static float nomalModeTime;

    public static float hardModeTime;

    public static float easyTargetScore;

    public static float nomalTargetScore;

    public static float hardTargetScore;

    public static float popSpeed;

    public static float popUpSize;

    public static float moveBarSpeed;

    private void Start()
    {
        gameInfo = CSVReader.CsvData;

        playerSpeed = float.Parse(gameInfo[0][1]);

        targetSpeed = float.Parse(gameInfo[1][1]);

        subTargetSpeed = float.Parse(gameInfo[2][1]);

        mouseHorizon = float.Parse(gameInfo[3][1]);

        mouseVertical = float.Parse(gameInfo[4][1]);

        gravity = float.Parse(gameInfo[5][1]);

        numSubTargetInField = float.Parse(gameInfo[6][1]);

        subSubTargetGenerationCoolTime = float.Parse(gameInfo[7][1]);

        skillLevel1 = float.Parse(gameInfo[8][1]);

        skillLevel2 = float.Parse(gameInfo[9][1]);

        skillLevel3 = float.Parse(gameInfo[10][1]);

        raisePlayerSpeed = float.Parse(gameInfo[11][1]);

        intervalActiveTargetMInimap = float.Parse(gameInfo[12][1]);

        targetShutterPlusCount = float.Parse(gameInfo[13][1]);

        airShutterMinusCount = float.Parse(gameInfo[14][1]);

        easyModeTime = float.Parse(gameInfo[15][1]);

        nomalModeTime = float.Parse(gameInfo[16][1]);

        hardModeTime = float.Parse(gameInfo[17][1]);

        easyTargetScore = float.Parse(gameInfo[18][1]);

        nomalTargetScore = float.Parse(gameInfo[19][1]);

        hardTargetScore = float.Parse(gameInfo[20][1]);

        popSpeed = float.Parse(gameInfo[21][1]);

        popUpSize = float.Parse(gameInfo[22][1]);

        moveBarSpeed = float.Parse(gameInfo[23][1]);

        Debug.Log(playerSpeed);
        Debug.Log(targetSpeed);
        Debug.Log(subTargetSpeed);
        Debug.Log(mouseHorizon);
        Debug.Log(mouseVertical);
        Debug.Log(gravity);
        Debug.Log(numSubTargetInField);
        Debug.Log(subSubTargetGenerationCoolTime);
        Debug.Log(skillLevel1);
        Debug.Log(skillLevel2);
        Debug.Log(skillLevel3);
        Debug.Log(raisePlayerSpeed);
        Debug.Log(intervalActiveTargetMInimap);
        Debug.Log(targetShutterPlusCount);
        Debug.Log(airShutterMinusCount);
        Debug.Log(easyModeTime);
        Debug.Log(nomalModeTime);
        Debug.Log(hardModeTime);
        Debug.Log(easyTargetScore);
        Debug.Log(nomalTargetScore);
        Debug.Log(hardTargetScore);
        Debug.Log(popSpeed);
        Debug.Log(popUpSize);
        Debug.Log(moveBarSpeed);
    }
}
