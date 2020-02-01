Shader "Custom/VertexColoredShader"
{
SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Standard
      #pragma vertex vert
      struct Input {
          float4 color : COLOR;
          float3 vertexColor;
      };
    struct v2f {
       float4 pos : SV_POSITION;
       fixed4 color : COLOR;
     };

         void vert (inout appdata_full v, out Input o)
         {
             UNITY_INITIALIZE_OUTPUT(Input,o);
             o.vertexColor = v.color; // Save the Vertex Color in the Input for the surf() method
         }

      void surf (Input IN, inout SurfaceOutputStandard o) {
          o.Albedo = IN.vertexColor * 0.8;
          o.Emission = IN.vertexColor * 0.3;
      }
      ENDCG
    }
    Fallback "Diffuse"
}
