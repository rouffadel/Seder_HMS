<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="WorkSiteGraph.aspx.cs" Inherits="AECLOGIC.ERP.HMS.WorkSiteGraph" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

<script src="Includes/JS/jsDraw2DX_Uncompressed.js" type="text/javascript"></script>

   <script type="text/javascript">

       function testDs() 
       {
           var vWorksite = AjaxDAL.GetEmpCountBYSite(1);
       }
   </script>

    <table width="100%">
        <tr>
           <td width="100%">
            
            <div class="content">
        <div class="section">
            <div id="graphics" style=" position: absolute; width: 600px; height: 400px;
                border: #999999 1px solid;">
            </div>
        </div>
    </div>
            </td>
        
        </tr>
    
    </table>
 <script type="text/javascript">
 
     // Data

     var vWorksite = AjaxDAL.GetEmpCountBYSite(1);
     var Vmaxmin = AjaxDAL.GetEmpCountBYSiteminMax(1);

     //Define graphics
     var graphicsDiv = document.getElementById("graphics");
     var gr = new jxGraphics(graphicsDiv);

     //Define pen and drawing brushes
     var penBlue = new jxPen(new jxColor('#77B7E1'), 1);

     var brushBlue = new jxBrush(new jxColor('#FFFFFF'));
     brushBlue.fillType = 'lin-grad';
     brushBlue.angle = 65;
     brushBlue.color2 = new jxColor('#2880B8');

     var brushText = new jxBrush(new jxColor('#054B78'));

     var brushRight = new jxBrush(new jxColor('#2880B8'));
     brushRight.color2 = new jxColor('#0C5D91');
     brushRight.fillType = 'lin-grad';
     brushRight.angle = 90;

     var shadowBrush = new jxBrush(new jxColor('#A0A0A0'));
     shadowBrush.fillType = 'lin-grad';

     //Define text font
     var font = new jxFont();
     font.size = 12;
     font.weight = 'bold';

     //Create Bar3D objects
     var z = 50;
     var maxvalue, MinValue, scale;
     maxvalue = Vmaxmin.value.Rows[0].maxval;
     MinValue = Vmaxmin.value.Rows[0].minval;

     // scaling 

     var divhight = 400;

     if (MinValue != maxvalue) 
     {
         scale = divhight / (maxvalue - MinValue);
     }
     else 
     {
         scale = 1;
     }
     scale =scale * (70 / 100);
     
     for (i = 0; i < vWorksite.value.Rows.length; i++) 
     {
         var b1 = new Bar3D(z, vWorksite.value.Rows[i].NoofEmp * scale, vWorksite.value.Rows[i].Site_Name);
         b1.animate();
         z = z + 70;
     }

     //Define Bar3D class to hold 3D bar information and drawing methods
     function Bar3D(x, y, z) {
         //Define drawing objects like polygons, font, texts etc.
         var rectFront = new jxRect(), polyRight = new jxPolygon(), polyTop = new jxPolygon(), polyShadow = new jxPolygon();
         var xText = new jxText(), yText = new jxText();

         var step, ys, intId;
         step = y / scale;
         //step = y/30;

         ys = step;

         //Assign static(not to chnage while animating) properties to the drawing objects
         rectFront.pen = penBlue;
         rectFront.brush = brushBlue;
         polyRight.pen = penBlue;
         polyRight.brush = brushRight;
         polyTop.pen = penBlue;
         polyTop.brush = brushBlue;
         polyShadow.brush = shadowBrush;

         yText.text =Math.round((y / scale),2);
         yText.font = font;
         yText.brush = brushText;

         xText.text = z;
         xText.font = font;
         xText.brush = brushText;
         xText.point = new jxPoint(70 + x, 330);
         xText.angle = 90;

         //Method to draw 3D bar
         this.drawStep = function () 
         {
             //Assign dynamic(to be changed for animation) properties to the animation
             rectFront.point = new jxPoint(50 + x, 350 - ys);
             rectFront.width = 30;
             rectFront.height = ys;

             polyRight.points = [new jxPoint(80 + x, 350 - ys), new jxPoint(100 + x, 330 - ys), new jxPoint(100 + x, 330), new jxPoint(80 + x, 350)];
             polyTop.points = [new jxPoint(50 + x, 350 - ys), new jxPoint(70 + x, 330 - ys), new jxPoint(100 + x, 330 - ys), new jxPoint(80 + x, 350 - ys)];
             polyShadow.points = [new jxPoint(50 + x, 350), new jxPoint(70 + x + ys / 3, 330 - ys / 5), new jxPoint(100 + x + ys / 3, 330 - ys / 5), new jxPoint(80 + x, 350)];

             yText.point = new jxPoint(60 + x, 325 - ys);

             polyShadow.draw(gr);
             rectFront.draw(gr);
             polyRight.draw(gr);
             polyTop.draw(gr);
             yText.draw(gr);

             ys += step;
             if (ys > y) 
             {
                 clearInterval(intId);
                 xText.draw(gr);
             }
         }

         //Call bar drawing method at intervals to have animation effect
         this.animate = function () 
         {
             intId = setInterval(this.drawStep, 50);
         }
     }
    </script>  
</asp:Content>

