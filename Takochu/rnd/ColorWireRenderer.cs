using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Takochu.rnd.ColorWireShapeData;

namespace Takochu.rnd
{
    public enum AreaType
    {
        Normal,
        Camera,
        Gravity
    }

    public enum ShapeID:int
    {
        Cube1 = 0,
        Cube2 = 1,
        Sphere = 2,
        Cylinder = 3,
        Bowl = 4
    }

    public class ColorWireRenderer : RendererBase
    {
        private readonly IColorWireShape _colorWireShape;
        private readonly AreaType _areaType;
        private readonly Dictionary<ShapeID, IColorWireShape> ShapeDictionary
            = new Dictionary<ShapeID, IColorWireShape>()
            {
                {ShapeID.Cube1 , new ColorWireCube() },
                {ShapeID.Cube2 , new ColorWireCube() },
                {ShapeID.Sphere , new ColorWireSphere() },
                {ShapeID.Cylinder , new ColorWireCylinder() },
                {ShapeID.Bowl , new ColorWireBowl() }
            };
        public ColorWireRenderer(AreaType areaType ,short areaShapeNo ,bool showAxes = false)
        {
            if (areaShapeNo < -1 || areaShapeNo > ShapeDictionary.Count) areaShapeNo = 0;
            _colorWireShape = ShapeDictionary[(ShapeID)areaShapeNo];
            _areaType = areaType;
            
            m_Size = new Vector3(500, 500, 500);
            m_BorderColor = new Vector4(1f, 1f, 1f, 1f);
            m_FillColor = new Vector4(1f, 1f, 1f, 1f);
            m_ShowAxes = showAxes;
            
        }

        public override bool GottaRender(RenderInfo info)
        {
            return info.Mode != RenderMode.Translucent;
        }

        public override void Render(RenderInfo info)
        {
            _colorWireShape.SetSize(m_Size);
            _colorWireShape.SetFillColor(m_FillColor);
            _colorWireShape.SetColor(_areaType);
            _colorWireShape.Render(info);
            
        }


        public Vector3 m_Size { get; private set; }
        private Vector4 m_BorderColor, m_FillColor;
        private bool m_ShowAxes;
    }
    
    
}
