using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ReadWriteJSON : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        string url = "http://192.168.99.100/puzzle/start";
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        string json = string.Empty;
        if (req.isNetworkError)
            print("ERROR");
        else if (req.responseCode == 200)
            json = req.downloadHandler.text;

        print(json);


    }
    [System.Serializable]
    public class u_user_puzzle
    {
        public string secure_token;
        public int user_id;
        public int high_score;
    }

    IEnumerator ReadMemo()
    {
        //WriteMemoを実行し終わるまで待つ
        yield return StartCoroutine("WriteMemo");

        //URLからデータを取得
        WWW www = new WWW("http://192.168.99.100/puzzle/start");
        yield return www;

        //Null or empty ならブレイク
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
            yield break;
        }
        //データを代入し整形
        string json = www.text;
        u_user_puzzle userData = (u_user_puzzle)JsonUtility.FromJson<u_user_puzzle>(json);

        //テスト用にコンソールに書き出し
        print(userData.secure_token);
        print(userData.user_id);
        print(userData.high_score);
    }
}