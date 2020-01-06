Shader "Unlit/test"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _UVTex("UV Texture", 2D) = "white" {}

        _Color ("Blacklight Color", Color) = (1,1,1,1)
        
        _LightDirection("Light Direction", Vector) = (0,0,1,0)
		_LightPosition("Light Position", Vector) = (0,0,0,0)
		_LightAngle("Light Angle", Range(0,180)) = 45
		_StrengthScaling("Strength", Float) = 50

        _LightRange("Light Range", Float) = 1
        _LightFallOff("Light FallOff", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
        sampler2D _MainTex;
        sampler2D _UVTex;
        fixed4 _Color;
        float4 _LightPosition;
		float4 _LightDirection;
		float _LightAngle;
		float _StrengthScaling;
        float _LightRange;
        float _LightFallOff;

            float4 frag (v2f i) : SV_Target
            {
                float3 direction = normalize(_LightPosition - i.worldPos);
			    float scale = dot(direction, _LightDirection);
			    float strength = scale - cos(_LightAngle * (3.14 / 360.0));
                strength = clamp(strength * _StrengthScaling, 0, 1);
                // Albedo comes from a texture tinted by color
                float4 uvTex = tex2D (_UVTex, i.uv) * _Color;
                
                //Used to multiply uvText by strength

                float _Distance = distance(_LightPosition, i.worldPos);
                float uvMultiplier = 1 - smoothstep(_LightRange - _LightFallOff, _LightRange, _Distance); // 1
                uvMultiplier *= strength;
                //Used to be multiplied by step(0, strength)

                float4 mainTex = tex2D(_MainTex, i.uv);
                mainTex *= 1 - uvMultiplier; //cleared
                mainTex +=  uvTex * uvMultiplier; //added

                return  mainTex;
            }
            ENDCG
        }
    }
}
