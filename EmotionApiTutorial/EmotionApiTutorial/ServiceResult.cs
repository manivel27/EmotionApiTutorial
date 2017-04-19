using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionApiTutorial
{

    public class ServiceResult
    {
        public FeelingOfFace[] Faces { get; set; }
    }

    public class FeelingOfFace
    {
        public Facerectangle faceRectangle { get; set; }
        public Scores scores { get; set; }
    }

    public class Facerectangle
    {
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Scores
    {
        public float anger { get; set; }
        public float contempt { get; set; }
        public float disgust { get; set; }
        public float fear { get; set; }
        public float happiness { get; set; }
        public float neutral { get; set; }
        public float sadness { get; set; }
        public float surprise { get; set; }
    }

}
