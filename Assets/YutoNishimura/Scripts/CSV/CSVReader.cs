using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//CSV読み込み（行、列で指定）
public class CSVReader : MonoBehaviour
{
    [SerializeField] private string filePath;                         // ファイルパス
    private TextAsset csvFile;                                        // csvファイル
    private static List<string[]> csvData;      // データ格納場所

    private void Awake()
    {
        csvData = new List<string[]>();

        // ファイルパスで読み込み
        csvFile = Resources.Load(filePath) as TextAsset;

        // TextAssetをStringReaderに変換
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            // 一行ずつ読み込む
            string line = reader.ReadLine();

            csvData.Add(line.Split(','));
        }
    }

    public static List<string[]> CsvData { get => csvData; }
}