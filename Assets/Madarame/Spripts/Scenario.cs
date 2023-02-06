using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Scenario
{
    //day1
    static public readonly List<MessageData> meslist_day1 = new List<MessageData>()
    {
        // 開始時
        new MessageData{ massage = "うぅ...", color = Color.magenta },
        new MessageData{ massage = "早く“あの人”を見つけなくちゃ…", color = Color.magenta },
        new MessageData{ massage = "でも気づかれないようにしないと…", color = Color.magenta },
        new MessageData{ massage = "だって私は…", color = Color.magenta },
    };
    // day2
    static public readonly List<MessageData> meslist_day2 = new List<MessageData>()
    {
        // 開始時
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
    // day4
    static public readonly List<MessageData> meslist_day4 = new List<MessageData>()
    {
        // 開始時
        new MessageData{ massage = "ハンカチは…よし。ちゃんと持ってきてる", color = Color.magenta },
        new MessageData{ massage = "「これ落としましたか？あなたのだと思って…」", color = Color.magenta },
        new MessageData{ massage = "予行練習はこんな感じでよしと", color = Color.magenta },
        new MessageData{ massage = "今日こそ“あの人”に話しかけるんだ…！", color = Color.magenta },
        new MessageData{ massage = "でも正面からは恥ずかしいから「後ろから」話しかけよう…", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        // 時間切れ
        new MessageData{ massage = "やっぱりこんな汚い手を使って話しかけようなんて…", color = Color.magenta },
        new MessageData{ massage = "…いや、ただ話しかけることすらできない自分への言い訳だよ…", color = Color.magenta },
        new MessageData{ massage = "ダメもとで明日話しかけちゃおうかな…", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
    // day5
    static public readonly List<MessageData> meslist_day5 = new List<MessageData>()
    {
        // 開始時
        new MessageData{ massage = "今日こそ“あの人”に話しかけるんだ…！", color = Color.magenta },
        new MessageData{ massage = "少し怖いけどきっと大丈夫だよね…", color = Color.magenta },
        new MessageData{ massage = "…　…", color = Color.magenta },
        new MessageData{ massage = "よし、行こう", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        // ハンカチを渡していた場合
        new MessageData{ massage = "　", color = Color.magenta },
        new MessageData{ massage = "　", color = Color.magenta },
        new MessageData{ massage = "　", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
        // ハンカチを渡していない場合
        new MessageData{ massage = "　", color = Color.magenta },
        new MessageData{ massage = "　", color = Color.magenta },
        new MessageData{ massage = "　", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
}
