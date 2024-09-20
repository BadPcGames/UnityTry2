﻿using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace Assets
{
    public class Generator
    {
        private float minY;
        public float MinY
        {
            get
            {
                return minY;
            }
            set
            {
                minY = value;
            }
        }

        private float maxY;
        public float MaxY
        {
            get
            {
                return maxY;
            }
            set
            {
                maxY = value;
            }
        }

        private float[,] matrix;
        private int xPosition;
        private int zPosition;

        public Vector3[] GetVertices(int xSize,int zSize, int steps,int players,float dif, int smoothIteration)
        {
            matrix = new float[xSize+1, zSize+1];
            Vector3[] vertices=new Vector3[(xSize + 1) * (zSize + 1)];

            for (int z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    matrix[x,z] = minY;
                }
            }

            setVertices(xSize,  zSize,  steps,  players,  dif);
            
            for(int i = 0; i <= smoothIteration; i++)
            {
                smooth(xSize,zSize);
            }

            int mark = 0;
            for (int z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {   
                    vertices[mark]=new Vector3(x, matrix[x,z], z);
                    mark++;
                }
               
            }
           
            return vertices;
        }

        private void smooth(int sizeX,int sizeZ)
        {
            
            for (int z = 0; z <= sizeZ; z++)
            {
               
                for (int x = 0; x <= sizeX; x++)
                {
                    float sum = 0;
                    int col = 0;
                    if (x>0)
                    {
                        sum += matrix[x-1,z];
                        col++;
                    }
                    if (x < sizeX-1)
                    {
                        sum += matrix[x+1, z];
                        col++;
                    }
                    if (z > 0)
                    {
                        sum += matrix[x, z-1];
                        col++;
                    }
                    if (z < sizeZ - 1)
                    {
                        sum += matrix[x , z+1];
                        col++;
                    }
                    matrix[x,z] = (float)sum/(float)col;
                }

            }
        }

        private void setVertices(int xSize, int zSize, int steps, int players, float dif)
        {
            for (int i = 0; i < players; i++)
            {
                xPosition = Random.Range(0, xSize + 1);
                zPosition = Random.Range(0, zSize + 1);

                if (matrix[xPosition, zPosition] < maxY)
                {
                    matrix[xPosition, zPosition] += dif;
                }
                for (int j = 0; j < steps; j++)
                {
                    xPosition += Random.Range(-1, 2);
                    if (xPosition > xSize)
                    {
                        xPosition = xSize;
                    }
                    if (xPosition < 0)
                    {
                        xPosition++;
                    }
                    zPosition += Random.Range(-1, 2);
                    if (zPosition > zSize)
                    {
                        zPosition = zSize;
                    }
                    if (zPosition < 0)
                    {
                        zPosition++;
                    }
                    if (matrix[xPosition, zPosition] < maxY)
                    {
                        matrix[xPosition, zPosition] += dif;
                    }
                }
            }
        }
    }
}
