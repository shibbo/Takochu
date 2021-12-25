using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takochu.rnd.BmdRendererArrays
{
    public class ShaderTxt
    {
        public static readonly string[] GenSrc = {
            "normalize(gl_Vertex)",
            "vec4(gl_Normal,1.0)",
            "argh",
            "argh",
            "gl_MultiTexCoord0",
            "gl_MultiTexCoord1",
            "gl_MultiTexCoord2",
            "gl_MultiTexCoord3",
            "gl_MultiTexCoord4",
            "gl_MultiTexCoord5",
            "gl_MultiTexCoord6",
            "gl_MultiTexCoord7"
        };
        public static readonly string[] outputregs = {
            "rprev",
            "r0",
            "r1",
            "r2"
        };
        public static readonly string[] c_inputregs = {
            "truncc3(rprev.rgb)",
            "truncc3(rprev.aaa)",
            "truncc3(r0.rgb)",
            "truncc3(r0.aaa)",
            "truncc3(r1.rgb)",
            "truncc3(r1.aaa)",
            "truncc3(r2.rgb)",
            "truncc3(r2.aaa)",
            "texcolor.rgb",
            "texcolor.aaa",
            "rascolor.rgb",
            "rascolor.aaa",
            "vec3(1.0,1.0,1.0)",
            "vec3(0.5,0.5,0.5)",
            "konst.rgb",
            "vec3(0.0,0.0,0.0)"
        };
        public static readonly string[] c_inputregsD = {
            "rprev.rgb",
            "rprev.aaa",
            "r0.rgb",
            "r0.aaa",
            "r1.rgb",
            "r1.aaa",
            "r2.rgb",
            "r2.aaa",
            "texcolor.rgb",
            "texcolor.aaa",
            "rascolor.rgb",
            "rascolor.aaa",
            "vec3(1.0,1.0,1.0)",
            "vec3(0.5,0.5,0.5)",
            "konst.rgb",
            "vec3(0.0,0.0,0.0)"
        };
        public static readonly string[] c_konstsel = {
            "vec3(1.0,1.0,1.0)",
            "vec3(0.875,0.875,0.875)",
            "vec3(0.75,0.75,0.75)",
            "vec3(0.625,0.625,0.625)",
            "vec3(0.5,0.5,0.5)",
            "vec3(0.375,0.375,0.375)",
            "vec3(0.25,0.25,0.25)",
            "vec3(0.125,0.125,0.125)",
            "",
            "",
            "",
            "",
            "k0.rgb",
            "k1.rgb",
            "k2.rgb",
            "k3.rgb",
            "k0.rrr",
            "k1.rrr",
            "k2.rrr",
            "k3.rrr",
            "k0.ggg",
            "k1.ggg",
            "k2.ggg",
            "k3.ggg",
            "k0.bbb",
            "k1.bbb",
            "k2.bbb",
            "k3.bbb",
            "k0.aaa",
            "k1.aaa",
            "k2.aaa",
            "k3.aaa"
        };

    }
}
