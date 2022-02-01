using System;
using System.Collections.Generic;
using Takochu.fmt;
using Takochu.util;

namespace Takochu.smg
{
    public class Scenario
    {
        public Scenario(BCSV.Entry entry, List<string> zoneList)
        {
            mEntry = entry;
            //foreach(var e in entry)
            //Console.WriteLine(BCSV.HashToFieldName(e.Key));
            mScenarioNo = mEntry.Get<int>("ScenarioNo");
            mScenarioName = mEntry.Get<string>("ScenarioName");
            mPowerStarID = mEntry.Get<int>("PowerStarId");
            mAppearPowerStar = mEntry.Get<string>("AppearPowerStarObj");

            mErrorCheck = mEntry.Get<int>("ErrorCheck");

            if (GameUtil.IsSMG2())
            {
                mPowerStarType = mEntry.Get<string>("PowerStarType");
                mCometLimitTimer = mEntry.Get<int>("CometLimitTimer");
            }
            else
            {
                mIsHidden = mEntry.Get<int>("IsHidden");
                if (!mEntry.ContainsKey("LuigiModeTimer"))
                    mLuigiModeTimer = default;
                else
                    mLuigiModeTimer = mEntry.Get<int>("LuigiModeTimer");
            }

            if (!mEntry.ContainsKey("Comet"))
                mComet = "";
            else
                mComet = mEntry.Get<string>("Comet");
            
            mZoneMasks = new Dictionary<string, int>();

            foreach (string zone in zoneList)
                mZoneMasks.Add(zone, mEntry.Get<int>(zone));
        }

        public void RemoveZone(string zoneName)
        {
            mZoneMasks.Remove(zoneName);
        }

        public BCSV.Entry mEntry { get; private set; }
        public int mScenarioNo { get; private set; }
        public string mScenarioName { get; private set; }
        public int mPowerStarID { get; private set; }
        public string mAppearPowerStar { get; private set; }
        public string mPowerStarType { get; private set; }
        public string mComet { get; private set; }
        public int mCometLimitTimer { get; private set; }
        public int mIsHidden { get; private set; }
        public int mErrorCheck { get; private set; }
        public int mLuigiModeTimer { get; private set; }

        public Dictionary<string, int> mZoneMasks { get; private set; }

        /// <summary>
        /// SMG2で使用されるコメットの一覧
        /// </summary>
        public static readonly string[] SMG2Comets = new string[]
        {
            "",
            "Red",
            "Purple",
            "Quick",
            "Dark",
            "Exterminate",
            "Mimic"
        };

        /// <summary>
        /// SMG1で使用されるコメットの一覧
        /// </summary>
        public static readonly string[] SMG1Comets = new string[]
        {
            "",
            "Red",
            "Purple",
            "Quick",
            "Dark",
            "Black",
            "Ghost"
        };

        public static readonly string[] StarType = new string[]
        {
            "Normal",
            "Hidden",
            "Green"
        };

        public static readonly string[] SMG2AppearPowerStarObj = new string[]
        {
            "",
            "ベビーディノパックン",
            "ボスジュゲム",
            "二脚ボス",
            "ボスカメムシ",
            "キングトッシン",
            "ユッキーナ",
            "ボスブッスン",
            "サンディー",
            "オタロックタンク",
            "バッタンキング",
            "ディノパックンVs2",
            "ベリードラゴン",
            "ベリードラゴンＬｖ２",
            "クッパJrロボット",
            "クッパJrキャッスル",
            "クッパ",
            "クッパLv2",
            "クッパLv3",
            "子連れカメムシ",
            "雪像クッパ",
            "ゴールドワンワン",
            "テレサチーフ",
            "巨大テレサ",
            "ロゼッタ",
            "キノピオ",
            "モック",
            "パマタリアン",
            "パマタリアンハンター",
            "ピーチャン",
            "ピーチャンレーサー",
            "いたずらウサギ",
            "いたずら幽霊ウサギ",
            "さすらいの遊び人(通常会話)",
            "さすらいの遊び人(スコアアタック)",
            "さすらいの遊び人(ムイムイアタック)",
            "さすらいの遊び人(トゲピンアタック)",
            "全滅監視チェッカー[パワースター]",
            "花さか監視[パワースター]",
            "フリップパネル監視者",
            "クリスタルケージ[小]",
            "クリスタルケージ[中]",
            "クリスタルケージ[大]",
            "壊れる籠",
            "壊れる籠[大]",
            "壊れる籠[回転タイプ]",
            "木箱",
            "ハテナ木箱",
            "砂上下タワー破壊壁Ａ",
            "砂上下タワー破壊壁Ｂ",
            "つらら岩",
            "雪ブロック",
            "クッパ彫像",
            "テレサマンション通り抜け穴のフタ",
            "クロックワークハンドル",
            "まゆEXキャノン",
            "沈没船",
            "たまころ",
            "チコ集め",
            "音符の妖精",
            "１００枚コイン"
        };

        public static readonly string[] SMG1AppearPowerStarObj = new string[]
        {
            "",
            "ディノパックン",
            "ディノパックンVs2",
            "オタキング",
            "ボスカメムシ",
            "ボスベーゴマン",
            "トゥームスパイダー",
            "ボスカメック",
            "ボスカメック2",
            "ポルタ",
            "アイスメラメラキング",
            "ドドリュウ",
            "スカルシャーク",
            "ウォーターバズーカ",
            "エレクトリックバズーカ",
            "メカクッパパーツヘッド",
            "クッパ大王（ＶＳ１）",
            "クッパ大王（ＶＳ２）",
            "クッパ大王（ＶＳ３）",
            "三脚ボス",
            "クッパＪｒシップ",
            "子連れカメムシ",
            "ゴールドワンワン",
            "テレサチーフ",
            "キノピオ",
            "天文台用キノピオ",
            "パマタリアン",
            "シャッチー",
            "シャッチー（会話用）",
            "テレサレーサー",
            "ペンギンレーサーリーダー",
            "ペンギンコーチ",
            "NPC用ルイージ",
            "イベント用ルイージ",
            "いたずらウサギ",
            "いたずらウサギ[自由逃走]",
            "いたずらウサギ集め",
            "いたずら幽霊ウサギ",
            "クリボー全滅チェッカー",
            "スカルシャークベイビー全滅チェッカー",
            "メラメラ全滅チェッカー",
            "フリップパネル監視者",
            "クリスタルケージ[小]",
            "クリスタルケージ[中]",
            "クリスタルケージ[大]",
            "壊れる籠",
            "壊れる籠[大]",
            "壊れる籠[回転タイプ]",
            "木箱",
            "砂上下タワー破壊壁Ａ",
            "砂上下タワー破壊壁Ｂ",
            "ひび割れ宝箱（パワースター）",
            "タルタコの樽A",
            "つらら岩",
            "雪ブロック",
            "クッパ彫像",
            "テレサマンション通り抜け穴のフタ",
            "クロックワークハンドル",
            "まゆEXキャノン",
            "沈没船",
            "ビー玉惑星",
            "たまころ",
            "チコ集め",
            "音符の妖精",
            "１００枚コイン"
        };
    }
}
