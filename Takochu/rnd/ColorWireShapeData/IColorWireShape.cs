using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Takochu.rnd.ColorWireShapeData
{
    public interface IColorWireShape
    {
        void SetSize(Vector3 size);
        void SetFillColor(Vector4 fillColor);
        void SetColor(AreaType areaType);
        void Render(RenderInfo info);
    }
}
