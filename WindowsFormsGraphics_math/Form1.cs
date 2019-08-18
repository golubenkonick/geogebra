using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsGraphics_math
{
    public partial class Form1 : Form
    {
        private CoordinateSystem cs1;
        RealPoint firstPoint;
        RealPoint secondPoint;
        bool creatingLine;      
        List<RealFigure> realFigureList;
        RealPoint selectedPoint;
        RealFigure selected;
        RealPoint clickPoint;
        List<RealPoint> pointList;
        Label selectedLabel;
        RealSegment selectedSeg1 = null;
        RealSegment selectedSeg2 = null;
        //List<Label> labelList;

        public Form1()
        {
            InitializeComponent();

            // create Coordinate System
            int unitInterval = 30;
            int x0 = pictureBox1.Width / 2;
            int y0 = pictureBox1.Height / 2;
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;           
            cs1 = new CoordinateSystem(unitInterval, x0, y0, w,h);           
            firstPoint = null;
            secondPoint = null;        
            creatingLine = false;
            realFigureList = new List<RealFigure>();          
            selectedPoint = null;
            selectedLabel = null;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;            
            cs1.DrawCoordinateSystem(g);
            foreach (RealFigure figure in realFigureList)
            {
                figure.Draw(g, cs1);
            }
        }

       

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            double x;
            double y;

          

            if (IntersectButton.Checked)
            {
                if (selectedSeg1 == null)
                {
                    selectedSeg1 = (RealSegment)SelectFigure(e);
                    if (selectedSeg1 != null)
                    {
                        selectedSeg1.SetBackLight();
                    }
                    this.Text = "select first segment";
                }

                else
                {
                    this.Text = "select second segment";
                    selectedSeg2 = (RealSegment)SelectFigure(e);
                    if (selectedSeg2 != null)
                    {
                        selectedSeg1.UnSetBackLight();
                        RealIntersect intersectPoint = new RealIntersect(selectedSeg1, selectedSeg2);
                        realFigureList.Add(intersectPoint);
                        selectedSeg1 = null;
                        selectedSeg2 = null;
                    }
                }
            }




            if (AddPointButton.Checked)
            {
                if (RoundingButton.Checked)
                {
                    x = Math.Round(cs1.VisualToRealX(e.X));
                    y = Math.Round(cs1.VisualToRealY(e.Y));
                }
                else
                {
                    x = cs1.VisualToRealX(e.X);
                    y = cs1.VisualToRealY(e.Y);
                }               
                realFigureList.Add(new RealPoint(x, y));     
            }
           
            if (AddLineButton.Checked)
            {
                if (RoundingButton.Checked)
                {
                    x = Math.Round(cs1.VisualToRealX(e.X));
                    y = Math.Round(cs1.VisualToRealY(e.Y));
                }
                else
                {
                    x = cs1.VisualToRealX(e.X);
                    y = cs1.VisualToRealY(e.Y);
                }
                
                if (creatingLine)
                {
                    if (RoundingButton.Checked)
                    {
                        selectedPoint.x = Math.Round(selectedPoint.x);
                        selectedPoint.y = Math.Round(selectedPoint.y);
                    }
                    selectedPoint = null;
                    creatingLine = false;
                }
                else
                {
                    firstPoint = new RealPoint(x, y);                   
                    realFigureList.Add(firstPoint);
                    secondPoint = new RealPoint(x, y);
                    realFigureList.Add(secondPoint);
                    RealSegment rs = new RealSegment(firstPoint, secondPoint);
                    realFigureList.Add(rs);                    
                    selectedPoint = secondPoint;                   
                    creatingLine = true;
                }
            }
            if (AddRectangleButton.Checked)
            {
                if (RoundingButton.Checked)
                {
                    x = Math.Round(cs1.VisualToRealX(e.X));
                    y = Math.Round(cs1.VisualToRealY(e.Y));
                }
                else
                {
                    x = cs1.VisualToRealX(e.X);
                    y = cs1.VisualToRealY(e.Y);
                }

                if (creatingLine)
                {
                    if (RoundingButton.Checked)
                    {
                        selectedPoint.x = Math.Round(selectedPoint.x);
                        selectedPoint.y = Math.Round(selectedPoint.y);
                    }
                    selectedPoint = null;
                    creatingLine = false;
                }
                else
                {
                    firstPoint = new RealPoint(x, y);
                    realFigureList.Add(firstPoint);
                    secondPoint = new RealPoint(x, y);
                    realFigureList.Add(secondPoint);
                    RealRectangle rr = new RealRectangle(firstPoint, secondPoint);
                    realFigureList.Add(rr);
                    selectedPoint = secondPoint;
                    creatingLine = true;
                }
            }
            if (AddCircleButton.Checked)
            {
                if (RoundingButton.Checked)
                {
                    x = Math.Round(cs1.VisualToRealX(e.X));
                    y = Math.Round(cs1.VisualToRealY(e.Y));
                }
                else
                {
                    x = cs1.VisualToRealX(e.X);
                    y = cs1.VisualToRealY(e.Y);
                }

                if (creatingLine)
                {
                    if (RoundingButton.Checked)
                    {
                        selectedPoint.x = Math.Round(selectedPoint.x);
                        selectedPoint.y = Math.Round(selectedPoint.y);
                    }
                    selectedPoint = null;
                    creatingLine = false;
                }
                else
                {
                    firstPoint = new RealPoint(x, y);                   
                    realFigureList.Add(firstPoint);
                    secondPoint = new RealPoint(x, y);                    
                    realFigureList.Add(secondPoint);
                    realFigureList.Add(new RealCircle(firstPoint, secondPoint, cs1));
                    selectedPoint = secondPoint;   
                    creatingLine = true;
                }
            }
            if (RightTriangleButton.Checked)
            {
                if (RoundingButton.Checked)
                {
                    x = Math.Round(cs1.VisualToRealX(e.X));
                    y = Math.Round(cs1.VisualToRealY(e.Y));
                }
                else
                {
                    x = cs1.VisualToRealX(e.X);
                    y = cs1.VisualToRealY(e.Y);
                }

                if (creatingLine)
                {
                    if (RoundingButton.Checked)
                    {
                        selectedPoint.x = Math.Round(selectedPoint.x);
                        selectedPoint.y = Math.Round(selectedPoint.y);
                    }
                    selectedPoint = null;
                    creatingLine = false;
                }
                else
                {
                    firstPoint = new RealPoint(x, y);
                    realFigureList.Add(firstPoint);
                    secondPoint = new RealPoint(x, y);
                    realFigureList.Add(secondPoint);
                    RealRightTriangle rt = new RealRightTriangle(firstPoint, secondPoint);
                    realFigureList.Add(rt);
                    selectedPoint = secondPoint;
                    creatingLine = true;
                }

            }
            if (AddPolygonButton.Checked)
            {
                if (RoundingButton.Checked)
                {
                    x = Math.Round(cs1.VisualToRealX(e.X));
                    y = Math.Round(cs1.VisualToRealY(e.Y));
                }
                else
                {
                    x = cs1.VisualToRealX(e.X);
                    y = cs1.VisualToRealY(e.Y);
                }

                if (creatingLine)
                {
                    if (RoundingButton.Checked)
                    {
                        selectedPoint.x = Math.Round(selectedPoint.x);
                        selectedPoint.y = Math.Round(selectedPoint.y);
                    }                    
                    selectedPoint = new RealPoint(x, y);                    
                    int x1 = cs1.RealToVisualX(pointList[0].x);
                    int y1 = cs1.RealToVisualY(pointList[0].y);
                    if (cs1.GetDistance(x1, y1, e.X, e.Y) < cs1.radius)
                    {
                        creatingLine = false;
                        pointList.RemoveAt(pointList.Count - 1);
                        realFigureList.RemoveAt(realFigureList.Count - 1);
                    } 
                    else 
                    {
                        pointList.Add(selectedPoint);
                        realFigureList.Add(selectedPoint);
                    }
                }
                else
                {
                    pointList = new List<RealPoint>(); 
                    firstPoint = new RealPoint(x, y);                   
                    secondPoint = new RealPoint(x, y); 
                    realFigureList.Add(firstPoint);
                    realFigureList.Add(secondPoint);
                    pointList.Add(firstPoint);
                    pointList.Add(secondPoint);

                    RealPolygon polygon = new RealPolygon(pointList);
                    realFigureList.Add(polygon);
                    selectedPoint = secondPoint;
                    creatingLine = true;
                }

            }
            if (IsoscelesTriangleButton.Checked)
            {
                if (RoundingButton.Checked)
                {
                    x = Math.Round(cs1.VisualToRealX(e.X));
                    y = Math.Round(cs1.VisualToRealY(e.Y));
                }
                else
                {
                    x = cs1.VisualToRealX(e.X);
                    y = cs1.VisualToRealY(e.Y);
                }

                if (creatingLine)
                {
                    if (RoundingButton.Checked)
                    {
                        selectedPoint.x = Math.Round(selectedPoint.x);
                        selectedPoint.y = Math.Round(selectedPoint.y);
                    }
                    selectedPoint = null;
                    creatingLine = false;
                }
                else
                {
                    firstPoint = new RealPoint(x, y);
                    realFigureList.Add(firstPoint);
                    secondPoint = new RealPoint(x, y);
                    realFigureList.Add(secondPoint);
                    RealIsoscelesTriangle rt = new RealIsoscelesTriangle(firstPoint, secondPoint);
                    realFigureList.Add(rt);
                    selectedPoint = secondPoint;
                    creatingLine = true;
                }
            }
            pictureBox1.Invalidate();
        }



        private RealFigure SelectFigure(MouseEventArgs e)
        {
            foreach (RealFigure figure in realFigureList)
            {           
                double mouseX = cs1.VisualToRealX(e.X);
                double mouseY = cs1.VisualToRealY(e.Y);
                clickPoint = new RealPoint(mouseX, mouseY);                   
                if (figure.HitTest(clickPoint, cs1))
                {
                    return figure;
                }
            }
            return null;
        }

        //private void SelectFigure(MouseEventArgs e)
        //{
        //    foreach (RealFigure figure in realFigureList)
        //    {
        //        double mouseX = cs1.VisualToRealX(e.X);
        //        double mouseY = cs1.VisualToRealY(e.Y);
        //        clickPoint = new RealPoint(mouseX, mouseY);
        //        if (figure.HitTest(clickPoint, cs1))
        //        {
        //            selected = figure;
        //            break;
        //        }
        //    }
        //}


        private void SelectLabel(MouseEventArgs e) // top left --> center // change this code 
        { 
            foreach (RealFigure figure in realFigureList)
            {
                if (figure is RealPoint)
                {
                    RealPoint point = (RealPoint)figure;
                    double mouseX = cs1.VisualToRealX(e.X);
                    double mouseY = cs1.VisualToRealY(e.Y);
                    clickPoint = new RealPoint(mouseX, mouseY);
                    if (point.HitTestLabel(clickPoint))
                    {
                       selectedLabel = point.label;                       
                    }
                }
            }
        }
        

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
             if (MoveButton.Checked)
             {                 
                 SelectLabel(e);              
                 selected = SelectFigure(e); 
                 if (selected != null)
                 {                    
                     selected.SetBackLight();
                 }
                 pictureBox1.Invalidate();
             }

             if (DeleteButton.Checked)
             {                              
                 selected = SelectFigure(e);
                 realFigureList.Remove(selected);
                 selected = null;
                 pictureBox1.Invalidate();
             }
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {          

            // deselect label
            if (MoveButton.Checked && selectedLabel != null)
            {
                selectedLabel = null;               
            }

            // deselect figure
            if (MoveButton.Checked && selected != null)
            {
                selected.UnSetBackLight();               
                selected = null;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            Graphics g = pictureBox1.CreateGraphics();
            if (selectedPoint != null)
            {
                selectedPoint.x = cs1.VisualToRealX(e.X);
                selectedPoint.y = cs1.VisualToRealY(e.Y);
                pictureBox1.Invalidate();
            }

            if (selectedLabel != null)
            {               
                double distanceToMoveX = cs1.VisualToRealX(e.X) - clickPoint.x;
                double distanceToMoveY = cs1.VisualToRealY(e.Y) - clickPoint.y;
                selectedLabel.offsetX += distanceToMoveX;
                selectedLabel.offsetY += distanceToMoveY;
                clickPoint.x = cs1.VisualToRealX(e.X);
                clickPoint.y = cs1.VisualToRealY(e.Y);

                pictureBox1.Invalidate();
            }         
            if (selected != null)
            {                          
                double distanceToMoveX = cs1.VisualToRealX(e.X) - clickPoint.x;  
                double distanceToMoveY = cs1.VisualToRealY(e.Y) - clickPoint.y; 
                selected.Move(distanceToMoveX, distanceToMoveY);
                clickPoint.x = cs1.VisualToRealX(e.X);
                clickPoint.y = cs1.VisualToRealY(e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            MoveButton.Checked = true;
        }

        private void AddPointButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            AddPointButton.Checked = true;
            AddMenu.Text = "Add (Point)";
        }

        private void AddLineButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            AddLineButton.Checked = true;
            AddMenu.Text = "Add (Line)";
        }

        private void CheckEngine()
        {
            AddPointButton.Checked = false;
            AddLineButton.Checked = false;
            AddRectangleButton.Checked = false;
            AddCircleButton.Checked = false;
            RightTriangleButton.Checked = false;
            IsoscelesTriangleButton.Checked = false;
            AddPolygonButton.Checked = false;
            IntersectButton.Checked = false;
            MoveButton.Checked = false;
            DeleteButton.Checked = false;
        }

        private void AddRectangleButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            AddRectangleButton.Checked = true;
            AddMenu.Text = "Add (Rectangle)";
        }

        private void AddCircleButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            AddCircleButton.Checked = true;
            AddMenu.Text = "Add (Circle)";
        }

        private void RightTriangleButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            RightTriangleButton.Checked = true;
            AddMenu.Text = "Add (Right Triangle)";
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            DeleteButton.Checked = true;
        }

        private void IsoscelesTriangleButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            IsoscelesTriangleButton.Checked = true;
            AddMenu.Text = "Add (Isosceles Triangle)";
        }

        private void AddPolygonButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            AddPolygonButton.Checked = true;
            AddMenu.Text = "Add (Polygon)";
        }

        private void IntersectButton_Click(object sender, EventArgs e)
        {
            CheckEngine();
            IntersectButton.Checked = true;
            AddMenu.Text = "Add (Intersect)";
        }



    }
}
