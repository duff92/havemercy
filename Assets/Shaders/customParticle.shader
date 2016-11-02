// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Particle" {
	Properties{
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Particle Texture", 2D) = "white" {}
		_Size("Size", Range(0, 3)) = 0.5
	}
		Category{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			Blend SrcAlpha One
			AlphaTest Greater .01
			ColorMask RGB
			ZTest LEqual Cull Off Lighting Off ZWrite Off Fog{ Color(0,0,0,0) }
			BindChannels{
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}

		SubShader {
			Pass
			{
				CGPROGRAM
				#pragma target 5.0
				#pragma vertex vert
				#pragma fragment frag
				#pragma geometry GS_Main
//				#pragma fragmentoption ARB_precision_hint_fastest
	//			#pragma multi_compile_particles

				#include "UnityCG.cginc"

				sampler2D _MainTex;
				fixed4 _TintColor;
				float _Size;

				struct appdata_t {
					float4 position : POSITION;
					float4 normal : NORMAL;
					float2 texcoord : TEXCOORD0;
					uint instance_id : SV_InstanceID;
				};

				struct v2f {
					float4 pos : SV_POSITION;
					//fixed4 color : COLOR;
					float2 tex0 : TEXCOORD0;
				};

				struct GS_INPUT
				{
					float4	pos		: POSITION;
					float3	normal	: NORMAL;
					float2  tex0	: TEXCOORD0;
				};

				float4 _MainTex_ST;


				// Particle's data
				struct Particle
				{
					float3 position;
					float3 velocity;
				};


				// Particle's data, shared with the compute shader
				StructuredBuffer<Particle> particleBuffer;


				// Vertex shader
				GS_INPUT vert(appdata_t v)
				{
					GS_INPUT o;
					o.pos = mul(UNITY_MATRIX_MVP, float4(particleBuffer[v.instance_id].position, 1.0f));
					o.normal = v.normal;
					o.tex0 = float2(0,0);
					return o;
				}

				// Geometry Shader -----------------------------------------------------
				[maxvertexcount(4)]
				void GS_Main(point GS_INPUT p[1], inout TriangleStream<v2f> triStream)
				{
					float3 up = float3(0, 1, 0);
					float3 look = _WorldSpaceCameraPos - p[0].pos;
					look.y = 0;
					look = normalize(look);
					float3 right = cross(up, look);

					float halfS = 0.5f * _Size;

					float4 v[4];
					v[0] = float4(p[0].pos + halfS * right - halfS * up, 1.0f);
					v[1] = float4(p[0].pos + halfS * right + halfS * up, 1.0f);
					v[2] = float4(p[0].pos - halfS * right - halfS * up, 1.0f);
					v[3] = float4(p[0].pos - halfS * right + halfS * up, 1.0f);

					float4x4 vp = mul(UNITY_MATRIX_MVP, unity_WorldToObject);
					v2f pIn;
					pIn.pos = mul(vp, v[0]);
					pIn.tex0 = float2(1.0f, 0.0f);
					triStream.Append(pIn);

					pIn.pos = mul(vp, v[1]);
					pIn.tex0 = float2(1.0f, 1.0f);
					triStream.Append(pIn);

					pIn.pos = mul(vp, v[2]);
					pIn.tex0 = float2(0.0f, 0.0f);
					triStream.Append(pIn);

					pIn.pos = mul(vp, v[3]);
					pIn.tex0 = float2(0.0f, 1.0f);
					triStream.Append(pIn);
				}

				sampler2D _CameraDepthTexture;
				
				// Fragment shader
				float4 frag(v2f i) : COLOR
				{
					float4 retval = tex2D(_MainTex, i.tex0);
					retval[3] *= 0.5f;

					return retval;
				}

				ENDCG
			}
		}
		FallBack Off
	}
}