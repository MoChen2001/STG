Shader "MyShaders/Sky"
{
    Properties
    {
        _MainTex("Main Texture",2D) = "white"{}
        _Color("Color",Color) = (1,1,1,1)
        _Speed("Speed",Range(0,10)) = 2
    }
    SubShader
    {
        Pass
        {
            Cull Off

            CGPROGRAM
            
            #include "UnityCG.cginc"
            
            #pragma vertex vert
            #pragma fragment frag

            struct a2v
            {
                fixed4 vertex : POSITION;
                fixed2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                fixed4 pos : SV_POSITION;
                fixed2 uv : TEXCOORD0;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            fixed4 _MainTex_ST;
            fixed _Speed;

            v2f vert(a2v v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv =  TRANSFORM_TEX(v.texcoord,_MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                i.uv += fixed2(_Time.x,0.0) * _Speed;
                fixed4 tex = tex2D(_MainTex,i.uv);
                return _Color + tex;
            }

            ENDCG
        }
        
    }
}
