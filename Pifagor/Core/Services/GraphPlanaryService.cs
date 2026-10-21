using SharpGraph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pifagor.Core.Services
{
    public class GraphPlanaryService
    {
        public bool CheckPlanary(List<(string From, string To)> networkEdges)
        {
            var sharpEdges = new List<Edge>();

            foreach (var edge in networkEdges)
            {
                sharpEdges.Add(new Edge(edge.From, edge.To));
            }

            Graph graph = new Graph(sharpEdges);

            return graph.IsPlanar();
        }
    }
}
