using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.rnd;
using System.ComponentModel;
using OpenTK;
using OpenTK.Graphics;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Takochu.Properties;
using System.Resources;

namespace Takochu.util
{
    //public class LocalizedDescriptionAttribute : DescriptionAttribute
    //{
    //    private readonly ResourceManager _resourceManager;
    //    private  string _resourceKey;
        

    //    public LocalizedDescriptionAttribute(string resourceKey)
    //    {
    //        var trans = Settings.Default.Translation;
    //        _resourceManager = new ResourceManager(Translate.t[trans]);
    //        _resourceKey = resourceKey;
    //    }

    //    public override string Description
    //    {
    //        get
    //        {
    //            string description = _resourceManager.GetString(_resourceKey);
    //            return string.IsNullOrWhiteSpace(description) ? string.Format("[[{0}]]", _resourceKey) : description;
    //        }
    //    }

    //    public new string DescriptionValue 
    //    {
    //        set { _resourceKey = value; }
    //    }
    //}

    //public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    //{
    //    private readonly ResourceManager _resourceManager;
    //    private readonly string _resourceKey;

    //    /// <summary>
    //    /// ディスプレイネームを翻訳します。
    //    /// </summary>
    //    /// <param name="resourceKey">リソースファイルの変数名</param>
    //    /// <param name="resourceType">必要なしリソースファイルから自動取得</param>
    //    public LocalizedDisplayNameAttribute(string resourceKey)
    //    {
    //        var trans = Settings.Default.Translation;
    //        _resourceManager = new ResourceManager(Translate.t[trans]);
    //        _resourceKey = resourceKey;
    //    }

    //    public override string DisplayName
    //    {
    //        get
    //        {
    //            string displayName = _resourceManager.GetString(_resourceKey);
    //            return string.IsNullOrWhiteSpace(displayName) ? string.Format("[[{0}]]", _resourceKey) : displayName;
    //        }
    //    }
    //}

    //public class LocalizedCategoryAttribute : CategoryAttribute
    //{
    //    private readonly ResourceManager _resourceManager;
    //    private readonly string _resourceKey;

    //    public LocalizedCategoryAttribute(string resourceKey)
    //    {
    //        var trans = Settings.Default.Translation;
    //        _resourceManager = new ResourceManager(Translate.t[trans]);
    //        _resourceKey = resourceKey;
    //    }

    //    protected override string GetLocalizedString(string value)
    //    {
    //        string category = _resourceManager.GetString(_resourceKey);
    //        return string.IsNullOrWhiteSpace(category) ? string.Format("[[{0}]]", _resourceKey) : category;
    //    }
    //}

    //public class ObjectPropertyGridSettings
    //{
    //    private smg.obj.LevelObj _levelObj;
    //    public static DescriptionAttribute aaaaa = new DescriptionAttribute("");

        

        

    //    [ReadOnly(true)]
    //    [Category("\t\tName")]
    //    [LocalizedDisplayName("ObjectName")]
    //    [LocalizedDescription("ObjectNameDes")]
    //    public string ObjectName
    //    {
    //        get
    //        {
    //            return _levelObj.mEntry.Get("name").ToString();
    //        }
    //        set
    //        {
    //            _levelObj.mName = value;
    //            _levelObj.mEntry.Set("name", value);
    //        }
    //    }

    //    [Category("\t\tPositions")]
    //    [LocalizedDisplayName("pos_x")]
    //    [LocalizedDescription("pos_xDes")]
    //    public float PositionX
    //    {
    //        get
    //        {
    //            return (float)_levelObj.mEntry.Get("pos_x");
    //        }
    //        set
    //        {
    //            _levelObj.mTruePosition.X = value;
    //            _levelObj.mEntry.Set("pos_x", value);
    //        }
    //    }
        

    //    [Category("\t\tPositions")]
    //    [LocalizedDisplayName("pos_y")]
    //    [LocalizedDescription("pos_yDes")]
    //    public float PositionY
    //    {
    //        get
    //        {
    //            return (float)_levelObj.mEntry.Get("pos_y");
    //        }
    //        set
    //        {
    //            _levelObj.mTruePosition.Y = value;
    //            _levelObj.mEntry.Set("pos_y", value);
    //        }
    //    }
    //    [Category("\t\tPositions")]
    //    [LocalizedDisplayName("pos_z")]
    //    [LocalizedDescription("pos_zDes")]
    //    public float PositionZ
    //    {
    //        get
    //        {
    //            return (float)_levelObj.mEntry.Get("pos_z");
    //        }
    //        set
    //        {
    //            _levelObj.mTruePosition.Z = value;
    //            _levelObj.mEntry.Set("pos_z", value);
    //        }
    //    }
    //    public ObjectPropertyGridSettings(smg.obj.LevelObj levelObj) 
    //    {
    //        _levelObj = levelObj;


    //    }
    //    //public ObjectPropertyGridSettings(/*GLControl glc,*/ smg.obj.AbstractObj abstObj) 
    //    //{
    //    //    _abstObj = abstObj;
    //    //}
    //}
}
