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

    public static int numSubTargetInField;

    public static float subSubTargetGenerationCoolTime;

    public static int skillLevel1;
        
    public static int skillLevel2;

    public static int skillLevel3;

    public static float raisePlayerSpeed;

    public static float intervalActiveTargetMInimap;

    public static float targetShutterPlusCount;

    public static float airShutterMinusCount;

    public static int easyModeTime;

    public static int nomalModeTime;

    public static int hardModeTime;

    public static int easyTargetScore;

    public static int nomalTargetScore;

    public static int hardTargetScore;

    public static float popSpeed;

    public static float popUpSize;

    public static float moveBarSpeed;

    public static float raiseScore;

    public static float reduceScaleFirst;

    public static float reduceScaleSecond;

    public static float movePrevTimeFirst;

    public static float movePrevTimeSecond;

    public static float movePrevTimeThird;

    public static float changePrevTransformFirst;

    public static float changePrevTransformSecond;

    public static float changePrevTransformThird;

    public static float delayTimeShutterAnimation;

    public static float fadeOutSpeed;

    public static float TimeActivationUI;

    public static int targetVisibilityFirstPhase;

    public static int targetVisibilitySecondPhase;

    public static float subTargetJudgeLength;

    private void Start()
    {
        gameInfo = CSVReader.CsvData;

        playerSpeed = float.Parse(gameInfo[0][1]);

        targetSpeed = float.Parse(gameInfo[1][1]);

        subTargetSpeed = float.Parse(gameInfo[2][1]);

        mouseHorizon = float.Parse(gameInfo[3][1]);

        mouseVertical = float.Parse(gameInfo[4][1]);

        gravity = float.Parse(gameInfo[5][1]);

        numSubTargetInField = int.Parse(gameInfo[6][1]);

        subSubTargetGenerationCoolTime = float.Parse(gameInfo[7][1]);

        skillLevel1 = int.Parse(gameInfo[8][1]);

        skillLevel2 = int.Parse(gameInfo[9][1]);

        skillLevel3 = int.Parse(gameInfo[10][1]);

        raisePlayerSpeed = float.Parse(gameInfo[11][1]);

        intervalActiveTargetMInimap = float.Parse(gameInfo[12][1]);

        targetShutterPlusCount = float.Parse(gameInfo[13][1]);

        airShutterMinusCount = float.Parse(gameInfo[14][1]);

        easyModeTime = int.Parse(gameInfo[15][1]);

        nomalModeTime = int.Parse(gameInfo[16][1]);

        hardModeTime = int.Parse(gameInfo[17][1]);

        easyTargetScore = int.Parse(gameInfo[18][1]);

        nomalTargetScore = int.Parse(gameInfo[19][1]);

        hardTargetScore = int.Parse(gameInfo[20][1]);

        popSpeed = float.Parse(gameInfo[21][1]);

        popUpSize = float.Parse(gameInfo[22][1]);

        moveBarSpeed = float.Parse(gameInfo[23][1]);

        raiseScore = float.Parse(gameInfo[24][1]);

        reduceScaleFirst = float.Parse(gameInfo[25][1]);

        reduceScaleSecond = float.Parse(gameInfo[26][1]);

        movePrevTimeFirst = float.Parse(gameInfo[27][1]);

        movePrevTimeSecond = float.Parse(gameInfo[28][1]);

        movePrevTimeThird = float.Parse(gameInfo[29][1]);

        changePrevTransformFirst = float.Parse(gameInfo[30][1]);

        changePrevTransformSecond = float.Parse(gameInfo[31][1]);

        changePrevTransformThird = float.Parse(gameInfo[32][1]);

        delayTimeShutterAnimation = float.Parse(gameInfo[33][1]);

        fadeOutSpeed = float.Parse(gameInfo[34][1]);

        TimeActivationUI = float.Parse(gameInfo[35][1]);

        targetVisibilityFirstPhase = int.Parse(gameInfo[36][1]);

        targetVisibilitySecondPhase = int.Parse(gameInfo[37][1]);

        subTargetJudgeLength = float.Parse(gameInfo[38][1]);
    }
}
