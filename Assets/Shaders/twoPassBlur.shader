Shader "Custom/twoPassBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag_horiz
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _BlurSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag_horiz(v2f i) : SV_Target
            {
                fixed4 col = fixed4(0,0,0,0);
                float2 blurSize = _BlurSize / _ScreenParams.xy;

                // Horizontal blur
                for (int x = -3; x <= 3; x++)
                {
                    col += tex2D(_MainTex, i.uv + float2(x * blurSize.x, 0));
                }

                return col / 7.0;
            }

            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag_vert
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _BlurSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag_vert(v2f i) : SV_Target
            {
                fixed4 col = fixed4(0,0,0,0);
                float2 blurSize = _BlurSize / _ScreenParams.xy;

                // Vertical blur
                for (int y = -3; y <= 3; y++)
                {
                    col += tex2D(_MainTex, i.uv + float2(0, y * blurSize.y));
                }

                return col / 7.0;
            }
            ENDCG
        }
    }
}