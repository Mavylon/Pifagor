using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Msagl.Drawing;
using Pifagor.Core.Services;
using Microsoft.Msagl.WpfGraphControl;
using Pifagor.Ui.UserControls;
using Pifagor.Ui.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pifagor.Core.ViewModels.DiscreteMath
{
    public partial class NetworkModelViewModel : ViewModelBase
    {
        public ObservableCollection<ChangeRelationsViewModel> NodesRelations { get; set; } = new ObservableCollection<ChangeRelationsViewModel>();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanNodeRelationsEdit))]
        private int _nodeCount;
        partial void OnNodeCountChanged(int value)
        {
            ClearAllRelations();
        }
        public NetworkModelViewModel(NavigationService navigationService, IServiceProvider serviceProvider) : base(navigationService, serviceProvider) { }

        public bool CanNodeRelationsEdit => NodeCount > 0;

        [RelayCommand]
        public void FillRelationsEditList()
        {
            NodesRelations.Clear();

            if (NodeCount > 0)
            {
                for (int i = 0; i < NodeCount - 1; i++)
                {
                    NodesRelations.Add(new ChangeRelationsViewModel(NodeCount));   
                }
            }
        }

        [RelayCommand]
        public void ClearAllRelations()
        {
            NodesRelations.Clear();
        }

        [RelayCommand]
        public void CreateNewRelations()
        {
            if(NodeCount > 0)
            {
                NodesRelations.Add(new ChangeRelationsViewModel(NodeCount));
            } else
            {
                MessageBox.Show("Введите количество вершин", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        public void DeleteLastRelation()
        {
            if(NodesRelations.Count > 0)
            {
                NodesRelations.Remove(NodesRelations.Last());
            }
        }

        [RelayCommand]
        public void SolveTheProblem()
        {
            var graphService = ServiceProvider.GetRequiredService<GraphPlanaryService>();

            if(ValidateAllRelaions())
            {
                List<(string From, string To, string Work)> edges = new List<(string From, string To, string Work)>();

                foreach (var node in NodesRelations) {
                    edges.Add((node.From.ToString(), node.To.ToString(), node.Work.ToString()));   
                }

                var edgeStructure = new List<(string, string)>();
                foreach (var job in edges)
                {
                    edgeStructure.Add((job.From, job.To));
                }
                
                Graph visualGraph = new Graph("Network graph");

                visualGraph.Attr.Margin = 6;
                visualGraph.Attr.LayerDirection = LayerDirection.LR;

                foreach (var job in NodesRelations)
                {
                    var edge = visualGraph.AddEdge(job.From.ToString(), job.To.ToString());

                    // Выводим название вашей работы НАД ребром
                    edge.LabelText = job.Work.ToString();

                    // Стили текста работы
                    edge.Label.FontSize = 11;
                    edge.Label.FontColor = Color.DarkSlateGray;

                    // Делаем круглой форму узлов событий (по умолчанию они овальные)
                    var sourceNode = visualGraph.FindNode(job.From.ToString());
                    var targetNode = visualGraph.FindNode(job.To.ToString());
                    if (sourceNode != null) sourceNode.Attr.Shape = Shape.Circle;
                    if (targetNode != null) targetNode.Attr.Shape = Shape.Circle;
                }

                AutomaticGraphLayoutControl graphViewer = new AutomaticGraphLayoutControl();

                graphViewer.Graph = visualGraph;
            } 
            else 
            {
                MessageBox.Show("У вас есть повторяющиеся отношения!", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool ValidateAllRelaions()
        {
            return NodesRelations.Distinct().Count() == NodesRelations.Count;
        }
    }
}
