using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

public class Cjson : MonoBehaviour
{
    string path = Path.Combine(Application.streamingAssetsPath, "playerData.json");
    void Start()
    {
        Show1();
    }

    void Show1()
    {
        string json = File.ReadAllText(path);
        Debug.Log(json);
        JToken root = JToken.Parse(json);

        JToken players = root["players"];


        for (int i = 0; i < players.Count(); i++)
        {
            Debug.Log($"{players[i]["id"]} 이 플레이어 번호고" + $"이름은 {players[i]["name"]}");
            JToken skills = players[i]["skills"];

            for (int j = 0; j < skills.Count(); j++)
            {
                JToken skill = skills[j];
                Debug.Log($"{skill["damage"]}");
            }
        }

        Debug.Log($"{players[0]["skills"][0]["effects"][1]["duration"]}");
    }

    public void Show3()
    {

    }

    IEnumerator SendDataFormWeb()
    {

        var tutor = new
        {
            name = "찬용",
            score = 77
        };

        string url = "https://sindatadragon.synology.me/TeamSparta/post.php";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        

        yield return null;
    }
}
