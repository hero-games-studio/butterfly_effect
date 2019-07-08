Shader "Hero/BendWorld"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MainColor("Color",Color)=(1,1,1,1)
		_Curvature("Curvature",Float) = 0.001
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
	#pragma surface surf Lambert vertex:vert addshadow

			float4 _MainColor;
			uniform sampler2D _MainTex;
			uniform float _Curvature;

			struct Input 
			{
				float uv_MainTex;
			};

			void vert(inout appdata_full v) 
			{
				float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
				worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
				worldSpace = float4 (0.0f, (worldSpace.z * worldSpace.z) * -_Curvature, 0.0f, 0.0f);

				v.vertex += mul(unity_WorldToObject, worldSpace);
			}

			void surf(Input IN, inout SurfaceOutput o)
			{
				half4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = _MainColor;
				o.Alpha = c.a;
			}


		ENDCG
	}
			FallBack "Mobile/Diffuse"
}
