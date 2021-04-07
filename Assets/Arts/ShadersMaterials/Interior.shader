Shader "Show Insides" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" { }
    }
        SubShader{

        Pass {
            Material {
                Diffuse(1,1,1,1)
            }
            Lighting On
            SetTexture[_MainTex]
        }

        Pass {
            Color(0,0,0,1)
            Cull Front
        }
    }
}