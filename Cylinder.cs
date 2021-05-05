using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GraphicsLab4
{
   class Cylinder : Figure
   {

      public Cylinder()
      {
         vertices = new List<Vector3>();
         faces = new List<Face>();
      }
      public override void InitFigure(string fileName)
      {
         using (StreamReader sr = new StreamReader(fileName))
         {
            string l = sr.ReadLine();
            string[] parameters = l.Split(' ');

            polygonBase = int.Parse(parameters[1]);
            radius = float.Parse(parameters[2]);
            height = float.Parse(parameters[3]);
            center = new Vector3(float.Parse(parameters[4]), float.Parse(parameters[5]), float.Parse(parameters[6]));

         }
      }
      public override void BuildMesh()
      {
         Vector3[] polygon;

         // Building of lower ring of cylinder
         polygon = CreatePolygon(new Vector3(center.X, center.Y - height / 2, center.Z), radius, polygonBase);
         for (int i = 0; i < polygonBase; i++)
            vertices.Add(polygon[i]);

         // Building of upper ring of cylinder
         polygon = CreatePolygon(new Vector3(center.X, center.Y + height / 2, center.Z), radius, polygonBase);
         for (int i = 0; i < polygonBase; i++)
            vertices.Add(polygon[i]);

         // Building of side faces
         for (int i = 0; i < polygonBase; i++)
         {
            int v0 = i;
            int v1 = i + polygonBase;
            int v2 = (i + polygonBase + 1) % polygonBase + polygonBase;

            faces.Add(new Face(v0, v1, v2));

            v1 = (i + 1) % polygonBase;

            faces.Add(new Face(v0, v1, v2));
         }

         // Building of lower circle
         vertices.Add(new Vector3(center.X, center.Y - height / 2, center.Z));
         for (int i = 0; i < polygonBase; i++)
         {
            int v0 = i;
            int v1 = (i + 1) % polygonBase;
            int v2 = vertices.Count - 1;

            faces.Add(new Face(v0, v1, v2));
         }

         // Building of upper circle
         vertices.Add(new Vector3(center.X, center.Y + height / 2, center.Z));
         for (int i = polygonBase; i < polygonBase * 2; i++)
         {
            int v0 = i;
            int v1 = (i + 1) % polygonBase + polygonBase;
            int v2 = vertices.Count - 1;

            faces.Add(new Face(v0, v1, v2));
         }

      }
   }
}
