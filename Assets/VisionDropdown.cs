using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static ObjectDropdown;

public class VisionDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public static Vector3 RGBToLMS(Vector3 RGBVal) // From https://arxiv.org/pdf/1711.10662.pdf
    {
        float R = RGBVal[0];
        float G = RGBVal[1];
        float B = RGBVal[2];

        float L = 17.8824f*R + 43.5161f*G + 4.1194f*B;
        float M = 3.4557f*R + 27.1554f*G + 3.8671f*B;
        float S = 0.0300f*R + 0.1843f*G + 1.4671f*B;

        Vector3 LMS = new Vector3(L, M, S);
        return LMS;
    }

    public static Vector3 LMSToRGB(Vector3 LMSVal)
    {
        float L = LMSVal[0];
        float M = LMSVal[1];
        float S = LMSVal[2];

        float R = 0.0809f*L - 0.1305f*M + 0.1167f*S;
        float G = -0.0102f*L + 0.0540f*M - 0.1136f*S;
        float B = -0.0004f*L - 0.0041f*M + 0.6935f*S;

        Vector3 RGB = new Vector3(R, G, B);

        return RGB;
    }

    public static Vector3 RGBToXYZ(Vector3 RGB)
    {
        float R = RGB[0];
        float G = RGB[1];
        float B = RGB[2];

        float X = 0.4124f*R + 0.3576f*G + 0.1804f*B;
        float Y = 0.2126f*R + 0.7152f*G + 0.0722f*B;
        float Z = 0.0193f*R + 0.1192f*G + 0.9503f*B;

        Vector3 XYZ = new Vector3(X, Y, Z);
        return XYZ;
    }

    public static Vector3 XYZToLMS(Vector3 XYZ)
    {
        float X = XYZ[0];
        float Y = XYZ[1];
        float Z = XYZ[2];

        float L = 0.4002f*X + 0.7076f*Y -0.0808f*X;
        float M = -0.2263f*X + 1.1653f*Y + 0.0457f*Z;
        float S = 0.9182f*Z;
        Vector3 LMS = new Vector3(L, M, S);
        return LMS;
    }

    public static Vector3 ProtanopiaSimulator(Vector3 LMSVal)
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

        Vector3 ProtanopiaVec = new Vector3(Lp, Mp, Sp);
        return ProtanopiaVec;
    }

    public static Vector3 DeuteranopiaSimulator(Vector3 LMSVal)
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

        Vector3 DeuteranopiaVec = new Vector3(Ld, Md, Sd);
        return DeuteranopiaVec;
    }

    public static Vector3 TritanopiaSimulator(Vector3 LMSVal)
    {// from http://mkweb.bcgsc.ca/colorblind/math.mhtml
        float L = LMSVal[0];
        float M = LMSVal[1];
        float S = LMSVal[2];

        float Lt = L;
        float Mt = M;
        float St = -0.8674f*L + 1.8673f*M;
         //-0.395913f*L + 0.801109f*M;

        Vector3 TritanopiaVec = new Vector3(Math.Max(Lt,0f), Math.Max(Mt,0f), Math.Max(St,0f));
        return TritanopiaVec;
    }

    public static Vector3 MonochromacySimulator(Vector3 RGBVal)
    {
        float gray = RGBVal[0]*0.2126f + RGBVal[1]*0.7152f + RGBVal[2]*0.0722f;
        Vector3 GrayscaleVec = new Vector3(gray, gray, gray);
        return GrayscaleVec;
    }


}
