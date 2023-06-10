using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//CSV�ǂݍ��݁i�s�A��Ŏw��j
public class CSVReader : MonoBehaviour
{
    [SerializeField] private string filePath;                         // �t�@�C���p�X
    private TextAsset csvFile;                                        // csv�t�@�C��
    private static List<string[]> csvData;      // �f�[�^�i�[�ꏊ

    private void Awake()
    {
        csvData = new List<string[]>();

        // �t�@�C���p�X�œǂݍ���
        csvFile = Resources.Load(filePath) as TextAsset;

        // TextAsset��StringReader�ɕϊ�
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            // ��s���ǂݍ���
            string line = reader.ReadLine();

            csvData.Add(line.Split(','));
        }
    }

    public static List<string[]> CsvData { get => csvData; }
}