using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//これでEnemyのpublicがインスペクタに表示される
#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
#endif
public class DayManager : GameManager
{
}