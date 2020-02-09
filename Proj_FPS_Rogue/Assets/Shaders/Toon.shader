Shader "Custom/Toon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}	
		// Add as a new property.
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        
        // Add as new properties.			
        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32
        
        // Add as new properties.
        [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        
        // Add as a new property.
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
        
        

	}
	SubShader
	{
		Pass
		{
		
		    Tags
            {
                "LightMode" = "UniversalForward"
                "PassFlags" = "OnlyDirectional"
            }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			
			#include "UnityCG.cginc"
			// Add below the existing #include "UnityCG.cginc"
            #include "Lighting.cginc"
            // As a new include, below the existing ones.
            #include "AutoLight.cginc"

			struct appdata
			{
			    // Inside the appdata struct.
                float3 normal : NORMAL;
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
			};

			struct v2f
			{
			    // Inside the v2f struct.
                float3 worldNormal : NORMAL;
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				// Add to the v2f struct.
                float3 viewDir : TEXCOORD1;
                
                // Add to the v2f struct.
                SHADOW_COORDS(2)
			};
            
            CBUFFER_START(UnityPerMaterial)

            float _Glossiness;
            float _RimAmount;
            // Matching variable.
            float _RimThreshold;
			float4 _MainTex_ST;
			// Matching variables.
            float4 _SpecularColor;
            
            // Matching variables.
            float4 _RimColor;
            			
			float4 _Color;
            // Matching variable, add above the fragment shader.
            float4 _AmbientColor;

            CBUFFER_END
            
			sampler2D _MainTex;


			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				// Add to the vertex shader.
                o.viewDir = WorldSpaceViewDir(v.vertex);
                
                // Add to the vertex shader.
                TRANSFER_SHADOW(o)
				return o;
			}


            
			float4 frag (v2f i) : SV_Target
			{
			    
			    // At the top of the fragment shader.
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                float2 uv = float2(1 - (NdotL * 0.5 + 0.5), 0.5);

                // In the fragment shader, above the existing lightIntensity declaration.
                float shadow = SHADOW_ATTENUATION(i);
                
                // Below the NdotL declaration.
                //float lightIntensity = NdotL > 0 ? 1 : 0;
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);

                // Add below the lightIntensity declaration.
                float4 light = lightIntensity * _LightColor0;
                
                // Add to the fragment shader, above the line sampling _MainTex.
                float3 viewDir = normalize(i.viewDir);
                
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);
                
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                
                // Add below the specularIntensity declaration.
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;
                
                // In the fragment shader, below the line declaring specular.
                float4 rimDot = 1 - dot(viewDir, normal);
                
                // Add below the line declaring rimDot.
                // Add above the existing rimIntensity declaration, replacing it.
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                //float rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot);
                float4 rim = rimIntensity * _RimColor;
                
				float4 sample = tex2D(_MainTex, i.uv);

				return _Color * sample * (_AmbientColor + light + specular + rim);
			}
			ENDCG
		}
		// Insert just after the closing curly brace of the existing Pass.
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}