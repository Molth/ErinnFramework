//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Color Type
    /// </summary>
    public readonly struct ColorType
    {
        public static readonly Color AntiqueWhite = new(0.98039216f, 0.92156863f, 0.84313726f, 1f);
        public static readonly Color Aquamarine = new(0.49803922f, 1f, 0.83137256f, 1f);
        public static readonly Color Beige = new(0.96078431f, 0.96078431f, 0.8627451f, 1f);
        public static readonly Color Black = new(0f, 0f, 0f, 1f);
        public static readonly Color Blue = new(0f, 0f, 1f, 1f);
        public static readonly Color Brown = new(0.64705882f, 0.16470588f, 0.16470588f, 1f);
        public static readonly Color BurlyWood = new(0.87058824f, 0.72156863f, 0.52941176f, 1f);
        public static readonly Color CadetBlue = new(0.37254902f, 0.61960784f, 0.62745098f, 1f);
        public static readonly Color Chartreuse = new(0.49803922f, 1f, 0f, 1f);
        public static readonly Color Chocolate = new(0.82352941f, 0.41176471f, 0.11764706f, 1f);
        public static readonly Color Coral = new(1f, 0.49803922f, 0.31372549f, 1f);
        public static readonly Color CornflowerBlue = new(0.39215686f, 0.58431373f, 0.92941176f, 1f);
        public static readonly Color Crimson = new(0.8627451f, 0.07843137f, 0.23529412f, 1f);
        public static readonly Color DarkBlue = new(0f, 0f, 0.54509804f, 1f);
        public static readonly Color DarkGoldenrod = new(0.72156863f, 0.5254902f, 0.04313725f, 1f);
        public static readonly Color DarkGray = new(0.6627451f, 0.6627451f, 0.6627451f, 1f);
        public static readonly Color DarkGreen = new(0f, 0.39215686f, 0f, 1f);
        public static readonly Color DarkKhaki = new(0.74117647f, 0.71764706f, 0.41960784f, 1f);
        public static readonly Color DarkOliveGreen = new(0.33333333f, 0.41960784f, 0.18431373f, 1f);
        public static readonly Color DarkOrange = new(1f, 0.54901961f, 0f, 1f);
        public static readonly Color DarkOrchid = new(0.6f, 0.19607843f, 0.8f, 1f);
        public static readonly Color DarkPink = new(1f, 0.07843137f, 0.57647059f, 1f);
        public static readonly Color DarkPurple = new(0.4f, 0f, 0.4f, 1f);
        public static readonly Color DarkRed = new(0.54509804f, 0f, 0f, 1f);
        public static readonly Color DarkSalmon = new(0.91372549f, 0.58823529f, 0.47843137f, 1f);
        public static readonly Color DarkSeaGreen = new(0.56078431f, 0.7372549f, 0.56078431f, 1f);
        public static readonly Color DarkSlateBlue = new(0.28235294f, 0.23921569f, 0.54509804f, 1f);
        public static readonly Color DarkSlateGray = new(0.18431373f, 0.30980392f, 0.30980392f, 1f);
        public static readonly Color DeepPink = new(1f, 0.07843137f, 0.57647059f, 1f);
        public static readonly Color DeepSkyBlue = new(0f, 0.74901961f, 1f, 1f);
        public static readonly Color DimGray = new(0.41176471f, 0.41176471f, 0.41176471f, 1f);
        public static readonly Color DodgerBlue = new(0.11764706f, 0.56470588f, 1f, 1f);
        public static readonly Color Firebrick = new(0.69803922f, 0.13333333f, 0.13333333f, 1f);
        public static readonly Color FloralWhite = new(1f, 0.98039216f, 0.94117647f, 1f);
        public static readonly Color ForestGreen = new(0.13333333f, 0.54509804f, 0.13333333f, 1f);
        public static readonly Color Gainsboro = new(0.8627451f, 0.8627451f, 0.8627451f, 1f);
        public static readonly Color GhostWhite = new(0.97254902f, 0.97254902f, 1f, 1f);
        public static readonly Color Gold = new(1f, 0.84313726f, 0f, 1f);
        public static readonly Color Goldenrod = new(0.85490196f, 0.64705882f, 0.1254902f, 1f);
        public static readonly Color Gray = new(0.50196078f, 0.50196078f, 0.50196078f, 1f);
        public static readonly Color Green = new(0f, 0.50196078f, 0f, 1f);
        public static readonly Color Honeydew = new(0.94117647f, 1f, 0.94117647f, 1f);
        public static readonly Color HotPink = new(1f, 0.41176471f, 0.70588235f, 1f);
        public static readonly Color IndianRed = new(0.80392157f, 0.36078431f, 0.36078431f, 1f);
        public static readonly Color Indigo = new(0.29411765f, 0f, 0.50980392f, 1f);
        public static readonly Color Ivory = new(1f, 1f, 0.94117647f, 1f);
        public static readonly Color Khaki = new(0.94117647f, 0.90196078f, 0.54901961f, 1f);
        public static readonly Color Lavender = new(0.90196078f, 0.90196078f, 0.98039216f, 1f);
        public static readonly Color LavenderBlush = new(1f, 0.94117647f, 0.96078431f, 1f);
        public static readonly Color LawnGreen = new(0.48627451f, 0.98823529f, 0f, 1f);
        public static readonly Color LemonChiffon = new(1f, 0.98039216f, 0.80392157f, 1f);
        public static readonly Color LightBlue = new(0.67843137f, 0.84705882f, 0.90196078f, 1f);
        public static readonly Color LightCoral = new(0.94117647f, 0.50196078f, 0.50196078f, 1f);
        public static readonly Color LightCyan = new(0.87843137f, 1f, 1f, 1f);
        public static readonly Color LightGoldenrodYellow = new(0.98039216f, 0.98039216f, 0.82352941f, 1f);
        public static readonly Color LightGray = new(0.82745098f, 0.82745098f, 0.82745098f, 1f);
        public static readonly Color LightGreen = new(0.56470588f, 0.93333333f, 0.56470588f, 1f);
        public static readonly Color LightPink = new(1f, 0.71372549f, 0.75686275f, 1f);
        public static readonly Color LightSalmon = new(1f, 0.62745098f, 0.47843137f, 1f);
        public static readonly Color LightSeaGreen = new(0.1254902f, 0.69803922f, 0.66666667f, 1f);
        public static readonly Color LightSkyBlue = new(0.52941176f, 0.80784314f, 0.98039216f, 1f);
        public static readonly Color LightSlateGray = new(0.46666667f, 0.53333333f, 0.6f, 1f);
        public static readonly Color LightSteelBlue = new(0.69019608f, 0.76862745f, 0.87058824f, 1f);
        public static readonly Color LightYellow = new(1f, 1f, 0.87843137f, 1f);
        public static readonly Color Lime = new(0f, 1f, 0f, 1f);
        public static readonly Color LimeGreen = new(0.19607843f, 0.80392157f, 0.19607843f, 1f);
        public static readonly Color Linen = new(0.98039216f, 0.94117647f, 0.90196078f, 1f);
        public static readonly Color Magenta = new(1f, 0f, 1f, 1f);
        public static readonly Color Maroon = new(0.50196078f, 0f, 0f, 1f);
        public static readonly Color MediumAquamarine = new(0.4f, 0.80392157f, 0.66666667f, 1f);
        public static readonly Color MediumBlue = new(0f, 0f, 0.80392157f, 1f);
        public static readonly Color MediumOrchid = new(0.72941176f, 0.33333333f, 0.82745098f, 1f);
        public static readonly Color MediumPurple = new(0.57647059f, 0.43921569f, 0.85882353f, 1f);
        public static readonly Color MediumSeaGreen = new(0.23529412f, 0.70196078f, 0.44313725f, 1f);
        public static readonly Color MediumSlateBlue = new(0.48235294f, 0.40784314f, 0.93333333f, 1f);
        public static readonly Color MediumSpringGreen = new(0f, 0.98039216f, 0.60392157f, 1f);
        public static readonly Color MediumTurquoise = new(0.28235294f, 0.81960784f, 0.8f, 1f);
        public static readonly Color MediumVioletRed = new(0.78039216f, 0.08235294f, 0.52156863f, 1f);
        public static readonly Color MidnightBlue = new(0.09803922f, 0.09803922f, 0.43921569f, 1f);
        public static readonly Color MintCream = new(0.96078431f, 1f, 0.98039216f, 1f);
        public static readonly Color MistyRose = new(1f, 0.89411765f, 0.88235294f, 1f);
        public static readonly Color Moccasin = new(1f, 0.89411765f, 0.70980392f, 1f);
        public static readonly Color NavajoWhite = new(1f, 0.87058824f, 0.67843137f, 1f);
        public static readonly Color Navy = new(0f, 0f, 0.50196078f, 1f);
        public static readonly Color OldLace = new(0.99215686f, 0.96078431f, 0.90196078f, 1f);
        public static readonly Color Olive = new(0.50196078f, 0.50196078f, 0f, 1f);
        public static readonly Color OliveDrab = new(0.41960784f, 0.55686275f, 0.1372549f, 1f);
        public static readonly Color Orange = new(1f, 0.64705882f, 0f, 1f);
        public static readonly Color OrangeRed = new(1f, 0.27058824f, 0f, 1f);
        public static readonly Color Orchid = new(0.85490196f, 0.43921569f, 0.83921569f, 1f);
        public static readonly Color PaleGoldenrod = new(0.93333333f, 0.90980392f, 0.66666667f, 1f);
        public static readonly Color PaleGreen = new(0.59607843f, 0.98431373f, 0.59607843f, 1f);
        public static readonly Color PaleTurquoise = new(0.68627451f, 0.93333333f, 0.93333333f, 1f);
        public static readonly Color PaleVioletRed = new(0.85882353f, 0.43921569f, 0.57647059f, 1f);
        public static readonly Color PapayaWhip = new(1f, 0.9372549f, 0.83529412f, 1f);
        public static readonly Color PeachPuff = new(1f, 0.85490196f, 0.7254902f, 1f);
        public static readonly Color Peru = new(0.80392157f, 0.52156863f, 0.24705882f, 1f);
        public static readonly Color Pink = new(1f, 0.75294118f, 0.79607843f, 1f);
        public static readonly Color Plum = new(0.86666667f, 0.62745098f, 0.86666667f, 1f);
        public static readonly Color PowderBlue = new(0.69019608f, 0.87843137f, 0.90196078f, 1f);
        public static readonly Color Purple = new(0.50196078f, 0f, 0.50196078f, 1f);
        public static readonly Color Red = new(1f, 0f, 0f, 1f);
        public static readonly Color RosyBrown = new(0.7372549f, 0.56078431f, 0.56078431f, 1f);
        public static readonly Color RoyalBlue = new(0.25490196f, 0.41176471f, 0.88235294f, 1f);
        public static readonly Color SaddleBrown = new(0.54509804f, 0.27058824f, 0.0745098f, 1f);
        public static readonly Color Salmon = new(0.98039216f, 0.50196078f, 0.44705882f, 1f);
        public static readonly Color SandyBrown = new(0.95686275f, 0.64313725f, 0.37647059f, 1f);
        public static readonly Color SeaGreen = new(0.18039216f, 0.54509804f, 0.34117647f, 1f);
        public static readonly Color SeaShell = new(1f, 0.96078431f, 0.93333333f, 1f);
        public static readonly Color Sienna = new(0.62745098f, 0.32156863f, 0.17647059f, 1f);
        public static readonly Color Silver = new(0.75294118f, 0.75294118f, 0.75294118f, 1f);
        public static readonly Color SkyBlue = new(0.52941176f, 0.80784314f, 0.92156863f, 1f);
        public static readonly Color SlateBlue = new(0.41568627f, 0.35294118f, 0.80392157f, 1f);
        public static readonly Color SlateGray = new(0.43921569f, 0.50196078f, 0.56470588f, 1f);
        public static readonly Color Snow = new(1f, 0.98039216f, 0.98039216f, 1f);
        public static readonly Color SpringGreen = new(0f, 1f, 0.49803922f, 1f);
        public static readonly Color SteelBlue = new(0.2745098f, 0.50980392f, 0.70588235f, 1f);
        public static readonly Color Tan = new(0.82352941f, 0.70588235f, 0.54901961f, 1f);
        public static readonly Color Teal = new(0f, 0.50196078f, 0.50196078f, 1f);
        public static readonly Color Thistle = new(0.84705882f, 0.74901961f, 0.84705882f, 1f);
        public static readonly Color Tomato = new(1f, 0.38823529f, 0.27843137f, 1f);
        public static readonly Color Turquoise = new(0.25098039f, 0.87843137f, 0.81568627f, 1f);
        public static readonly Color Violet = new(0.93333333f, 0.50980392f, 0.93333333f, 1f);
        public static readonly Color Wheat = new(0.96078431f, 0.87058824f, 0.70196078f, 1f);
        public static readonly Color White = new(1f, 1f, 1f, 1f);
        public static readonly Color WhiteSmoke = new(0.96078431f, 0.96078431f, 0.96078431f, 1f);
        public static readonly Color Yellow = new(1f, 1f, 0f, 1f);
        public static readonly Color YellowGreen = new(0.60392157f, 0.80392157f, 0.19607843f, 1f);
    }
}