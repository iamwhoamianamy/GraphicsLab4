using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GraphicsLab4
{
   abstract class Figure
   {
      protected List<Vector3> vertices;
      protected List<Face> faces;
      protected int polygonBase;
      protected float radius;
      protected float height;
      protected Vector3 center;

      //Bitmap bitmap;
      //int textureID;
      //public Face[] textureFaces;
      //public Vector2[][] textureCoords;

      public abstract void InitFigure(string fileName);

      public abstract void BuildMesh();
      
      public void DrawMesh()
      {
         GL.Begin(BeginMode.Triangles);

         for (int i = 0; i < faces.Count; i++)
         {
            GL.Vertex3(vertices[faces[i].v0]);
            GL.Vertex3(vertices[faces[i].v1]);
            GL.Vertex3(vertices[faces[i].v2]);
         }

         GL.End();
      }

      public void DrawGrid()
      {
         GL.Begin(BeginMode.Lines);

         for (int i = 0; i < faces.Count; i++)
         {
            GL.Vertex3(vertices[faces[i].v0]);
            GL.Vertex3(vertices[faces[i].v1]);

            GL.Vertex3(vertices[faces[i].v1]);
            GL.Vertex3(vertices[faces[i].v2]);

            GL.Vertex3(vertices[faces[i].v2]);
            GL.Vertex3(vertices[faces[i].v0]);
         }
         GL.End();
      }

      //public void BindTexture()
      //{
      //   GL.GenTextures(1, out textureID);

      //   GL.BindTexture(TextureTarget.Texture2D, textureID);

      //   BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
      //     ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      //   GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
      //       OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

      //   bitmap.UnlockBits(data);

      //   //GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, 6);

      //   GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
      //   GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
      //   GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
      //   GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
      //}

      //public void CalcTextureCoords()
      //{
      //   textureCoords = new Vector2[divisions][];

      //   for (int i = 0; i < divisions; i++)
      //      textureCoords[i] = new Vector2[polygonBase + 1];

      //   float[] lengthes = new float[polygonBase + 1];

      //   for (int i = 0; i < divisions - 1; i++)
      //      for (int j = 0; j < polygonBase + 1; j++)
      //         lengthes[j] += Vector3.Distance(sideVertices[i][(j + 1) % polygonBase], sideVertices[i + 1][(j + 1) % polygonBase]);

      //   float elemHeight = 1f / polygonBase;

      //   for (int j = 0; j < polygonBase + 1; j++)
      //      textureCoords[0][j] = new Vector2(0f, elemHeight * j);

      //   for (int i = 1; i < divisions; i++)
      //   {
      //      for (int j = 0; j < polygonBase + 1; j++)
      //      {
      //         float elemWidth = Vector3.Distance(sideVertices[i - 1][j % (polygonBase)], sideVertices[i][j % (polygonBase)]);

      //         float x = textureCoords[i - 1][j].X + elemWidth / lengthes[j];

      //         textureCoords[i][j] = new Vector2(x, elemHeight * j);
      //      }
      //   }

      //   textureFaces = new Face[2 * polygonBase * (divisions - 1)];

      //   int faceStart = 0;

      //   for (int i = 0; i < divisions - 1; i++)
      //   {
      //      for (int j = 0; j < polygonBase; j++)
      //      {
      //         Tuple<int, int> v0 = new Tuple<int, int>(i, j);
      //         Tuple<int, int> v1 = new Tuple<int, int>(i + 1, j);
      //         Tuple<int, int> v2 = new Tuple<int, int>(i + 1, (j + 1) % (polygonBase + 1));

      //         textureFaces[faceStart++] = new Face(v0, v1, v2);

      //         v1 = new Tuple<int, int>(i, (j + 1) % (polygonBase + 1));

      //         textureFaces[faceStart++] = new Face(v1, v0, v2);
      //      }
      //   }
      //}

      //public void DrawTexture()
      //{
      //   GL.BindTexture(TextureTarget.Texture2D, textureID);

      //   GL.Enable(EnableCap.Texture2D);
      //   GL.Begin(BeginMode.Triangles);

      //   for (int i = 0; i < sideFaces.Length; i++)
      //   {
      //      GL.TexCoord2(textureCoords[textureFaces[i].v0.Item1][textureFaces[i].v0.Item2]);
      //      GL.Vertex3(sideVertices[sideFaces[i].v0.Item1][sideFaces[i].v0.Item2]);

      //      GL.TexCoord2(textureCoords[textureFaces[i].v1.Item1][textureFaces[i].v1.Item2]);
      //      GL.Vertex3(sideVertices[sideFaces[i].v1.Item1][sideFaces[i].v1.Item2]);

      //      GL.TexCoord2(textureCoords[textureFaces[i].v2.Item1][textureFaces[i].v2.Item2]);
      //      GL.Vertex3(sideVertices[sideFaces[i].v2.Item1][sideFaces[i].v2.Item2]);
      //   }

      //   GL.End();
      //   GL.Disable(EnableCap.Texture2D);

      //   //GL.Begin(BeginMode.Triangles);

      //   //for (int i = 0; i < faces.Length; i++)
      //   //{
      //   //   GL.TexCoord2(0, 0);
      //   //   GL.Vertex3(vertices[faces[i].v0]);
      //   //   GL.TexCoord2(0, 1);
      //   //   GL.Vertex3(vertices[faces[i].v1]);
      //   //   GL.TexCoord2(1, 0);
      //   //   GL.Vertex3(vertices[faces[i].v2]);
      //   //}

      //   //GL.End();
      //   //GL.Disable(EnableCap.Texture2D);
      //}

      //public void ReadTexture(string fileName)
      //{
      //   bitmap = new Bitmap(fileName);
      //}

      protected Vector3[] CreatePolygon(Vector3 pos, float radius, int polygonBase)
      {
         Vector3[] res = new Vector3[polygonBase];
         double angle = 2.0 / polygonBase * Math.PI;
         double addAngle = 0;

         if (polygonBase == 3)
            addAngle = -angle / 4;
         else if (polygonBase == 4)
            addAngle = angle / 2;

         for (int i = 0; i < polygonBase; i++)
            res[i] = new Vector3(pos.X + radius * (float)Math.Cos(angle * i + addAngle),
                                 pos.Y,
                                 pos.Z + radius * (float)Math.Sin(angle * i + addAngle));

         return res;
      }
   }

   class Face
   {
      public int v0;
      public int v1;
      public int v2;

      public Face(int v0, int v1, int v2)
      {
         this.v0 = v0;
         this.v1 = v1;
         this.v2 = v2;
      }
   }
}
