using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.IO;

/// <summary>
/// 이펙트 클립 리스트와 이펙트 파일이름과 경로를 가지고 있으며
/// 파일을 읽고 쓰는 기능을 가지고 있다.
/// </summary>
public class EffectData : BaseData
{
    public EffectClip[] effectClips = new EffectClip[0];

    public string clipPath = "Effects/";
    private string xmlFilePath = "";
    private string xmlFileName = "effectData.xml";
    private string dataPath = "Data/effectData";
    //XML 구분자.
    private const string EFFECT = "effect"; //저장 키.
    private const string CLIP = "clip";     //저장 키.

    private EffectData() { }
    //읽어오고 저장하고, 데이터를 삭제하고, 특정 클립을 얻어오고, 복사하는 기능
    public void LoadData()
    {
        Debug.Log($"xmlFilePath = {Application.dataPath} + {dataDirectory}");
        // $ 없을 때 Debug.Log("xmlFilePath = " + Application.dataPath + " + " + dataDirectory);
        this.xmlFilePath = Application.dataPath + dataDirectory;
        TextAsset asset = (TextAsset)ResourceManager.Load(dataPath);
        if (asset == null || asset.text == null)
        {
            this.AddData("New Effect");
            return;
        }

        using (XmlTextReader reader = new XmlTextReader(new StringReader(asset.text)))
        {
            int currentID = 0;
            while(reader.Read())
            {
                if(reader.IsStartElement())
                {
                    switch(reader.Name)
                    {
                        case "length":
                            int length = int.Parse(reader.ReadString());
                            this.names = new string[length];
                            break;
                        case "id":
                            break;
                        case "name":
                            break;
                        case "effectType":
                            break;
                        case "effectName":
                            break;
                        case "effectPath":
                            break;

                    }
                }
            }
        }
    }

}
