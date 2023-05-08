Shader "Unlit/AppleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ChosenMethod("ChosenMethod", Int) = 0
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _ChosenMethod;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 RGBToLMS(float4 RGBVal)
            {
                float R = RGBVal.r;
                float G = RGBVal.g;
                float B = RGBVal.b;

                float L = 17.8824f*R + 43.5161f*G + 4.1194f*B;
                float M = 3.4557f*R + 27.1554f*G + 3.8671f*B;
                float S = 0.0300f*R + 0.1843f*G + 1.4671f*B;

                float4 LMS = float4(L, M, S, 1);
                return LMS;
            }

            float4 LMSToRGB(float4 LMSVal)
            {
                float L = LMSVal[0];
                float M = LMSVal[1];
                float S = LMSVal[2];

                float R = 0.0809f*L - 0.1305f*M + 0.1167f*S;
                float G = -0.0102f*L + 0.0540f*M - 0.1136f*S;
                float B = -0.0004f*L - 0.0041f*M + 0.6935f*S;

                float4 RGB = float4(R, G, B, 1);

                return RGB;
            }


            float4 RGBToXYZ(float4 RGBVal)
            {
                float R = RGBVal.r;
                float G = RGBVal.g;
                float B = RGBVal.b;

                float X = 0.4124f*R + 0.3576f*G + 0.1804f*B;
                float Y = 0.2126f*R + 0.7152f*G + 0.0722f*B;
                float Z = 0.0193f*R + 0.1192f*G + 0.9503f*B;

                float4 XYZ = float4(X, Y, Z, 1);
                return XYZ;
            }


            float4 XYZToLMS(float4 XYZVal)
            {
                float X = XYZVal[0];
                float Y = XYZVal[1];
                float Z = XYZVal[2];

                float L = 0.4002f*X + 0.7076f*Y -0.0808f*X;
                float M = -0.2263f*X + 1.1653f*Y + 0.0457f*Z;
                float S = 0.9182f*Z;
                float4 LMS = float4(L, M, S, 1);
                return LMS;
            }

            float4 LMSToXYZ(float4 LMSVal)
            {
                float X = LMSVal[0]*1.9102f - 1.1121f*LMSVal[1] + 0.2019f*LMSVal[2];
                float Y = 0.3710f*LMSVal[0] + 0.6291f*LMSVal[1];
                float Z = LMSVal[2];
                float4 XYZ = float4(X, Y, Z, 1);
                return XYZ;
            }

            float4 XYZToRGB(float4 XYZVal)
            {
                float R = 3.2404f*XYZVal[0] - 1.5371f*XYZVal[1] - 0.4985f*XYZVal[2];
                float G = -0.9693f*XYZVal[0] + 1.8760f*XYZVal[1] + 0.0416f*XYZVal[2];
                float B = 0.0556f*XYZVal[0] - 0.2040f*XYZVal[1] + 1.0572f*XYZVal[2];
                float4 RGB = float4(R, G, B, 1);
                return RGB;
            }

            float4 ProptanopiaSimulator(float4 LMSVal)
            {
                float L = LMSVal[0];
                float M = LMSVal[1];
                float S = LMSVal[2];

                float Lp = 2.0234f*M -2.5258f*S;
                float Mp = M;
                float Sp = S;

                if (Sp < 0)
                {
                    Sp = 0;
                }

                float4 ProtanopiaVec = float4(Lp, Mp, Sp, 1);
                return ProtanopiaVec;
            }

            float4 DeuteranopiaSimulator(float4 LMSVal)
            {
                float L = LMSVal[0];
                float M = LMSVal[1];
                float S = LMSVal[2];

                float Ld = L;
                float Md = 0.4942f*L + 1.2483f*S;
                float Sd = S;

                if (Sd < 0)
                {
                    Sd = 0;
                }

                float4 DeuteranopiaVec = float4(Ld, Md, Sd, 1);
                return DeuteranopiaVec;
            }

            float4 TritanopiaSimulator(float4 LMSVal)
            {
                float L = LMSVal[0];
                float M = LMSVal[1];
                float S = LMSVal[2];

                float Lt = L;
                float Mt = M;
                float St = -0.8674f*L + 1.8673f*M;
                //-0.395913f*L + 0.801109f*M;

                float4 TritanopiaVec = float4(Lt, Mt, St, 1);
                return TritanopiaVec;
            }

            float4 MonochromacySimulator(float4 RGBVal)
            {
                float gray = RGBVal.r*0.2126f + RGBVal.g*0.7152f + RGBVal.b*0.0722f;
                float4 GrayscaleVec = float4(gray, gray, gray, 1);
                return GrayscaleVec;
            }




            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
              

                if (_ChosenMethod == 1) // Deuteranopia color assignment
                {
                    float4 deuteranopiaPxl = DeuteranopiaSimulator(RGBToLMS(col));
                    float4 deuteranopiaPxlRGB = LMSToRGB(deuteranopiaPxl);
                    col = deuteranopiaPxlRGB;
                }

                else if (_ChosenMethod == 2) // Monochromacy color assignment
                {
                    col = MonochromacySimulator(col);
                }
                

                else if (_ChosenMethod == 3) // Proptanopia color assignment
                {
                    float4 proptanopiaPxl = ProptanopiaSimulator(RGBToLMS(col));
                    float4 proptanopiaPxlRGB = LMSToRGB(proptanopiaPxl);
                    col = proptanopiaPxlRGB;
                }

                else if (_ChosenMethod == 4) // Tritanopia color assignment
                {
                    float4 tritanopiaPxl = TritanopiaSimulator(XYZToLMS(RGBToXYZ(col))); 
                    float4 tritanopiaPxlRGB = XYZToRGB(LMSToXYZ(tritanopiaPxl));
                    col = tritanopiaPxlRGB;
                }

                return col;
            }
            ENDCG
        }
    }
}
