using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatexPresentator.Model
{
    public interface ILatexParser
    {
        IEnumerable<ISlideModel> GetSlides(string filename);
    }
}
