Shader "Custom/UI/BarTwoColor"
{
    Properties
    {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("_Color", Color) = (1,1,1,1)
		_ColorBG ("_ColorBG", Color) = (1,1,1,1)
        _Percent ("_Percent", Range(0.0, 1.0)) = 1.0
        _S ("_S", Range(0.0, 1.0)) = 1.0
        _Light ("_Light", Range(0.0, 1.0)) = 1.0
     }

    SubShader
    {
        LOD 200

        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Pass
        {
            Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
			half4 _Color;
			half4 _ColorBG;
            float _Percent;
            float _S;
            float _Light;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            half4 frag (v2f IN) : COLOR
            {
				float4 c = tex2D(_MainTex, IN.texcoord);
                c.rgb = c.r * .11 + c.g * .33 + c.b * .66;
				c.rgb /= _Light;
				half o = _S - _S * IN.texcoord.x;
				if (IN.texcoord.x > _Percent + _S - o)
					return c * _ColorBG;
				if (IN.texcoord.x > _Percent - o)
                {
					half p = (IN.texcoord.x - _Percent + o) / _S;
					c *= _ColorBG * p + _Color * (1 - p);
					return c;
				}
				return c * _Color;
            }
            ENDCG
        }
    }
}