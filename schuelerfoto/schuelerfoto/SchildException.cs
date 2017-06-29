using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace schuelerausweis
{
    [Serializable]
    internal class SchildException : Exception
    {
        public string RectText { get; set; }
        public Brush RectTextColor { get; set; }
        public bool LblAttentionVisible { get; set; }        
        public string TsFooterText { get; set; }
        public Image TsFooterImage { get; set; }
        public bool PasswordVisible { get; set; }
        public Color RectBorderColor { get; private set; }
        public string LogEntry { get; set; }

        public SchildException(string rectText, Brush rectTextColor, Color rectBorderColor, bool lblAttentionVisible, string tsFooterText, Image tsFooterImage, bool passwordVisible, string logEntry)
        {            
            RectText = rectText;
            RectTextColor = rectTextColor;
            RectBorderColor = rectBorderColor;
            LblAttentionVisible = lblAttentionVisible;
            TsFooterText = tsFooterText;
            TsFooterImage = tsFooterImage;
            PasswordVisible = passwordVisible;
            LogEntry = logEntry;
        }
    }
}