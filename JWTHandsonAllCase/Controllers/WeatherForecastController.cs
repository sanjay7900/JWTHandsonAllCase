using iText.Forms.Form.Element;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Safety;
using iText.StyledXmlParser.Jsoup.Select;
using iTextSharp.text;
using iTextSharp.text.pdf;
using JWTHandsonAllCase.Common;
using JWTHandsonAllCase.Models.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Rotativa.AspNetCore;
using System;
using static iTextSharp.text.pdf.AcroFields;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel;
using System.Text;

namespace JWTHandsonAllCase.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery] UserRequest userRequest)
        {
            var htt = HttpContext.Request;
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public  FileResult GetPdf([FromForm] Re htmlcontent2)
        {
            var htmlContent = "<html><head><style> .main{ width:50%;height:50%;background:red; float:right;}/* Your CSS styles here */</style></head><body><h1>Hello, World!</h1>" +
                   "<div class='main'> hi i m using div </div></body></html>";


            string n;
            htmlContent = @"< header >
<style>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,400;0,600;0,700;1,400&display=swap');

/* 
Define all colors used in this template 
*/
:root{
  --main-highlight-color:#60D0E4;
  --secondary-highlight-color:#B8E6F1;
  --list-point-color:#C2C6CA;
  --text-color:#303E48;
}

@page{
  /*
  This CSS highlights how page sizes, margins, and margin boxes are set.
  https://docraptor.com/documentation/article/1067959-size-dimensions-orientation

  Within the page margin boxes content from running elements is used instead of a 
  standard content string. The name which is passed in the element() function can
  be found in the CSS code below in a position property and is defined there by 
  the running() function.
  */
  size:A4;
  margin:5cm 1.2cm 1.2cm 8cm;
  
  @top-left-corner{
    content:"""";
    background-color:var(--main-highlight-color);
  }
  
  @top-left{
    content:element(header);
    margin-left:-8cm;
    width:210mm;
    background-color:var(--main-highlight-color);
  }
  
  @top-right-corner{
    content:"""";
    background-color:var(--main-highlight-color);
  }
  
  @left-top{
    content:element(aside);
    background-color:var(--secondary-highlight-color);
  }
  
  @bottom-left-corner{
    content:"""";
    background-color:var(--secondary-highlight-color);
  }
}

/* 
The body itself is used to set the default font family, size and color for the document.
*/
body{
  font-family: 'Montserrat', sans-serif;
  font-size:10pt;
  color:var(--text-color);
}

/*
We remove the margin from headings level 3 and 4 and also set the font size to 10pt.
*/
h3,
h4{
  font-size:10pt;
  margin:0;
}

/*
The links in the document should not be highlighted by an different color and underline
instead we use the color value inherit to get the current texts color.
*/
a{
  color:inherit;
  text-decoration:none;
}

/*
The page header in our document uses the HTML HEADER element, it will appear
in the top page margin boxes (see @page rule above). Also a padding of top and bottom
1cm and left and right 1.5cm is set.

To show the name on the left and details like the current title, email address etc on the 
right side we use flexbox.
*/
header{
  position:running(header);
  padding:1cm 1.5cm 1cm 1.5cm;
  display:flex;
  justify-content:space-between;
  align-items:top;
}

/*
The div element with the header details gets a text align right, line height of 16pt and
the complete text within the element is set to uppercase.
*/
header div{
  text-align:right;
  line-height:16pt;
  text-transform:uppercase;
}

/*
The only element in the header details that should not have the text uppercase is the current
job title, so here we reset the text transformation.
*/
header div h2{
  font-size:12pt;
  margin:0;
  text-transform:none;
}

/*
The aside element uses the position running to move the element in the page margin boxes 
on the left side of each page.

The content itself gets a padding of 1.5cm on all sides.
*/
aside{
  position:running(aside);
  padding:1.5cm;
  text-align:left;
}

/*
The image of Joe McFadden is placed in a circle with an black border. 
To do so we set a fixed size, the image as background image, position the image
and apply the border and border radius.

Additionally we want the image to be partly in the header section so we move it 
up by half of the circle's height with the help of position absolute.
*/
aside span.img{
  width:3cm;
  height:3cm;
  background-image:url(""https://images.unsplash.com/photo-1583195764036-6dc248ac07d9?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2376&q=80"");
  background-size:cover;
  background-repeat:no-repeat;
  background-position:center;
  border:1.5mm solid var(--text-color);
  border-radius:100%;
  display:block;
  position:absolute;
  top:-1.5cm;
}

/*
The sections in the aside element are separated by hr elements. We use a margin
of 2cm on the top, 0 left and right and half a centimeter on the bottom. 

The height of the hr element gets set to 0, we use a border instead with the same
color as the text.
*/
aside hr{
  margin:2cm 0 .5cm 0;
  border:0;
  border-top:.5mm solid var(--text-color);
  height:0;
}

/*
If a header level 3 is followed by a header of level 4 we give it a margin of 2.5mm.
*/
aside h3 + h4{
  margin-top:2.5mm;
}

/*
H4 headers get a margin top of 5mm.
*/
aside h4{
  margin-top:5mm;
}

/*
The link to the website is shown all uppercase and far below the skills section.
For this we just set a margin top.
*/
aside a{
  display:block;
  margin-top:6cm;
  font-weight:bold;
  text-transform:uppercase;
}

/*
Looking closely we see that the TLD .com is in lowercase so we but this in a separate span
and use the text transform to none.
*/
aside a span{
  text-transform:none;
}

/*
All paragraphs in the aside element should have no margin.
*/
aside p{
  margin:0;
}

/*
The main content on the right side gets a margin top of 1.2cm and a margin left of
the same 1.2cm. This is done to keep some space from the blue header and sidebar.
*/
main{
  margin:1.2cm 0 0 1.2cm;
}

/*
All headers level 3 in the main content get a margin top of 1cm.
*/
main h3{
  margin-top:1cm;
  margin-bottom:1em;
}

/*
The professional experience is done via an ordered list, as we want nicely styled list points 
so instead of using decimal or decimal with leading zero as list style type, we set it to none.

We actually use a counter for the list points and to do so we first need to reset it.
*/
main ol{
  list-style-type:none;
  counter-reset:listitems;
  margin-left:1cm;
}

/*
For each list element we increment the counter.
*/
main ol > li{
  counter-increment:listitems;
}

/*
Before each list element we add the counter on the left side and give it some different style
like a larger font size.
*/
main ol > li:before{
  content:""0"" counter(listitems);
  float:left;
  margin-left:-1cm;
  font-size:18pt;
  color:var(--list-point-color);
}

/*
The unordered lists in the ""Outcomes"" part get a margin left of half a cm.
*/
main ul{
  margin-left:.5cm;
}

/*
Headers level 4 followed by an paragraph get no margin so there is no space between them.
*/
main h4 + p{
  margin:0;
}

/*
The company names in the experience section should always be displayed uppercase.
*/
main .company{
  text-transform:uppercase;
}

/*
Finally all headers level 3 get a special font color and get displayed uppercase.
*/
h3{
  color:var(--main-highlight-color);
  text-transform:uppercase;
}


/*
Import the desired font from Google fonts.
*/
@import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,400;0,600;0,700;1,400&display=swap');

/*
Define all colors used in this template
*/
:root{
  --main-highlight-color:#60D0E4;
  --secondary-highlight-color:#B8E6F1;
  --list-point-color:#C2C6CA;
  --text-color:#303E48;
}

@page{
  /*
  This CSS highlights how page sizes, margins, and margin boxes are set.
  https://docraptor.com/documentation/article/1067959-size-dimensions-orientation

  Within the page margin boxes content from running elements is used instead of a
  standard content string. The name which is passed in the element() function can
  be found in the CSS code below in a position property and is defined there by
  the running() function.
  */
  size:US-Letter;
  margin:5cm 1.2cm 1.2cm 8cm;

  @top-left-corner{
    content:"""";
    background-color:var(--main-highlight-color);
  }

  @top-left{
    content:element(header);
    margin-left:-8cm;
    width:210mm;
    background-color:var(--main-highlight-color);
  }

  @top-right-corner{
    content:"""";
    background-color:var(--main-highlight-color);
  }

  @left-top{
    content:element(aside);
    background-color:var(--secondary-highlight-color);
  }

  @bottom-left-corner{
    content:"""";
    background-color:var(--secondary-highlight-color);
  }
}

/*
The body itself is used to set the default font family, size and color for the document.
*/
body{
  font-family: 'Montserrat', sans-serif;
  font-size:10pt;
  color:var(--text-color);
}

/*
We remove the margin from headings level 3 and 4 and also set the font size to 10pt.
*/
h3,
h4{
  font-size:10pt;
  margin:0;
}

/*
The links in the document should not be highlighted by an different color and underline
instead we use the color value inherit to get the current texts color.
*/
a{
  color:inherit;
  text-decoration:none;
}

/*
The page header in our document uses the HTML HEADER element, it will appear
in the top page margin boxes (see @page rule above). Also a padding of top and bottom
1cm and left and right 1.5cm is set.

To show the name on the left and details like the current title, email address etc on the
right side we use flexbox.
*/
header{
  position:running(header);
  padding:1cm 1.5cm 1cm 1.5cm;
  display:flex;
  justify-content:space-between;
  align-items:top;
}

/*
The div element with the header details gets a text align right, line height of 16pt and
the complete text within the element is set to uppercase.
*/
header div{
  text-align:right;
  line-height:16pt;
  text-transform:uppercase;
}

/*
The only element in the header details that should not have the text uppercase is the current
job title, so here we reset the text transformation.
*/
header div h2{
  font-size:12pt;
  margin:0;
  text-transform:none;
}

/*
The aside element uses the position running to move the element in the page margin boxes
on the left side of each page.

The content itself gets a padding of 1.5cm on all sides.
*/
aside{
  position:running(aside);
  padding:1.5cm;
  text-align:left;
}

/*
The image of Joe McFadden is placed in a circle with an black border.
To do so we set a fixed size, the image as background image, position the image
and apply the border and border radius.

Additionally we want the image to be partly in the header section so we move it
up by half of the circle's height with the help of position absolute.
*/
aside span.img{
  width:3cm;
  height:3cm;
  background-image:url(""https://images.unsplash.com/photo-1583195764036-6dc248ac07d9?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2376&q=80"");
  background-size:cover;
  background-repeat:no-repeat;
  background-position:center;
  border:1.5mm solid var(--text-color);
  border-radius:100%;
  display:block;
  position:absolute;
  top:-1.5cm;
}

/*
The sections in the aside element are separated by hr elements. We use a margin
of 2cm on the top, 0 left and right and half a centimeter on the bottom.

The height of the hr element gets set to 0, we use a border instead with the same
color as the text.
*/
aside hr{
  margin:2cm 0 .5cm 0;
  border:0;
  border-top:.5mm solid var(--text-color);
  height:0;
}

/*
If a header level 3 is followed by a header of level 4 we give it a margin of 2.5mm.
*/
aside h3 + h4{
  margin-top:2.5mm;
}

/*
H4 headers get a margin top of 5mm.
*/
aside h4{
  margin-top:5mm;
}

/*
The link to the website is shown all uppercase and far below the skills section.
For this we just set a margin top.
*/
aside a{
  display:block;
  margin-top:4cm;
  font-weight:bold;
  text-transform:uppercase;
}

/*
Looking closely we see that the TLD .com is in lowercase so we but this in a separate span
and use the text transform to none.
*/
aside a span{
  text-transform:none;
}

/*
All paragraphs in the aside element should have no margin.
*/
aside p{
  margin:0;
}

/*
The main content on the right side gets a margin top of 1.2cm and a margin left of
the same 1.2cm. This is done to keep some space from the blue header and sidebar.
*/
main{
  margin:1.2cm 0 0 1.2cm;
}

/*
All headers level 3 in the main content get a margin top of 1cm.
*/
main h3{
  margin-top:1cm;
  margin-bottom:1em;
}

/*
The professional experience is done via an ordered list, as we want nicely styled list points
so instead of using decimal or decimal with leading zero as list style type, we set it to none.

We actually use a counter for the list points and to do so we first need to reset it.
*/
main ol{
  list-style-type:none;
  counter-reset:listitems;
  margin-left:1cm;
}

/*
For each list element we increment the counter.
*/
main ol > li{
  counter-increment:listitems;
}

/*
Before each list element we add the counter on the left side and give it some different style
like a larger font size.
*/
main ol > li:before{
  content:""0"" counter(listitems);
  float:left;
  margin-left:-1cm;
  font-size:18pt;
  color:var(--list-point-color);
}

/*
The unordered lists in the ""Outcomes"" part get a margin left of half a cm.
*/
main ul{
  margin-left:.5cm;
}

/*
Headers level 4 followed by an paragraph get no margin so there is no space between them.
*/
main h4 + p{
  margin:0;
}

/*
The company names in the experience section should always be displayed uppercase.
*/
main .company{
  text-transform:uppercase;
}

/*
Finally all headers level 3 get a special font color and get displayed uppercase.
*/
h3{
  color:var(--main-highlight-color);
  text-transform:uppercase;
}
</style>
  < h1 > Joe McFadden </ h1 >
  < div >
    < h2 > Digital Strategist & amp; Marketing </ h2 >
    < a href = 'mailto:joemcfadden@email.com' > joemcfadden@email.com </ a >
    < br />
    < a href = 'https://joewebsite.com' > joewebsite.com </ a >
    < br />
    317.123.6543
  </ div >
</ header >
< !--
The aside element is used to show education and skills on the left side
of each page.
-- >
< aside >
  < !--
  The picture gets included via a CSS background image.
  -->
  < span class='img'></span>
  <hr />
  <h3>Education</h3>
  <h4>Bachelor Degree</h4>
  <p>
    Name of University
    <br />
    2011-2015
  </p>
  <h4>Masters Degree</h4>
  <p>
    Name of University
    <br />
    2015-2017
  </p>
  <hr />
  <h3>Skills</h3>
  <h4>Item Name Goes Here</h4>
  <p>
    Description goes here
  </p>
  <h4>Lorem Ipsum</h4>
  <p>
    Description goes here
        </p>
  <h4>Dolor Set Amit Caslum</h4>
  <p>
    Description goes here
        </p>
  <a href = 'https://joewebsite.com' > joewebsite<span>.com </ span ></ a >
</ aside >
< !--
        The main element contains the actual content of the resume, so the summary
and professional experience.
-->
<main>
  <h3>Summary</h3>
  <h4>Corporate Real Estate Executive</h4>
  <p>
    Increasing Bottom-Line Profitability Through Real Estate Strategies
  </p>
  <p>
    Accomplished executive with a proven ability to develop and implement real-estate strategies that support business and financial objectives. Have led key initiatives that reduced operating budget by $32 million and contributed to 550% stock increase. Recognized as an expert in applying financial concepts to asset management decisions.
  </p>
  <h3>Professional Experience</h3>
  <ol>
    <li>
      <h4>Corporate Real Estate Executive</h4>
      <p>
        <span class='company'>Company Name</span> . Boston, MA.Data Analyst, 2019
      </p>
      <p>
        Data Mining and Modeling: <i>Collected, cleansed, and provided modeling and analyses of structured and unstructured data used for major business initiatives.</i>
      </p>
      <p>
        Outcomes:
        <ul>
          <li>Executed 15% reduction in transportation costs, resulting in $1.2M annual savings.</li>
          <li>Improved demand forecasting that reduced backorders to retail partners by 17%.</li>
          <li>Completed focus group and BI research that helped boost NW region sales by 10%.</li>
        </ul>
      </p>
    </li>
    <li>
      <h4>Corporate Real Estate Executive</h4>
      <p>
        <span class='company'>Company Name</span> . Boston, MA.Data Analyst, 2019
      </p>
      <p>
        Data Mining and Modeling: <i>Collected, cleansed, and provided modeling and analyses of structured and unstructured data used for major business initiatives.</i>
      </p>
      <p>
        Outcomes:
        <ul>
          <li>Executed 15% reduction in transportation costs, resulting in $1.2M annual savings.</li>
          <li>Completed focus group and BI research that helped boost NW region sales by 10%.</li>
        </ul>
      </p>
    </li>
  </ol>
</main>";

            string fileContent;
            using (var memoryStream = new MemoryStream())
            {
                // Copy the file content to a memory stream
                 htmlcontent2.file.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Read the content of the memory stream as a string
                fileContent = Encoding.UTF8.GetString(memoryStream.ToArray());
               n= fileContent.Replace("\r", "").Replace("\n", ""); 
                 

                // You can now use the file content as needed
                Console.WriteLine("File content:");
                Console.WriteLine(fileContent);

               
            }

            //    MemoryStream memoryStream = new MemoryStream();
            //PdfWriter pdf = new PdfWriter();
            //    ConverterProperties properties = new ConverterProperties();
            //    HtmlConverter.ConvertToPdf(htmlContent, memoryStream, properties);

            // Reset the position of the memory stream to the beginning
            //   memoryStream.Seek(0, SeekOrigin.Begin);
            byte[] pdfBytes = HtmlToPdfConverter.ConvertHtmlToPdf(n);

            // Return the PDF file as a downloadable file
            return  File(pdfBytes, "application/pdf", "output.pdf");

            // Return the memory stream content as a downloadable file to the client
            //return File(memoryStream, "application/pdf", "file.pdf");



        }



    }
   public class Re
    {
        public IFormFile file
        {
            get; set;
        }
    }
}

 

