using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamFaceIdentification.Model
{
    public class IdentifyResultModel
    {
        public List<Candidates> candidates { get; set; }
        public string faceid { get; set; }
    }

    public class Candidates
    {
        public double confidence { get; set; }
        public string personId { get; set; }

    }
}