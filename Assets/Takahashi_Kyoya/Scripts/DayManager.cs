using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//�����Enemy��public���C���X�y�N�^�ɕ\�������
#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
#endif
public class DayManager : GameManager
{
}