using System.Collections.Generic;
using PixelsForGlory.ComputationalSystem;
using UnityEngine;

public static class MapGenerator {

    public static Texture2D GenerateMapTexture(int width = 4096, int height = 4096) {
        var voronoiDiagram = new VoronoiDiagram<Color>(new Rect(0f, 0f, width, height));    

        var points = new List<VoronoiDiagramSite<Color>>();
        while(points.Count < 1000) {
            int randX = Random.Range(0, width - 1);
            int randY = Random.Range(0, height - 1);

            var point = new Vector2(randX, randY);
            points.Add(new VoronoiDiagramSite<Color>(point, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))));
        }
            
        voronoiDiagram.AddSites(points);
        voronoiDiagram.GenerateSites(2);

        var outImg = new Texture2D(width, height);
        outImg.SetPixels(voronoiDiagram.Get1DSampleArray());
        outImg.Apply();

        System.IO.File.WriteAllBytes("/Users/skdamico/Desktop/diagram.png", outImg.EncodeToPNG());

        return outImg;
    }
}

