using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.util;

namespace Takochu.smg
{
    public class Scenario
    {
        private List<string> _zoneList;

        public Scenario(BCSV.Entry entry, List<string> zoneList)
        {
            mEntry = entry;
            _zoneList = zoneList;

            mScenarioNo = mEntry.Get<int>("ScenarioNo");
            mScenarioName = mEntry.Get<string>("ScenarioName");
            mPowerStarID = mEntry.Get<int>("PowerStarId");
            mAppearPowerStar = mEntry.Get<string>("AppearPowerStarObj");

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
            mTest_ZoneMasks = new Dictionary<string, Layer[]>();

            foreach (string zone in zoneList)
            {
                mZoneMasks.Add(zone, mEntry.Get<int>(zone));
            }

            ReloadTest_ZoneMask();

        }

        public void ReloadTest_ZoneMask()
        {
            mTest_ZoneMasks = new Dictionary<string, Layer[]>();

            foreach (string zone in _zoneList)
            {


                int[] layerArray = { mEntry.Get<int>(zone) };
                BitArray bitArray = new BitArray(layerArray);

                var layers = new Layer[16];


                Console.WriteLine();
                Console.WriteLine(zone);
                for (int i = 0; i < 16; i++)
                {
                    layers[i].Name = GameUtil.GalaxyLayers[i];
                    layers[i].Checked = bitArray[i];

                    Console.WriteLine($"{layers[i].Name}:{layers[i].Checked}");
                    
                }
                mTest_ZoneMasks.Add(zone, layers);
            }
        }

        public int GetLayerIndex(CheckBox cb, string zoneName)
        {
            //var nameIndex = Array.IndexOf(GameUtil.GalaxyLayers, cb.Tag.ToString());

            var nameIndex =  -1;

            for (var t = 0; t< 16; t++) 
            {
                if (mTest_ZoneMasks[zoneName][t].Name == cb.Tag.ToString()) 
                {
                    nameIndex = t;
                    break;
                }
            }

            
            if (nameIndex < 0) throw new Exception("Layer NotFound");

            return nameIndex;
        }

        public void ChangeZoneMask(CheckBox cb, string zoneName)
        {
            var layerIndex = GetLayerIndex(cb, zoneName);
            var layer = GetLayer(zoneName, layerIndex);
            layer.Checked = cb.Checked;

            //Console.WriteLine($"{cb.Tag}:{cb.Checked}");
            Console.WriteLine($"{layer.Name}:{layer.Checked}");
            mTest_ZoneMasks[zoneName][layerIndex] = layer;
            //mZoneMasks[zoneName] = ;
        }

        public int GetZoneMaskInt(string zoneName)
        {
            int test = 0;

            for (int layer = 0; layer < mTest_ZoneMasks[zoneName].Length; layer++)
            {
                Console.WriteLine(zoneName+layer.ToString());
                if (mTest_ZoneMasks[zoneName][layer].Checked) 
                {

                    test += LayerNo[layer];
                    Console.WriteLine($"index: {layer}");
                }
                    

                if (layer == mTest_ZoneMasks[zoneName].Length-1) Console.WriteLine("t:"+layer.ToString());
            }
            Console.WriteLine(test);
            Console.WriteLine();
            return test;
        }

        public void SetZoneMask(string zoneName)
        {
            mEntry.Set(zoneName, GetZoneMaskInt(zoneName));
        }

        public Layer GetLayer(string zoneName, int layerIndex)
        {
            return mTest_ZoneMasks[zoneName][layerIndex];
        }

        public void RemoveZone(string zoneName)
        {
            mZoneMasks.Remove(zoneName);
        }

        public void SetZoneMask(string zoneName, int layerInt)
        {
            mZoneMasks[zoneName] = layerInt;
        }

        public static string[] Comets { get => GameUtil.IsSMG1() ? SMG1Comets : SMG2Comets; }
        public static string[] AppearStarObjs { get => GameUtil.IsSMG1() ? SMG1AppearPowerStarObj : SMG2AppearPowerStarObj; }
        public static string TimerNameFromGameVer { get => GameUtil.IsSMG1() ? "LuigiModeTimer:" : "CometTimer:"; }

        public BCSV.Entry mEntry { get; private set; }
        public int mScenarioNo { get; private set; }
        public string mScenarioName { get; private set; }
        public int mPowerStarID { get; private set; }
        public string mAppearPowerStar { get; private set; }
        public string mPowerStarType { get; private set; }
        public string mComet { get; private set; }
        public int mCometLimitTimer { get; private set; }
        public int mIsHidden { get; private set; }
        public int mLuigiModeTimer { get; private set; }

        public Dictionary<string, int> mZoneMasks { get; private set; }

        public Dictionary<string, Layer[]> mTest_ZoneMasks { get; private set; }

        public struct Layer 
        {
            public string Name;
            public bool Checked;
        }

        public static readonly int[] LayerNo = new int[]
        {
            0b_0000_0000_0000_0001,
            0b_0000_0000_0000_0010,
            0b_0000_0000_0000_0100,
            0b_0000_0000_0000_1000,
            0b_0000_0000_0001_0000,
            0b_0000_0000_0010_0000,
            0b_0000_0000_0100_0000,
            0b_0000_0000_1000_0000,
            0b_0000_0001_0000_0000,
            0b_0000_0010_0000_0000,
            0b_0000_0100_0000_0000,
            0b_0000_1000_0000_0000,
            0b_0001_0000_0000_0000,
            0b_0010_0000_0000_0000,
            0b_0100_0000_0000_0000,
            0b_1000_0000_0000_0000

        };

        

        /// <summary>
        /// SMG2で使用されるコメットの一覧
        /// </summary>
        private static readonly string[] SMG2Comets = new string[]
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
        private static readonly string[] SMG1Comets = new string[]
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

        private static readonly string[] SMG2AppearPowerStarObj = new string[]
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

        private static readonly string[] SMG1AppearPowerStarObj = new string[]
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
