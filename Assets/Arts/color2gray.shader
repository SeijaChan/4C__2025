Shader "color2gray"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _sdfTex ("sdf", 2D) = "white" {}
        _gray ("灰度", Range(-0.05,1.05) ) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #define NUM_LAYERS 4.

            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uvposss:TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _sdfTex;
            float _gray;
            //float _randomsize;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uvposss=ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 a = float2(0.5, 0.5);
                float distances=distance(a,i.uv);

                float3 maintex=tex2D(_MainTex,i.uv);
                float3 Gray=0.299*maintex.r+0.578*maintex.r+0.114*maintex.r;
                float sdf=tex2D(_sdfTex,float2(i.uvposss.xy ));
                //return 
                //float4(lerp(Gray,maintex,_gray),1);
                //return smoothstep(0.4,0.5,distances*_gray);
                //return float4(lerp(Gray,maintex,smoothstep(0,0.3,(1-distances*20*_gray))),1);
                //return float4(lerp(Gray,maintex,smoothstep(0.4,0.5,distances*_gray)),1);
                //最终效果
                //return float4(lerp(Gray,maintex,step(sdf,_gray)),1);
                //测试sdf
                return 1-step(sdf,_gray);
            }
            ENDCG
        }
    }
}
