using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Scenario
{
    //day1
    static public readonly List<MessageData> meslist_day1 = new List<MessageData>()
    {
        
        new MessageData{ massage = "うぅ...", color = Color.magenta },
        new MessageData{ massage = "早く“あの人”を見つけなくちゃ…", color = Color.magenta },
        new MessageData{ massage = "でも気づかれないようにしないと…", color = Color.magenta },
        new MessageData{ massage = "だって私は…", color = Color.magenta },
    };
    // day2
    static public readonly List<MessageData> meslist_day2 = new List<MessageData>()
    {
        new MessageData{ massage = "今日もまず“あの人”を見つけるところから始めなきゃ…", color = Color.magenta },
        new MessageData{ massage = "あの人はどこに向かってるのかな…？", color = Color.magenta },
        new MessageData{ massage = "「お店から出てきたところ」を激写しちゃおう！", color = Color.magenta },
        new MessageData{ massage = "隠れながら写真を撮れる場所あるかな…", color = Color.magenta },
    };
    // day3
    static public readonly List<MessageData> meslist_day3 = new List<MessageData>()
    {
        // 開始時
        new MessageData{ massage = "はぁ…今日で3日目か…", color = Color.magenta },
        new MessageData{ massage = "今日は何をしようかな…", color = Color.magenta },
        new MessageData{ massage = "とりあえず町を散策しつつ“あの人”を見つけよう", color = Color.magenta },
        new MessageData{ massage = "まだ見つかっちゃまずいよね…", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        // ハンカチ取得時
        new MessageData{ massage = "ハンカチだ。誰か落としちゃったのかな？", color = Color.magenta },
        new MessageData{ massage = "…これをきっかけに“あの人”に話しかけれないかな", color = Color.magenta },
        new MessageData{ massage = "本当の落とし主さんには悪いけど…", color = Color.magenta },
        new MessageData{ massage = "手段を迷ってる暇はないよね！", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        // ハンカチ拾えなかった時
        new MessageData{ massage = "あーあ。今日何もできなかったな…", color = Color.magenta },
        new MessageData{ massage = "“あの人”とは一生話せないのかな…", color = Color.magenta },
        new MessageData{ massage = "きっかけがあればいいんだけど", color = Color.magenta },
        new MessageData{ massage = "もう無理かも…", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
}
