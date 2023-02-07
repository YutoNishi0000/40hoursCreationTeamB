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
        new MessageData{ massage = "早く<color=#ffff00>“あの人”</color>を見つけなくちゃ…", color = Color.magenta },
        new MessageData{ massage = "でも気づかれないようにしないと…", color = Color.magenta },
        new MessageData{ massage = "だって私は…", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
    // day2
    static public readonly List<MessageData> meslist_day2 = new List<MessageData>()
    {
        // 開始時
        new MessageData{ massage = "今日もまず“あの人”を見つけるところから始めなきゃ…", color = Color.magenta },
        new MessageData{ massage = "あの人はどこに向かってるのかな…？", color = Color.magenta },
        new MessageData{ massage = "「お店から出てきたところ」を激写しちゃおう！", color = Color.magenta },
        new MessageData{ massage = "隠れながら写真を撮れる場所あるかな…", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
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
        //ハンカチイベント
        new MessageData{ massage = "あ、あの！", color = Color.magenta },
        new MessageData{ massage = "は、はい？", color = Color.cyan },
        new MessageData{ massage = "これ落としましたか？あなたのだと思って・・・", color = Color.magenta },
        new MessageData{ massage = "あ・・ち、違います", color = Color.cyan },
        new MessageData{ massage = "そうですか…私の方で持ち主さん探してみますね！", color = Color.magenta },
        new MessageData{ massage = "きっと困っているだろうし…", color = Color.magenta },
        new MessageData{ massage = "そうですね…持ち主見つかるといいですね！", color = Color.cyan },
        new MessageData{ massage = "心優しい方に拾ってもらえたようで良かったです", color = Color.cyan },
        new MessageData{ massage = "では、無事持ち主が見つかることを祈ってます！", color = Color.cyan },
        new MessageData{ massage = "#", color = Color.cyan },

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
        
        
    };
    // day5 ハンカチを渡していた場合
    static public readonly List<MessageData> meslist_GameClear = new List<MessageData>()
    {
        new MessageData{ massage = "あ、あの！", color = Color.magenta },
        new MessageData{ massage = "は、はい…", color = Color.cyan },
        new MessageData{ massage = "あ！あなたは昨日の！", color = Color.cyan },
        new MessageData{ massage = "覚えてくれてて良かったです！", color = Color.magenta },
        new MessageData{ massage = "ハンカチの持ち主は見つかりましたか？", color = Color.cyan },
        new MessageData{ massage = "あー、は、はい！見つかりました！", color = Color.magenta },
        new MessageData{ massage = "それは良かったです", color = Color.cyan },
        new MessageData{ massage = "…　…", color = Color.magenta },
        new MessageData{ massage = "あのー。そのキーホルダーって…", color = Color.magenta },
        new MessageData{ massage = "これですか？この前駅前で落ちてるのを拾ったんですよね～", color = Color.cyan },
        new MessageData{ massage = "そ、そうだったんですか…実はそれ生産が世界で1つだけの激レア品で…", color = Color.magenta },
        new MessageData{ massage = "そ、そうなんですか！？", color = Color.cyan },
        new MessageData{ massage = "はい…私が作った「ミラクルダブルバイセップスキーホルダー」なので…", color = Color.magenta },
        new MessageData{ massage = "あ、あなたが作ったものだったんですね…", color = Color.cyan },
        new MessageData{ massage = "自作の物だとは思いませんでした！完成度高くてびっくりです！", color = Color.cyan },
        new MessageData{ massage = "そうなんです…もしよければ返してもらってよろしいですか？", color = Color.magenta },
        new MessageData{ massage = "あ、はい…大丈夫です。ダサいんで…", color = Color.cyan },
        new MessageData{ massage = "え…？", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
    // day5 ハンカチを渡していない場合
    static public readonly List<MessageData> meslist_GameOver = new List<MessageData>()
    {
        new MessageData{ massage = "あ、あの！", color = Color.magenta },
        new MessageData{ massage = "は、はい・・・", color = Color.magenta },
        new MessageData{ massage = "わ、わたし・・・その・・・", color = Color.magenta },
        new MessageData{ massage = "あー、多分人違いだと思いますので・・・", color = Color.magenta },
        new MessageData{ massage = "い、いや、その・・・", color = Color.magenta },
        new MessageData{ massage = "それじゃ、失礼します", color = Color.magenta },
        new MessageData{ massage = "#", color = Color.magenta },
    };
}
