using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    private TextAsset csvFile;
    private string lineSeperator = System.Environment.NewLine;
    private char surround = '"';
    private string[] fieldSeperator = { "\",\"" };

    #region Singleton
    public static CSVLoader Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    public void LoadCSV(string attributeId)
    {
        switch(attributeId)
        {
            case "en":
                csvFile = Resources.Load<TextAsset>("localizationEN");
                break;
            case "pl":
                csvFile = Resources.Load<TextAsset>("localizationPL");
                break;
        }
        
    }

    public Dictionary<string,string> GetDictionaryValues()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split(lineSeperator);

        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for(int i = 0; i<lines.Length; i++)
        {
            string line = lines[i];

            string[] fields = CSVParser.Split(line);

            for(int f=0; f<fields.Length; f++)
            {
                fields[f] = fields[f].TrimStart(' ', surround);
                fields[f] = fields[f].TrimEnd(surround);
            }

            string key = fields[0];

            if (dictionary.ContainsKey(key))
            {
                continue;
            }

            string value = fields[1];

            dictionary.Add(key, value);
        }

        return dictionary;
    }
}
