Shader "GUI/ReverseFont" {
    Properties{
       _MainTex("Font Texture", 2D) = "white" {}
       _Color("Text Color", Color) = (0,0,0,1)
    }

        SubShader{
           Tags { "Queue" = "Transparent"}
           //Lighting Off Cull Off ZWrite Off Fog { Mode Off }

           Pass {
              Color[_Color]
              AlphaTest Greater 0.5
              Blend SrcColor DstColor
              BlendOp LogicalInvert
              SetTexture[_MainTex] {
                 combine previous, texture
              }
           }
       }
}