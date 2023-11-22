using UnityEngine;

namespace KE 
{

    public static class Math 
    {
        // returns a value that falls on a sine wave that uses the given parameters
        public static float SineWave(float theta, float amplitude, float baseline)
        {
            return (Mathf.Sin(theta) * amplitude) + baseline;
        }

        public static Vector2 GetPositionAroundCirlce(float angle, float radius, Vector2 center)
        {
            var x = (Mathf.Cos(Mathf.Deg2Rad * angle) * radius) + center.x;
            var y = (Mathf.Sin(Mathf.Deg2Rad * angle) * radius) + center.y;
            //Debug.DrawRay(center, new Vector2(x, y), Color.red, 20, false);
            return new Vector2(x, y);
        }

        // used to create a lerp that slows down in the middle, then speeds back up
        public static float InverseSmoothStep(float ratio)
        {
            return 4 * (ratio * ratio * ratio) - 6 * (ratio * ratio) + 3 * ratio;
        }

        // used to create a smooth in smooth out lerp
        public static float SmootherStep(float ratio)
        {
            return ratio * ratio * ratio * (ratio * (6f * ratio - 15f) + 10f);
        }

        // activation functions for neural networks
        
        public static double Sigmoid(double v)
        {
            double k = (double) System.Math.Exp(v);
            return k / (1.0f + k);
        }

        public static double TanH(double v)
        {
            return (2 * Sigmoid(2 * v) - 1);
        }

        public static double BinaryStep(double v)
        {
            return ((v < 0) ? 0 : 1);
        }

        public static double Relu(double v)
        {
            return ((v > 0) ? v : 0);
        }

        public static double LeakyRelu(double v)
        {
            return ((v > 0) ? (0.01 * v) : 0);
        }

        public static double Sinusoid(double v)
        {
            return Mathf.Sin((float)v);
        }

        public static double SoftSign(double v)
        {
            return v / (1 + Mathf.Abs((float)v));
        }
    }
}
