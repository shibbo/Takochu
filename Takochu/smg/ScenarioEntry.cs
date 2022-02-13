using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Takochu.fmt;
using Takochu.util;

namespace Takochu.smg
{
    public class ScenarioEntry
    {
        private List<string> _zoneList;

        public ScenarioEntry(BCSV.Entry entry, List<string> zoneList)
        {
            Entry = entry;
            _zoneList = zoneList;

            ScenarioNo      = Entry.Get<int>("ScenarioNo");
            ScenarioName    = Entry.Get<string>("ScenarioName");
            PowerStarID     = Entry.Get<int>("PowerStarId");
            AppearStarObj   = Entry.Get<string>("AppearPowerStarObj");

            Console.WriteLine(AppearStarObj);

            SettingsEachGameVer();

            if (!Entry.ContainsKey("Comet"))
                Comet = "";
            else
                Comet = Entry.Get<string>("Comet");

            ZoneMasks = new Dictionary<string, Layer[]>();

            ReloadZoneMask();

        }

        private void SettingsEachGameVer() 
        {
            if (GameUtil.IsSMG2())
            {
                PowerStarType = Entry.Get<string>("PowerStarType");
                CometLimitTimer = Entry.Get<int>("CometLimitTimer");
            }
            else
            {
                IsHidden = Entry.Get<int>("IsHidden");
                if (!Entry.ContainsKey("LuigiModeTimer"))
                    LuigiModeTimer = default;
                else
                    LuigiModeTimer = Entry.Get<int>("LuigiModeTimer");
            }
        }

        public void ReloadZoneMask()
        {
            ZoneMasks = new Dictionary<string, Layer[]>();

            foreach (string zone in _zoneList)
            {
                int[] layerArray = { Entry.Get<int>(zone) };
                BitArray bitArray = new BitArray(layerArray);

                var layers = new Layer[16];

                for (int i = 0; i < 16; i++)
                {
                    layers[i].Name = GameUtil.GalaxyLayers[i];
                    layers[i].Checked = bitArray[i];
                }
                ZoneMasks.Add(zone, layers);
            }
        }

        public int GetLayerIndex(CheckBox cb, string zoneName)
        {
            var nameIndex =  -1;

            for (var layerIndex = 0; layerIndex< 16; layerIndex++) 
            {
                if (ZoneMasks[zoneName][layerIndex].Name == cb.Tag.ToString()) 
                {
                    nameIndex = layerIndex;
                    break;
                }
            }

            
            if (nameIndex < 0) throw new Exception("Layer NotFound");

            return nameIndex;
        }

        public void ChangeZoneMask(CheckBox cb, string zoneName)
        {
            var layerIndex  = GetLayerIndex(cb, zoneName);
            var layer       = GetLayer(zoneName, layerIndex);
            layer.Checked   = cb.Checked;

            ZoneMasks[zoneName][layerIndex] = layer;
        }

        public int GetZoneMaskInt(string zoneName)
        {
            int maskData = 0;

            for (int layer = 0; layer < ZoneMasks[zoneName].Length; layer++)
            {
                if (ZoneMasks[zoneName][layer].Checked) 
                {
                    maskData += LayerNo[layer];
                }
            }

            return maskData;
        }

        public void SetZoneMask(string zoneName)
        {
            Entry.Set(zoneName, GetZoneMaskInt(zoneName));
        }

        private Layer GetLayer(string zoneName, int layerIndex)
        {
            return ZoneMasks[zoneName][layerIndex];
        }

        public void RemoveZone(string zoneName)
        {
            ZoneMasks.Remove(zoneName);
        }

        public void ChangeShowScenario(BitArray bitArray) 
        {
            var powerStarID = 0;
            for (int i = 0; i < bitArray.Length; i++) 
            {
                if (bitArray[i] == true) 
                {
                    powerStarID += ShowScenarioNo[i];
                }
            }

            PowerStarID = powerStarID;
        }

        public void SetShowScenario(string zoneName) 
        {
            //Console.WriteLine( Entry.Get("PowerStarID"));
            Entry.Set("PowerStarId", PowerStarID);
        }

        public void ChangeScenarioName(string scenarioName) 
        {
            if (scenarioName.Length < 1) return;
            ScenarioName = scenarioName;
        }

        public void SetScenarioName() 
        {
            Entry.Set("ScenarioName", ScenarioName);
        }

        public void ChangeAppearStarObj(string selectedItemName) 
        {
            AppearStarObj = selectedItemName;
        }

        public void SetAppearStarObj() 
        {
            Entry.Set("AppearPowerStarObj", AppearStarObj);
        }

        public void ChangePowerStarType(string selectedItemName) 
        {
            PowerStarType = selectedItemName;
        }

        public void SetPowerStarType() 
        {
            Entry.Set("PowerStarType", PowerStarType);
        }

        public void ChangeComet(string selectedItemName) 
        {
            Comet = selectedItemName;
        }

        public void SetComet() 
        {
            Entry.Set("Comet", Comet);
        }

        public void ChangeTimer(int time) 
        {
            var returnTime = time;

            if (time < 0) returnTime = 0;
            if (time > int.MaxValue) returnTime = int.MaxValue;

            if (GameUtil.IsSMG1())
            {
                LuigiModeTimer = returnTime;
            }
            else
            {
                CometLimitTimer = returnTime;
            }
        }

        public void SetTimer() 
        {
            if (GameUtil.IsSMG1())
            {
                Entry.Set("LuigiModeTimer", LuigiModeTimer);
            }
            else
            {
                Entry.Set("CometLimitTimer", CometLimitTimer);
            }
        }

        public void ChangeIsHidden(bool isHidden) 
        {
            IsHidden = IsHidden;
        }

        public void SetIsHidden() 
        {
            Entry.Set("IsHidden", IsHidden);
        }

        public static string[] Comets { get => GameUtil.IsSMG1() ? SMG1Comets : SMG2Comets; }
        public static string[] AppearStarObjs { get => GameUtil.IsSMG1() ? SMG1AppearPowerStarObj : SMG2AppearPowerStarObj; }
        public static string TimerNameFromGameVer { get => GameUtil.IsSMG1() ? "LuigiModeTimer:" : "CometTimer:"; }

        public BCSV.Entry Entry { get; private set; }
        public int ScenarioNo { get; private set; }
        public string ScenarioName { get; private set; }
        public int PowerStarID { get; private set; }
        public string AppearStarObj { get; private set; }
        public string PowerStarType { get; private set; }
        public string Comet { get; private set; }
        public int CometLimitTimer { get; private set; }
        public int IsHidden { get; private set; }
        public int LuigiModeTimer { get; private set; }

        public Dictionary<string, Layer[]> ZoneMasks { get; private set; }

        public struct Layer 
        {
            public string Name;
            public bool Checked;
        }

        public static readonly int[] ShowScenarioNo =
        {
            0b0000_0000_0000_0001,
            0b0000_0000_0000_0010,
            0b0000_0000_0000_0100,
            0b0000_0000_0000_1000,
            0b0000_0000_0001_0000,
            0b0000_0000_0010_0000,
            0b0000_0000_0100_0000,
            0b0000_0000_1000_0000
        };

        /// <summary>
        /// LayerA～LayerPまでのビットフラグ
        /// </summary>
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
